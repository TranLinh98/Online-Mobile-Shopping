using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using PagedList;
namespace WebBanHang.Controllers
{
    public class SanPhamController : Controller
    {
        private WebBanHangDbContext db = new WebBanHangDbContext();
        // GET: SanPham
        [ChildActionOnly]
        public ActionResult SanPhamStyle1Partial()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult SanPhamStyle2Partial()
        {
            return PartialView();
        }
        //GET
        // Load chi tiết thông tin 1 sản phẩm được chọn
        public ActionResult XemChiTiet(int? id,string tensp)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanpham = db.SanPhams.SingleOrDefault(n => n.MaSP == id && n.DaXoa == false);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            return View(sanpham);
        }
        //GET
        //Load sản phẩm theo danh mục menu theo mã loại sản phẩm và mã nhà sản xuất
        public ActionResult SanPham(int? MaLoaiSP,int? MaNSX, int? page)
        {
            if (MaLoaiSP == null|| MaNSX==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var listSP = db.SanPhams.Where(m => m.MaLoaiSP == MaLoaiSP && m.MaNSX ==MaNSX);
            if (listSP.Count() == 0)
            {
                return HttpNotFound();
            }
            //Phân trang
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNSX = MaNSX;
            return View(listSP.OrderBy(n=>n.MaSP).ToPagedList(pageNumber,pageSize));
        }
    }
}