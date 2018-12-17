using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMarket.Areas.Admin.Models;
using EMarket.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EMarket.Areas.Client.Controllers
{
    [Area("Client")]
    public class LoginController : Controller
    {
        
            private readonly EMarketContext _context;

            public LoginController(EMarketContext context)
            {
                _context = context;
            }

            public IActionResult Index()
            {
                return View();
            }

            public IActionResult Logout()
            {
                HttpContext.Session.SetString("User", "");
                return RedirectToAction("Index", "HangHoa");
            }



            [ValidateAntiForgeryToken]
            public IActionResult Login(LoginViewModel user)
            {
                var result = _context.TaiKhoan.Where(p => p.UserName == user.Username).FirstOrDefault();
                if (result != null)
                {
                    if (result.Password == Encryptor.MD5Hash(user.Password))
                    {
                       
                        HttpContext.Session.SetString("User", result.UserName);
                        return RedirectToAction("Index", "HangHoa");
                    }
                    else
                    {
                        ViewData["Error"] = "Mật Khẩu Không Chính Xác";
                        return View("Index");
                    }
                }
                else
                {
                    ViewData["Error"] = "Tài Khoản Không Tồn Tại";
                    return View("Index");
                }
            }

            [HttpGet]
            public IActionResult Register()
            {

                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Register([Bind("UserName,Email,Password")] TaiKhoan register, string repassword)
            {
                var check = _context.TaiKhoan.Where(p => p.UserName == register.UserName).FirstOrDefault();
                if (check != null) { ViewData["RegisterError"] = "Tài Khoản Đã Tồn Tại"; return View("Register"); }
                register.LoaiTaiKhoan = true;
                register.NgayDk = DateTime.Now;

                if (ModelState.IsValid)
                {
                    if (repassword == register.Password)
                    {
                        register.Password = Encryptor.MD5Hash(register.Password);
                        _context.Add(register);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewData["RegisterError"] = "Mật Khẩu không Khớp";
                        return View("Register");
                    }
                }
                return View("Index");
            }
        
    }
}