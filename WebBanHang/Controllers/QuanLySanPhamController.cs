using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        private WebBanHangDbContext db = new WebBanHangDbContext();

        // GET: QuanLySanPham
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.LoaiSanPham).Include(s => s.NhaCungCap).Include(s => s.NhaSanXuat);
            return View(sanPhams.ToList());
        }

        // GET: QuanLySanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: QuanLySanPham/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoai");
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX");
            return View();
        }

        // POST: QuanLySanPham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "MaSP,TenSP,DonGia,NgayCapNhat,CauHinh,MoTa,HinhAnh,SoLuongTon,LuotXem,LuotBinhChon,SoLanMua,Moi,MaNCC,MaNSX,MaLoaiSP,DaXoa,HinhAnh1,HinhAnh2,HinhAnh3")] SanPham sanPham,
                HttpPostedFileBase[] fileUpload)
        {

            for (int i = 0; i < fileUpload.Length; i++)
            {

                if (fileUpload[i] != null)
                {
                    var fileName = System.IO.Path.GetFileName(fileUpload[i].FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/Content/AnhSanPham"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.upload = "file đã tồn tại";
                    }
                    else
                    {
                        fileUpload[i].SaveAs(path);

                    }
                    if (i == 0)
                    {
                        sanPham.HinhAnh = fileName;
                    }
                    if (i == 1)
                    {
                        sanPham.HinhAnh1 = fileName;
                    }

                    if (i == 2)
                    {
                        sanPham.HinhAnh2 = fileName;
                    }
                    if (i == 3)
                    {
                        sanPham.HinhAnh3 = fileName;
                    }


                }
            }
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX", sanPham.MaNSX);
            return View(sanPham);
        }

        // GET: QuanLySanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX", sanPham.MaNSX);
            return View(sanPham);
        }

        // POST: QuanLySanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,TenSP,DonGia,NgayCapNhat,CauHinh,MoTa,HinhAnh,SoLuongTon,LuotXem,LuotBinhChon,SoLanMua,Moi,MaNCC,MaNSX,MaLoaiSP,DaXoa,HinhAnh1,HinhAnh2,HinhAnh3")] SanPham sanPham/*, HttpPostedFileBase[] fileUpload*/)
        {

            //for (int i = 0; i < fileUpload.Length; i++)
            //{

            //    if (fileUpload[i] != null)
            //    {
            //        var fileName = System.IO.Path.GetFileName(fileUpload[i].FileName);
            //        var path = System.IO.Path.Combine(Server.MapPath("~/Content/AnhSanPham"), fileName);
            //        if (System.IO.File.Exists(path))
            //        {
            //            ViewBag.upload = "file đã tồn tại";
            //            if (i == 0)
            //            {
            //                sanPham.HinhAnh = fileName;
            //            }
            //            if (i == 1)
            //            {
            //                sanPham.HinhAnh1 = fileName;
            //            }

            //            if (i == 2)
            //            {
            //                sanPham.HinhAnh2 = fileName;
            //            }
            //            if (i == 3)
            //            {
            //                sanPham.HinhAnh3 = fileName;
            //            }
            //        }
            //        else
            //        {
            //            fileUpload[i].SaveAs(path);
            //            if (i == 0)
            //            {
            //                sanPham.HinhAnh = fileName;
            //            }
            //            if (i == 1)
            //            {
            //                sanPham.HinhAnh1 = fileName;
            //            }

            //            if (i == 2)
            //            {
            //                sanPham.HinhAnh2 = fileName;
            //            }
            //            if (i == 3)
            //            {
            //                sanPham.HinhAnh3 = fileName;
            //            }
            //        }


            //    }
            //}
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams, "MaLoaiSP", "TenLoai", sanPham.MaLoaiSP);
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps, "MaNCC", "TenNCC", sanPham.MaNCC);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX", sanPham.MaNSX);
            return View(sanPham);
        }

        // GET: QuanLySanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: QuanLySanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
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
