using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EMarket.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}