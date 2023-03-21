using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOANLAPTRINHWEB.Models
{
    public class Cart
    {
        dbWatchDataContext data = new dbWatchDataContext();
        public int id { get; set; }
        public int idDay { get; set; }

        public string ten { get; set; }

        public string hinh { get; set; }

        public double gia { get; set; }

        public int iSoluong { get; set; }

        public Double dThanhtien
        {
            get { return iSoluong * gia; }
        }
        public Cart(int id)
        {
            this.id = id;
            DONGHO dongho = data.DONGHOs.SingleOrDefault(n => n.MaDongHo == id);
            ten = dongho.TenDongHo;
            hinh = dongho.HinhDongHo;
            gia = double.Parse(dongho.GiaBan.ToString());
            iSoluong = 1;
        }
    }
}