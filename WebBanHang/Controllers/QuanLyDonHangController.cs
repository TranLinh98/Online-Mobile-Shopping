using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Net;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        private WebBanHangDbContext db = new WebBanHangDbContext();
        // GET: QuanLyDonHang
        public ActionResult DonHangChuaThanhToan()
        {
            var listctt = db.DonDatHangs.Where(n => n.DaThanhToan == false).OrderBy(n => n.NgatDat);
            return View(listctt);
        }
        public ActionResult DonHangChuaGiao()
        {
            var listcg = db.DonDatHangs.Where(n => n.TinhTrang == false).OrderBy(n => n.NgatDat);
            return View(listcg);
        }
        public ActionResult DonHangHoanThanh()
        {
            var listht = db.DonDatHangs.Where(n => n.TinhTrang == true && n.DaThanhToan == true).OrderBy(n => n.NgatDat);
            return View(listht);
        }
        [HttpGet]
        public ActionResult DuyetDonHang(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang donDatHang = db.DonDatHangs.Find(id);
            if (donDatHang == null)
            {
                return HttpNotFound();
            }
            var listChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDDH == id);
            ViewBag.listChiTietDH = listChiTietDH;
            return View(donDatHang);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DuyetDonHang([Bind(Include = "MaDDH,NgatDat,TinhTrang,NgayGiao,DaThanhToan,DaHuy,UuDai,MaKH")] DonDatHang donDatHang)
        {
            DonDatHang ddhupdate = db.DonDatHangs.Single(n => n.MaDDH == donDatHang.MaDDH);
            if (ModelState.IsValid)
            {
                ddhupdate.DaThanhToan = donDatHang.DaThanhToan;
                ddhupdate.TinhTrang = donDatHang.TinhTrang;
                db.SaveChanges();
                return RedirectToAction("DonHangHoanThanh");
            }                  
            var listChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDDH == donDatHang.MaDDH);
            ViewBag.listChiTietDH = listChiTietDH;
            return View(donDatHang);
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