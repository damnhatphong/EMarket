using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMarket.Areas.Admin.Models
{
    public partial class TaiKhoan
    {
        public TaiKhoan() {
            ThongTinTaiKhoan = new HashSet<ThongTinTaiKhoan>();
        }
        [Display(Name="Mã Tài Khoản")]
        public int TaiKhoanId { get; set; }
       
        [Display(Name ="Tên Tài Khoản")]
        [Required]
        public string UserName { get; set; }

        [Display(Name = "Mật Khẩu")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Ngày Đăng Ký")]
        public DateTime NgayDk { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Display(Name ="Là Khách Hàngs")]
        [Required]
        public bool LoaiTaiKhoan { get; set; }

        public ICollection<ThongTinTaiKhoan> ThongTinTaiKhoan { get; set; }
    }
}
