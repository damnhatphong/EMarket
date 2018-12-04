using System;
using System.Collections.Generic;
using System.Linq;

namespace EMarket.Areas.Admin.Models
{
    public partial class HangHoa
    {
        public HangHoa()
        {
            TopSelling = new HashSet<TopSelling>();
        }

        public int HangHoaId { get; set; }
        public string TenHangHoa { get; set; }
        public int NhaCungCapId { get; set; }
        public int LoaiId { get; set; }
        public double Gia { get; set; }
        public string Hinh { get; set; }
        public string MoTa { get; set; }

        public Loai Loai { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
        public ICollection<TopSelling> TopSelling { get; set; }

        internal static IQueryable<HangHoa> AsNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}
