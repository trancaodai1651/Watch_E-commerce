using DOANLAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANLAPTRINHWEB.Areas.Admin.Controllers
{
    [CustomAuthorization(Order = 1)]
    public class ProductController : Controller
    {
        dbWatchDataContext data = new dbWatchDataContext();
        // GET: Admin
        public ActionResult List()
        {
            var L_Watch = from r in data.DONGHOs select r;
            return View(L_Watch);
        }
        public ActionResult Add()
        {
            List<THUONGHIEU> thuonghieu = data.THUONGHIEUs.ToList();
            ViewBag.ThuongHieu = thuonghieu.Select(p => new SelectListItem { Text = p.TenThuongHieu, Value = p.MaThuongHieu.ToString() }).ToList();
            List<LOAIDONGHO> loaidongho = data.LOAIDONGHOs.ToList();
            ViewBag.LoaiDongHo = loaidongho.Select(p => new SelectListItem { Text = p.TenLoaiDongHo, Value = p.MaLoaiDongHo.ToString() }).ToList();
            List<LOAIDAY> loaiday = data.LOAIDAYs.ToList();
            ViewBag.LoaiDay = loaiday.Select(p => new SelectListItem { Text = p.TenLoaiDay, Value = p.MaLoaiDay.ToString() }).ToList();
            List<CHATLIEUMATKINH> chatlieu = data.CHATLIEUMATKINHs.ToList();
            ViewBag.ChatLieu = chatlieu.Select(p => new SelectListItem { Text = p.TenChatLieuMatKinh, Value = p.MaChatLieuMatKinh.ToString() }).ToList();
            List<HINHDANG> hinhdang = data.HINHDANGs.ToList();
            ViewBag.HinhDang = hinhdang.Select(p => new SelectListItem { Text = p.TenHinhDang, Value = p.MaHinhDang.ToString() }).ToList();
            List<KICHTHUOC> kichthuoc = data.KICHTHUOCs.ToList();
            ViewBag.KichThuoc = kichthuoc.Select(p => new SelectListItem { Text = p.TenKichThuoc, Value = p.MaKichThuoc.ToString() }).ToList();
            List<MUCDOCHONGNUOC> mucdochongnuoc = data.MUCDOCHONGNUOCs.ToList();
            ViewBag.MucDoChongNuoc = mucdochongnuoc.Select(p => new SelectListItem { Text = p.TenMucDoChongNuoc, Value = p.MaMucDoChongNuoc.ToString() }).ToList();
            List<GIOITINH> gioitinh = data.GIOITINHs.ToList();
            ViewBag.GioiTinh = gioitinh.Select(p => new SelectListItem { Text = p.TenGioiTinh, Value = p.MaGioiTinh.ToString() }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(FormCollection collection, DONGHO h, HttpPostedFileBase HinhDongHo)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(HinhDongHo.FileName);
                var path = Path.Combine(Server.MapPath("/Content/images"), fileName);
                HinhDongHo.SaveAs(path);


                var TenDongHo = collection["TenDongHo"];
                var MoTa = collection["MoTa"];
                var GioiTinh = Convert.ToInt32(collection["IDGioiTinh"]);
                var ThoiGianBaoHanh = collection["ThoiGianBaoHanh"];
                var LoaiMay = collection["LoaiMay"];
                var GiaNhap = Convert.ToDouble(collection["GiaNhap"]);
                var GiaBan = Convert.ToDouble(collection["GiaBan"]);
                var IDThuongHieu = Convert.ToInt32(collection["IDThuongHieu"]);
                var IDLoaiDongHo = Convert.ToInt32(collection["IDLoaiDongHo"]);
                var IDLoaiDay = Convert.ToInt32(collection["IDLoaiDay"]);
                var IDChatLieuMatKinh = Convert.ToInt32(collection["IDChatLieuMatKinh"]);
                var IDHinhDangMatSo = Convert.ToInt32(collection["IDHinhDangMatSo"]);
                var IDKichThuocMatSo = Convert.ToInt32(collection["IDKichThuocMatSo"]);
                var IDMucDoChongNuoc = Convert.ToInt32(collection["IDMucDoChongNuoc"]);
                var SoLuongTon = Convert.ToInt32(collection["SoLuongTon"]);

                h.TenDongHo = TenDongHo.ToString();
                h.MoTa = MoTa.ToString();
                h.IDGioiTinh = GioiTinh;
                h.ThoiGianBaoHanh = ThoiGianBaoHanh.ToString();
                h.LoaiMay = LoaiMay.ToString();
                h.GiaNhap = GiaNhap;
                h.GiaBan = GiaBan;
                h.HinhDongHo = "/Content/images/" + fileName;
                h.IDThuongHieu = IDThuongHieu;
                h.IDLoaiDongHo = IDLoaiDongHo;
                h.IDLoaiDay = IDLoaiDay;
                h.IDChatLieuMatKinh = IDChatLieuMatKinh;
                h.IDHinhDangMatSo = IDHinhDangMatSo;
                h.IDKichThuocMatSo = IDKichThuocMatSo;
                h.IDMucDoChongNuoc = IDMucDoChongNuoc;
                h.SLTon = SoLuongTon;
                data.DONGHOs.InsertOnSubmit(h);
                data.SubmitChanges();
                return RedirectToAction("List", "Product");
            }
            else
            {
                return View(h);

            }
        }
        public ActionResult Update(int id)
        {
            var MaDongHo = data.DONGHOs.First(m => m.MaDongHo == id);
            List<THUONGHIEU> thuonghieu = data.THUONGHIEUs.ToList();
            ViewBag.ThuongHieu = thuonghieu.Select(p => new SelectListItem { Text = p.TenThuongHieu, Value = p.MaThuongHieu.ToString() }).ToList();
            List<LOAIDONGHO> loaidongho = data.LOAIDONGHOs.ToList();
            ViewBag.LoaiDongHo = loaidongho.Select(p => new SelectListItem { Text = p.TenLoaiDongHo, Value = p.MaLoaiDongHo.ToString() }).ToList();
            List<LOAIDAY> loaiday = data.LOAIDAYs.ToList();
            ViewBag.LoaiDay = loaiday.Select(p => new SelectListItem { Text = p.TenLoaiDay, Value = p.MaLoaiDay.ToString() }).ToList();
            List<CHATLIEUMATKINH> chatlieu = data.CHATLIEUMATKINHs.ToList();
            ViewBag.ChatLieu = chatlieu.Select(p => new SelectListItem { Text = p.TenChatLieuMatKinh, Value = p.MaChatLieuMatKinh.ToString() }).ToList();
            List<HINHDANG> hinhdang = data.HINHDANGs.ToList();
            ViewBag.HinhDang = hinhdang.Select(p => new SelectListItem { Text = p.TenHinhDang, Value = p.MaHinhDang.ToString() }).ToList();
            List<KICHTHUOC> kichthuoc = data.KICHTHUOCs.ToList();
            ViewBag.KichThuoc = kichthuoc.Select(p => new SelectListItem { Text = p.TenKichThuoc, Value = p.MaKichThuoc.ToString() }).ToList();
            List<MUCDOCHONGNUOC> mucdochongnuoc = data.MUCDOCHONGNUOCs.ToList();
            ViewBag.MucDoChongNuoc = mucdochongnuoc.Select(p => new SelectListItem { Text = p.TenMucDoChongNuoc, Value = p.MaMucDoChongNuoc.ToString() }).ToList();
            List<GIOITINH> gioitinh = data.GIOITINHs.ToList();
            ViewBag.GioiTinh = gioitinh.Select(p => new SelectListItem { Text = p.TenGioiTinh, Value = p.MaGioiTinh.ToString() }).ToList();
            return View(MaDongHo);
        }
        [HttpPost]
        public ActionResult Update(int id, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var MaDongHo = data.DONGHOs.First(m => m.MaDongHo == id);
                var TenDongHo = collection["TenDongHo"];
                var MoTa = collection["MoTa"];
                var GioiTinh = Convert.ToInt32(collection["GioiTinh"]);
                var HinhDongHo = collection["HinhDongHo"];
                var ThoiGianBaoHanh = collection["ThoiGianBaoHanh"];
                var LoaiMay = collection["LoaiMay"];
                var GiaNhap = Convert.ToDouble(collection["GiaNhap"]);
                var GiaBan = Convert.ToDouble(collection["GiaBan"]);
                var IDThuongHieu = Convert.ToInt32(collection["IDThuongHieu"]);
                var IDLoaiDongHo = Convert.ToInt32(collection["IDLoaiDongHo"]);
                var IDLoaiDay = Convert.ToInt32(collection["IDLoaiDay"]);
                var IDChatLieuMatKinh = Convert.ToInt32(collection["IDChatLieuMatKinh"]);
                var IDHinhDangMatSo = Convert.ToInt32(collection["IDHinhDangMatSo"]);
                var IDKichThuocMatSo = Convert.ToInt32(collection["IDKichThuocMatSo"]);
                var IDMucDoChongNuoc = Convert.ToInt32(collection["IDMucDoChongNuoc"]);
                var SoLuongTon = Convert.ToInt32(collection["SoLuongTon"]);

                MaDongHo.TenDongHo = TenDongHo.ToString();
                MaDongHo.MoTa = MoTa.ToString();
                MaDongHo.IDGioiTinh = GioiTinh;
                MaDongHo.ThoiGianBaoHanh = ThoiGianBaoHanh.ToString();
                MaDongHo.LoaiMay = LoaiMay.ToString();
                MaDongHo.GiaNhap = GiaNhap;
                MaDongHo.GiaBan = GiaBan;
                MaDongHo.HinhDongHo = HinhDongHo.ToString();
                MaDongHo.IDThuongHieu = IDThuongHieu;
                MaDongHo.IDLoaiDongHo = IDLoaiDongHo;
                MaDongHo.IDLoaiDay = IDLoaiDay;
                MaDongHo.IDChatLieuMatKinh = IDChatLieuMatKinh;
                MaDongHo.IDHinhDangMatSo = IDHinhDangMatSo;
                MaDongHo.IDKichThuocMatSo = IDKichThuocMatSo;
                MaDongHo.IDMucDoChongNuoc = IDMucDoChongNuoc;
                MaDongHo.SLTon = SoLuongTon;
                UpdateModel(MaDongHo);
                data.SubmitChanges();
            }
            return RedirectToAction("List", "Product");
        }

        public ActionResult Statictical()
        {
            /*Dong ho ban chay nhat thang*/
            var result = from ctdh in data.CTHOADONBANDONGHOs
                         from sp in data.DONGHOs
                         from dh in data.HOADONs
                         where ctdh.IDDongHo == sp.MaDongHo
                            && dh.NgayLap.Value.Month == DateTime.Now.Month
                            && dh.NgayLap.Value.Year == DateTime.Now.Year
                         group ctdh by ctdh.IDDongHo into g
                         select new DongHoBanChay
                         {
                             IDDongHo = g.FirstOrDefault().IDDongHo.Value,
                             SoLuongBan = g.Sum(p => p.SoLuong).Value
                         };

            
            if(result.Count() > 0)
            {
                var maSP = result.OrderByDescending(p => p.SoLuongBan).Select(p => p.IDDongHo).FirstOrDefault();
                var tenSanPham = data.DONGHOs.FirstOrDefault(p => p.MaDongHo == maSP).TenDongHo;
                ViewBag.SanPhamBanChayNhat = tenSanPham;
            }
            else
            {
                ViewBag.SanPhamBanChayNhat = "Không có";
            }

            /*Khach hang mua nhieu nhat thang*/
            var result1 = from dh in data.HOADONs
                         from kh in data.KHACHHANGs
                         where dh.IDKhachHang == kh.MaKhachHang
                             && dh.NgayLap.Value.Month == DateTime.Now.Month
                             && dh.NgayLap.Value.Year == DateTime.Now.Year
                         group dh by dh.IDKhachHang into g
                         select new KhachHangMuaNhieuNhat
                         {
                             IDKhachHang = g.FirstOrDefault().IDKhachHang.Value,
                             TongTienMua = g.Sum(p => p.TongTien).Value
                         };

            if(result1.Count() > 0)
            {
                var maKH = result1.OrderByDescending(p => p.TongTienMua).Select(p => p.IDKhachHang).FirstOrDefault();
                var tenKhachHang = data.KHACHHANGs.FirstOrDefault(p => p.MaKhachHang == maKH).TenKhachHang;
                ViewBag.KhachHangMuaNhieuNhat = tenKhachHang;
            }
            else
            {
                ViewBag.KhachHangMuaNhieuNhat = "Không có";
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var MaDongHo = data.DONGHOs.First(m => m.MaDongHo == id);
            return View(MaDongHo);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var MaDongHo = data.DONGHOs.First(m => m.MaDongHo == id);
            data.DONGHOs.DeleteOnSubmit(MaDongHo);
            data.SubmitChanges();
            return RedirectToAction("List", "Product");
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
    }
}