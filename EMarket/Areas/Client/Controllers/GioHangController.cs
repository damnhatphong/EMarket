using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EMarket.Areas.Admin.Models;
using EMarket.Areas.Client.Helpers;
using EMarket.Areas.Client.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace EMarket.Areas.Client.Controllers
{
    [Area("Client")]
    public class GioHangController : Controller
    {
        private readonly EMarketContext eMarketContext;

        public GioHangController(EMarketContext context)
        {
            eMarketContext = context;
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
                cart.Add(new GioHang { HangHoa = eMarketContext.HangHoa.Where(p=>p.HangHoaId == id).FirstOrDefault(), SoLuong = soLuong });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<GioHang> cart = SessionHelper.GetObjectFromJson<List<GioHang>>(HttpContext.Session, "cart");
                int index = IsExist(id);
                if (index != -1)
                {
                    cart[index].SoLuong+=soLuong;
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
        public IActionResult ThanhToan(string name,string email,string address,string tel,string danhsach)
        {
            List <GioHang> danhsachhang = JsonConvert.DeserializeObject<List<GioHang>>(danhsach);       
           
            HttpContext.Session.SetString("cart", "");
            string key = HttpContext.Session.GetString("User");
            var user = eMarketContext.TaiKhoan.Include(p => p.ThongTinTaiKhoan).Where(p => p.UserName == key).FirstOrDefault();
           
            HoaDon hoadon = new HoaDon();
            hoadon.TenKhachHang = name;
            hoadon.Sdt = tel;
            hoadon.DiaChi = address;
            hoadon.Email = user.Email;
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
            return RedirectToAction("Index","HangHoa");
        }

    }
}