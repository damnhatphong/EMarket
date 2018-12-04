using System;
using System.Collections.Generic;

namespace EMarket.Areas.Admin.Models
{
    public partial class NhaCungCap
    {
        public NhaCungCap()
        {
            HangHoa = new HashSet<HangHoa>();
        }

        public int NhaCungCapId { get; set; }
        public string TenNhaCungCap { get; set; }
        public string MoTa { get; set; }

        public ICollection<HangHoa> HangHoa { get; set; }
    }
}
