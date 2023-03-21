using DOANLAPTRINHWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace DOANLAPTRINHWEB.Areas.Admin.Controllers
{
    internal class CustomAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            dbWatchDataContext data = new dbWatchDataContext();
            TAIKHOAN db = new TAIKHOAN();
            KHACHHANG kh = (KHACHHANG)HttpContext.Current.Session["Taikhoan"];
            QUANTRI qt = (QUANTRI)HttpContext.Current.Session["QuanTri"];
            
            if (kh == null && qt == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "Login", area = "" }));
            }
            else
            {
                TAIKHOAN tk;
                if (kh != null)
                {
                    tk = data.TAIKHOANs.FirstOrDefault(p => p.MaTaiKhoan == kh.IDTaiKhoanKH);
                }
                else
                {
                    tk = data.TAIKHOANs.FirstOrDefault(p => p.MaTaiKhoan == qt.IDTaiKhoanQT);
                }
                var hasRole = tk.IDVaiTro;
                if (hasRole > Order)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "User", action = "NotAuthorize", area = "" }));
                }
            }
        }
    }
}