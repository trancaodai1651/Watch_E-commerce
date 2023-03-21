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
    public class UserController : Controller
    {
        // GET: User
        dbWatchDataContext data = new dbWatchDataContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Register(FormCollection collection, KHACHHANG kh, TAIKHOAN tk)
        { 
            //gan gia tri vao form
            var TenKhachHang = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];
            var nhaplaimatkhau = collection["NhaplaimatKhau"];
            var email = collection["Email"];
            var diachi = collection["DiaChi"];
            var sdt = collection["DienThoai"];
            if (string.IsNullOrEmpty(TenKhachHang) || string.IsNullOrEmpty(tendn) || string.IsNullOrEmpty(matkhau) || string.IsNullOrEmpty(nhaplaimatkhau) || string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(sdt))
            {
                ViewData["Error"] = "Vui lòng nhập đầy đủ thông tin";
            }
            else
            {
                //Them Tai Khoan
                tk.MaTaiKhoan = tendn;
                tk.MatKhau = matkhau;
                tk.Email = email;
                tk.IDVaiTro = 2;
                data.TAIKHOANs.InsertOnSubmit(tk);
                data.SubmitChanges();

                //Them Khach Hang
                kh.TenKhachHang = TenKhachHang;
                kh.DiachiKH = diachi;
                kh.DienThoaiKH = sdt;
                kh.IDTaiKhoanKH = tendn;
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("Login");
            }
            return this.Register();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {

            var tendn = collection["TenDN"];
            var matkhau = collection["MatKhau"];

            if (string.IsNullOrEmpty(tendn) || string.IsNullOrEmpty(matkhau))
            {
                ViewData["Error"] = "Vui lòng nhập đầy đủ thông tin";
            }
            else
            {
                TAIKHOAN tk = data.TAIKHOANs.SingleOrDefault(n => n.MaTaiKhoan == tendn && n.MatKhau == matkhau);
                if (tk != null)
                {
                   if(tk.IDVaiTro == 1)
                    {
                        QUANTRI qt = data.QUANTRIs.SingleOrDefault(p => p.IDTaiKhoanQT == tk.MaTaiKhoan);
                        Session["QuanTri"] = qt;
                        return RedirectToAction("Statictical", "Product", new { area = "Admin" });
                    }
                   else
                    {
                        KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(p => p.IDTaiKhoanKH == tk.MaTaiKhoan);
                        Session["Taikhoan"] = kh;
                        return RedirectToAction("Index", "Watch");
                    }
                }
                else
                {
                    ViewBag.ThongBao = "Sai tên đăng nhập hoặc mật khẩu";
                }

            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["Taikhoan"] = null;
            return RedirectToAction("Index", "Watch", new { area = "" });
        }

        public ActionResult ForgetPassword(string email = "")
        {
            if (email != "")
            {
                var account = data.TAIKHOANs.FirstOrDefault(p => p.Email == email);
                
                if (account != null)
                {
                    Session["TaiKhoanQuenMatKhau"] = account;
                    /*Gửi Mail*/
                    string emailShop = "thebees.hutech@gmail.com";
                    string passWordShop = "boksnmojcgaaskeh";
                    string emailKhachHang = account.Email;
                    MailMessage mailMessage = new MailMessage(emailShop, emailKhachHang);
                    Random r = new Random();
                    int maXacNhan = r.Next(10000, 99999);
                    mailMessage.Subject = "[THEBEES_HUTECH_SHOP] Xác nhận tài khoản";
                    mailMessage.Body = "Mã xác nhận tài khoản là: " + maXacNhan;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        System.Net.NetworkCredential nc = new NetworkCredential(emailShop, passWordShop);
                        smtp.Credentials = nc;
                        smtp.EnableSsl = true;
                        smtp.Send(mailMessage);
                    }
                    /**/
                    Session["Code"] = maXacNhan;
                    return RedirectToAction("ConfirmCode", "User");
                }
                else
                {
                    ViewBag.Error = "Email không tồn tại";
                }
            }
            return View();
        }

        public ActionResult ConfirmCode(int? confirmCode)
        {
            TAIKHOAN account = Session["TaiKhoanQuenMatKhau"] as TAIKHOAN;
            if (account != null)
            {
                int code = (int) Session["Code"];
                if (confirmCode != null)
                {
                    if (confirmCode == code)
                    {
                        return RedirectToAction("ChangeNewPassWord", "User");
                    }
                    else
                    {
                        ViewBag.MaXacNhan = "Mã xác nhận không hợp lệ";
                    }
                }
            }
            else
            {
                return RedirectToAction("ForgetPassword", "User");
            }
            return View();
        }

        public ActionResult ChangeNewPassWord(string newPassword = "", string newPassword1 = "")
        {
            TAIKHOAN account = Session["TaiKhoanQuenMatKhau"] as TAIKHOAN;
            if (account != null)
            {
                if(newPassword != "" && newPassword1 != "")
                {
                    if(newPassword == newPassword1)
                    {
                        TAIKHOAN tk = data.TAIKHOANs.FirstOrDefault(p => p.MaTaiKhoan == account.MaTaiKhoan);
                        tk.MatKhau = newPassword;
                        UpdateModel(account);
                        data.SubmitChanges();
                        Session["TaiKhoanQuenMatKhau"] = null;
                        Session["Code"] = null;
                        return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        ViewBag.Error = "Mật khẩu không khớp. Vui lòng nhập lại !";
                    }
                }
            }
            else
            {
                return RedirectToAction("ForgetPassword", "User");
            }

            return View();
        }

        public ActionResult NotAuthorize()
        {
            return View();
        }
    }
}