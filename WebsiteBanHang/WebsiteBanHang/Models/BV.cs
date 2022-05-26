using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteBanHang.Models
{
    public class BV
    {
      
        public String MaBV { get; set; }
        public String TenBV { get; set; }
        public String NoiDung { get; set; }

        public String HinhDD { get; set; }

        public DateTime NgayDang { get; set; }

        public List<BV> std { get; set; }

    }
}