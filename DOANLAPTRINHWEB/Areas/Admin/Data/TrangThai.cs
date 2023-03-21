using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANLAPTRINHWEB.Models
{
    public class TrangThai
    {
        dbWatchDataContext data = new dbWatchDataContext();
        public int MaTrangThai { get; set; }
        public string TenTrangThai { get; set; }
    }
}