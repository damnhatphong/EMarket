using System;
using System.Collections.Generic;

namespace EMarket.Areas.Admin.Models
{
    public partial class ThongTinTaiKhoan
    {
        public ThongTinTaiKhoan()
        {
            TaiKhoan = new HashSet<TaiKhoan>();
        }

        public int ThongTinTaiKhoanId { get; set; }
        public string HoVaTen { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string Sdt { get; set; }
        public string DiaChi { get; set; }

        public ICollection<TaiKhoan> TaiKhoan { get; set; }
    }
}
