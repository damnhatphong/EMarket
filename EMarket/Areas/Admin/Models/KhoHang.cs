using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMarket.Areas.Admin.Models
{
    public partial class KhoHang
    {
        public int KhoHangID { get; set; }
        public int SoLuong { get; set; }

        public int HangHoaID { get; set; }
        HangHoa HangHoa { get; set; }
    }
}
