using System;
using System.Collections.Generic;

namespace EMarket.Areas.Admin.Models
{
    public partial class Loai
    {
        public Loai()
        {
            HangHoa = new HashSet<HangHoa>();
        }

        public int LoaiId { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }

        public ICollection<HangHoa> HangHoa { get; set; }
    }
}
