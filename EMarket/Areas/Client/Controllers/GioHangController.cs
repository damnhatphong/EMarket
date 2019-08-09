using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EMarket.Areas.Admin.Models;
using EMarket.Areas.Client.Helpers;
using EMarket.Areas.Client.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EMarket.Services.PayPal;
using PayPal.v1.Payments;

namespace EMarket.Areas.Client.Controllers
{
    [Area("Client")]
    public class GioHangController : Controller
    {
        private readonly EMarketContext eMarketContext;
        private readonly ILogger<GioHangController> _logger;
        private readonly IPayPalPayment _payPal;

        public GioHangController(EMarketContext context, ILogger<GioHangController> logger,IPayPalPayment payPal)
        {
            eMarketContext = context;
            _logger = logger;
            _payPal = payPal;
        }


        public IActionResult Index()
        {
            return RedirectToAction("Index", "HangHoa");
        }


        public IActionResult Them(int id, int soLuong = 0)
        {
            if (SessionHelper.GetObjectFromJson<List<GioHang>>(HttpContext.Session, "cart") == null)
            {
                List<GioHang> cart = new List<GioHang>();
                cart.Add(new GioHang { HangHoa = eMarketContext.HangHoa.Where(p => p.HangHoaId == id).FirstOrDefault(), SoLuong = soLuong });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<GioHang> cart = SessionHelper.GetObjectFromJson<List<GioHang>>(HttpContext.Session, "cart");
                int index = IsExist(id);
                if (index != -1)
                {
                    cart[index].SoLuong += soLuong;
                }
                else
                {
                    cart.Add(new GioHang { HangHoa = eMarketContext.HangHoa.Where(p => p.HangHoaId == id).FirstOrDefault(), SoLuong = soLuong });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }


        public IActionResult Xoa(int id)
        {
            List<GioHang> cart = SessionHelper.GetObjectFromJson<List<GioHang>>(HttpContext.Session, "cart");
            int index = IsExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");
        }

        private int IsExist(int id)
        {
            List<GioHang> cart = SessionHelper.GetObjectFromJson<List<GioHang>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].HangHoa.HangHoaId.Equals(id))
                {
                    return i;
                }
            }
            return -1;
        }

        [HttpPost]
        public IActionResult ThanhToan(string name, string email, string address, string tel)
        {
            List<GioHang> danhsachhang = SessionHelper.GetObjectFromJson<List<GioHang>>(HttpContext.Session, "cart");

            CreateInvoice(danhsachhang, name, email, address, tel);
            HttpContext.Session.SetString("cart", "");           

            foreach (var item in danhsachhang)
            {
                var topselling = eMarketContext.TopSelling.Where(p => p.HangHoaId == item.HangHoa.HangHoaId).FirstOrDefault();
                if (topselling == null)
                {
                    var newcolumn = new TopSelling();
                    newcolumn.HangHoaId = item.HangHoa.HangHoaId;
                    newcolumn.SoLan = 1;
                    eMarketContext.Add(newcolumn);
                    eMarketContext.SaveChanges();
                }
                else
                {
                    topselling.SoLan += 1;
                    eMarketContext.Update(topselling);
                    eMarketContext.SaveChanges();
                }
            }
           
            TempData["status"] = "Đặt Hàng Thành Công";
            return RedirectToAction("Index", "HangHoa");
        }


        private void CreateInvoice(List<GioHang> danhsachhang, string name, string email, string address, string tel)
        {
            string value = SessionHelper.GetObjectFromJson<string>(HttpContext.Session,"User");
            var user = eMarketContext.TaiKhoan.Include(p => p.ThongTinTaiKhoan).Where(p => p.UserName == value).FirstOrDefault();

            HoaDon hoadon = new HoaDon();
            hoadon.TenKhachHang = name ?? user.UserName;
            hoadon.Sdt = tel ?? user.ThongTinTaiKhoan.Sdt;
            hoadon.DiaChi = address ?? user.ThongTinTaiKhoan.DiaChi;
            hoadon.Email = email ?? user.Email;
            hoadon.NgayLapHoaDon = DateTime.Now;
            hoadon.TinhTrang = false;

            eMarketContext.Add(hoadon);
            eMarketContext.SaveChanges();

            foreach (var item in danhsachhang)
            {
                ChiTietHoaDon chitethoadon = new ChiTietHoaDon();
                chitethoadon.HangHoaId = item.HangHoa.HangHoaId;
                chitethoadon.SoLuong = item.SoLuong;
                chitethoadon.TongTien = item.SoLuong * item.HangHoa.Gia;
                chitethoadon.HoaDonId = hoadon.HoaDonId;
                eMarketContext.Add(chitethoadon);
                eMarketContext.SaveChanges();
            }
        }


        [HttpPost]
        public async Task<IActionResult> PaypalPayment()
        {
            List<GioHang> danhsachhang = SessionHelper.GetObjectFromJson<List<GioHang>>(HttpContext.Session, "cart");
            List<Item> items = new List<Item>();
            double total = 0;

            foreach (var x in danhsachhang)
            {   
                items.Add(new Item() {
                    Name = x.HangHoa.TenHangHoa,
                    Currency = "USD",
                    Price = x.HangHoa.Gia.ToString(),
                    Quantity = x.SoLuong.ToString(),
                    Sku = "sku",
                    Tax = "0"
                });
                total += x.HangHoa.Gia * x.SoLuong;
            }

            Payment payment = _payPal.CreatePayment(total, @"https://www.google.com.vn/", @"https://www.facebook.com/","sale",items);
            string paypalRedirectUrl = await _payPal.ExecutePayment(payment);
            if (paypalRedirectUrl == "fail") {
                return RedirectToAction("Fail");
            }
            return Redirect(paypalRedirectUrl);
        }

        public IActionResult Success()
        {
            //Tạo đơn hàng trong CSDL với trạng thái : Đã thanh toán, phương thức: Paypal
            return Content("Thanh toán thành công");
        }

        public IActionResult Fail()
        {
            //Tạo đơn hàng trong CSDL với trạng thái : Chưa thanh toán, phương thức: 
            return Content("Thanh toán thất bại");
        }
    }
}