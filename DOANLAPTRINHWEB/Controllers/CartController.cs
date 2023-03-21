using DOANLAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace DOANLAPTRINHWEB.Controllers
{
    public class CartController : Controller
    {
        dbWatchDataContext  data = new dbWatchDataContext();
        public List<Cart> Laygiohang()
        {
            List<Cart> lstGiohang = Session["ViewCart"] as List<Cart>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Cart>();
                Session["ViewCart"] = lstGiohang;
            }
            return lstGiohang;
        }
        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<Cart> lstGiohang = Laygiohang();
            Cart sanpham = lstGiohang.Find(n => n.id == id);
            if (sanpham == null)
            {
                sanpham = new Cart(id);
                lstGiohang.Add(sanpham);
                Session["CountProductCart"] = TongSoLuong();
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong ++;
                Session["CountProductCart"] = TongSoLuong();
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<Cart> lstGiohang = Session["ViewCart"] as List<Cart>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(n => n.iSoluong);
            }
            return tsl;
        }
        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<Cart> lstGiohang = Session["ViewCart"] as List<Cart>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }
        private double TongTien()
        {
            double tt = 0;
            List<Cart> lstGiohang = Session["ViewCart"] as List<Cart>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(n => n.dThanhtien);
            }
            return tt;
        }
        public ActionResult ViewCarts()
        {
            List<Cart> lstGiohang = Laygiohang();
            ViewBag.ViewCarts = lstGiohang.Count;
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);

        }
        public ActionResult GioHangPartial()
        {
            TempData["Tongsoluong"] = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }
        public ActionResult XoaGiohang(int id)
        {
            List<Cart> lstGiohang = Laygiohang();
            
            Cart sanpham = lstGiohang.SingleOrDefault(n => n.id == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.id == id);
                Session["CountProductCart"] = TongSoLuong();
                return RedirectToAction("ViewCarts");
            }
            return RedirectToAction("ViewCarts");
        }
        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            List<Cart> lstGiohang = Laygiohang();
            Cart sanpham = lstGiohang.SingleOrDefault(n => n.id == id);
            if (sanpham != null)
            {
                
                sanpham.iSoluong = int.Parse(collection["txtSoLg"].ToString());
                Session["CountProductCart"] = TongSoLuong();
            }
            return RedirectToAction("ViewCarts");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<Cart> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            Session["CountProductCart"] = TongSoLuong();
            return RedirectToAction("ViewCarts");
        }
        [HttpGet]
        public ActionResult Dathang()
        {
            // kiem tra dang nhap
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            if (Session["ViewCart"] == null)
            {
                return RedirectToAction("Index", "Watch");
            }
            List<Cart> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Watch");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult Dathang(FormCollection collection)
        {
            DONGHO sp = new DONGHO();
            HOADON ddh = new HOADON();
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            List<Cart> lstGiohang = Laygiohang();
            ddh.IDKhachHang = kh.MaKhachHang;
            ddh.NgayLap = DateTime.Now;
            ddh.TinhTrang = 1;
            ddh.TongTien = TongTien();
            data.HOADONs.InsertOnSubmit(ddh);
            data.SubmitChanges();

            //Them chi tiet don hang
            foreach (var item in lstGiohang)
            {
                CTHOADONBANDONGHO ctdh = new CTHOADONBANDONGHO();
                ctdh.IDHoaDon = ddh.MaHoaDon;
                ctdh.IDDongHo = item.id;
                ctdh.SoLuong = item.iSoluong;
                ctdh.ThanhTien = (float)item.dThanhtien;

                sp = data.DONGHOs.FirstOrDefault(n => n.MaDongHo == item.id);
                sp.SLTon -= item.iSoluong;
                data.SubmitChanges();
                data.CTHOADONBANDONGHOs.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();

            /*Gửi Mail*/
            string emailShop = "thebees.hutech@gmail.com";
            string passWordShop = "boksnmojcgaaskeh";
            var account = data.TAIKHOANs.FirstOrDefault(p => p.MaTaiKhoan == kh.IDTaiKhoanKH);
            string emailKhachHang = account.Email;
            MailMessage mailMessage = new MailMessage(emailShop, emailKhachHang);

            string ctDonHang = "Tên sản phẩm   Số lượng   Thành tiền\n";
            foreach(var item in lstGiohang)
            {
                ctDonHang += $"{item.ten}\t\t{item.iSoluong}\t\t{item.dThanhtien}\n";
            }
            mailMessage.Subject = "[THEBEES_HUTECH_SHOP] Thông báo đơn hàng";
            mailMessage.Body = $"CẢM ƠN BẠN ĐÃ ĐẶT HÀNG, CHÚNG TÔI SẼ GỬI ĐƠN HÀNG CHO BẠN SỚM NHẤT! \n\n " +
                $"Mã hóa đơn: {ddh.MaHoaDon}\n Ngày đặt: {ddh.NgayLap}\n Số điện thoại: {kh.DienThoaiKH} \n Giao tới: {kh.DiachiKH}\n Tổng tiền: {ddh.TongTien}\n\n" +
                 "-----------------------------------------------\n" +
                "CHI TIẾT ĐƠN HÀNG\n" +
                $"{ctDonHang}" +
                "------------------------------------------------\n" +
                "CẢM ƠN!";
            
            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                System.Net.NetworkCredential nc = new NetworkCredential(emailShop, passWordShop);
                smtp.Credentials = nc;
                smtp.EnableSsl = true;
                smtp.Send(mailMessage);
            }
            /**/
            Session["ViewCart"] = null;
            return RedirectToAction("DatHangThanhCong", "Cart");
        }
        public ActionResult DatHangThanhCong()
        {
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        
    }
}