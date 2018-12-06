using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EMarket.Areas.Admin.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            HangHoa = new HashSet<HangHoa>();
        }

        [Display(Name ="Mã Nhà Cung Cấp")]
        [Required]
        public int NhaCungCapId { get; set; }
        [Required]
        [Display(Name = "Tên Nhà Cung Cấp")]
        public string TenNhaCungCap { get; set; }
        [Display(Name = "Mô Tả")]
        public string MoTa { get; set; }

        public ICollection<HangHoa> HangHoa { get; set; }
    }
}
