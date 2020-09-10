using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "XemChiTiet",
            url: "{tensp}-{id}",
            defaults: new { controller = "SanPham", action = "XemChiTiet", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Login",
            url: "dangnhap",
            defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Register",
            url: "dangky",
            defaults: new { controller = "Home", action = "Register", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "Home Master",
            url: "trangchu",
            defaults: new { controller = "Home", action = "index", id = UrlParameter.Optional }
        );
            routes.MapRoute(
       name: "ViewCart",
       url: "xemgiohang",
       defaults: new { controller = "GioHang", action = "XemGioHang", id = UrlParameter.Optional }
     );
            routes.MapRoute(
           name: "Admin",
           url: "admin",
           defaults: new { controller = "Admin", action = "index", id = UrlParameter.Optional }
         );
            routes.MapRoute(
         name: "DuyetDonHang",
         url: "duyet-don-hang/{id}",
         defaults: new { controller = "QuanLyDonHang", action = "DuyetDonHang", id = UrlParameter.Optional }
       );
            routes.MapRoute(
         name: "ThongKe",
         url: "thongke",
         defaults: new { controller = "ThongKe", action = "index", id = UrlParameter.Optional }
       );
            routes.MapRoute(
     name: "PhieuNhap",
     url: "xem-phieu-nhap/{id}",
     defaults: new { controller = "PhieuNhap", action = "Details", id = UrlParameter.Optional }
   );
            routes.MapRoute(
         name: "XemPhieuNhap",
         url: "xemphieunhap",
         defaults: new { controller = "PhieuNhap", action = "index", id = UrlParameter.Optional }
       );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
