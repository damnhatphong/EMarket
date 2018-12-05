using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EMarket.Areas.Admin.Models
{
    public partial class HangHoa
    {
        public HangHoa()
        {
            TopSelling = new HashSet<TopSelling>();
            KhoHang = new HashSet<KhoHang>();
        }

        [Display(Name = "Mã Hàng Hóa")]
        [Required]
        public int HangHoaId { get; set; }
        [Display(Name ="Tên Hàng Hóa")]
        [Required]
        public string TenHangHoa { get; set; }
        [Display(Name ="Nhà Cung Cấp")]  
        public int NhaCungCapId { get; set; }
        [Display(Name = "Loại")]
        public int LoaiId { get; set; }
        [Display(Name = "GIá")]
        [Required]
        public double Gia { get; set; }
        [Display(Name = "Hình")]
        [FileExtensions(Extensions = "jpg,png,jpeg")]
        [Required]
        public string Hinh { get; set; }
        [Display(Name = "Mô Tả")]
        public string MoTa { get; set; }

       
        public Loai Loai { get; set; }
        public NhaCungCap NhaCungCap { get; set; }

        public ICollection<TopSelling> TopSelling { get; set; }
        public ICollection<KhoHang> KhoHang { get; set; }

        internal static IQueryable<HangHoa> AsNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}
