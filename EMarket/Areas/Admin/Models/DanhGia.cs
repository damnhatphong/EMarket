using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EMarket.Areas.Admin.Models
{
    public class DanhGia
    {
        public int Id { get; set; }
        public int TaiKhoanId { get; set; }
        public int HangHoaId { get; set; }
        [MaxLength]
        public string Comment { get; set; }
        public int Rating { get; set; }


    }
}
