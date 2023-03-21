﻿using DOANLAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANLAPTRINHWEB.Areas.Admin.Controllers
{
    [CustomAuthorization(Order = 1)]
    public class CustomerController : Controller
    {
        
        dbWatchDataContext data = new dbWatchDataContext();
        public ActionResult List()
        {
            var L_Admin = from r in data.KHACHHANGs select r;
            return View(L_Admin);
        }
        public ActionResult Update(int id)
        {
            var MaKhachHang = data.KHACHHANGs.First(m => m.MaKhachHang == id);
            return View(MaKhachHang);
        }
        [HttpPost]
        public ActionResult Update(int id, FormCollection collection)
        {
            var Customer = data.KHACHHANGs.First(m => m.MaKhachHang == id);
            var TaiKhoan = data.TAIKHOANs.First(m => m.MaTaiKhoan == Customer.IDTaiKhoanKH);
            //gan gia tri vao form
            var TenKhachHang = collection["TenKhachHang"];
            var DiachiKH = collection["DiachiKH"];
            var DienThoaiKH = collection["DienThoaiKH"];
            var MatKhau = collection["TAIKHOAN.MatKhau"];
            var Email = collection["TAIKHOAN.Email"];

            TaiKhoan.MatKhau = MatKhau.ToString();
            TaiKhoan.Email = Email.ToString();
            Customer.TenKhachHang = TenKhachHang.ToString();
            Customer.DiachiKH = DiachiKH.ToString();
            Customer.DienThoaiKH = DienThoaiKH.ToString();
            UpdateModel(TaiKhoan);
            UpdateModel(Customer);
            data.SubmitChanges();
            return RedirectToAction("List", "Customer");
        }
        public ActionResult Delete(int id)
        {
            var MaKhachHang = data.KHACHHANGs.First(m => m.MaKhachHang == id);
            return View(MaKhachHang);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var MaKhachHang = data.KHACHHANGs.First(m => m.MaKhachHang == id);
            data.KHACHHANGs.DeleteOnSubmit(MaKhachHang);
            data.SubmitChanges();
            return RedirectToAction("List", "Customer");
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
            if (string.IsNullOrEmpty(TenKhachHang) || string.IsNullOrEmpty(tendn) || string.IsNullOrEmpty(matkhau) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(sdt))
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
                return RedirectToAction("List", "Customer");
            }
            return this.Register();
        }
    }
}