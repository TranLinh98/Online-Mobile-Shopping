using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
namespace WebBanHang.Controllers
{
    public class ThongKeController : Controller
    {
        WebBanHangDbContext db = new WebBanHangDbContext();
        // GET: ThongKe
        public ActionResult Index()
        {
            //lấy số người truy cập vào website
            ViewBag.PeopleVisits = (int)HttpContext.Application["PeopleVisits"];
            //số lượng người đang online
            ViewBag.PeopleOnlines = (int)HttpContext.Application["PeopleOnlines"];
            ViewBag.TongDoanhThu = ThongKeDoanhThu();
            ViewBag.User = ThongKeUser();
            ViewBag.Order = ThongKeOrder();
            ViewBag.Customer = ThongKeKhachHang();
            return View();
        }
        public decimal ThongKeDoanhThu()
        {
            decimal TongDoanhThu = Decimal.Parse( db.ChiTietDonDatHangs.Sum(n => n.SoLuong * n.Gia).Value.ToString());
            return TongDoanhThu;
        }
        public decimal ThongKeDoanhThuThang(int month,int year) 
        {
            var listDDH = db.DonDatHangs.Where(n => n.NgatDat.Value.Month == month && n.NgatDat.Value.Year == year);
            decimal TongDoanhThuThang = 0; 
            foreach(var item in listDDH)
            {
                TongDoanhThuThang+=Decimal.Parse(item.ChiTietDonDatHangs.Sum(n => n.SoLuong * n.Gia).Value.ToString());
            }
            return TongDoanhThuThang;
        }
        public double ThongKeOrder()
        {           
            if (db.DonDatHangs.Count() > 0)
            {
                 return db.DonDatHangs.Count();
            }
            return 0;
        }
        public double ThongKeUser()
        {
            if (db.ThanhViens.Count() > 0)
            {
                return db.ThanhViens.Count();
            }
            return 0;
        }
        public double ThongKeKhachHang()
        {
            if (db.KhachHangs.Count() > 0)
            {
                return db.KhachHangs.Count();
            }
            return 0;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}