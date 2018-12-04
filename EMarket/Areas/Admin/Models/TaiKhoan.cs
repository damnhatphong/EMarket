using System;
using System.Collections.Generic;

namespace EMarket.Areas.Admin.Models
{
    public partial class TaiKhoan
    {
        public int TaiKhoanId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime NgayDk { get; set; }
        public bool LoaiTaiKhoan { get; set; }
        public int ThongTinTaiKhoanId { get; set; }

        public ThongTinTaiKhoan ThongTinTaiKhoan { get; set; }
    }
}
