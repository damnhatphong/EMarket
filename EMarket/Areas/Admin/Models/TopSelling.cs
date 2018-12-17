using System;
using System.Collections.Generic;

namespace EMarket.Areas.Admin.Models
{
    public partial class TopSelling
    {
        public int TopSellingId { get; set; }
        public int HangHoaId { get; set; }
        public int? SoLan { get; set; }
        public int? DanhGia { get; set; }

        public HangHoa HangHoa { get; set; }
    }
}
