using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using PagedList;
namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        private WebBanHangDbContext db = new WebBanHangDbContext();
        // GET: SanPham
        public ActionResult Index()
        {
            var listSanPhamLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1 && n.DaXoa == false).OrderByDescending(n=>n.LuotXem);
            ViewBag.ListLTM = listSanPhamLTM;

            var listSanPhamDTM = db.SanPhams.Where(n => n.MaLoaiSP == 1 && n.Moi == 1 && n.DaXoa == false).OrderByDescending(n => n.LuotXem);
            ViewBag.ListDTM = listSanPhamDTM;

            var listSanPhamMTBM = db.SanPhams.Where(n => n.MaLoaiSP == 3 && n.Moi == 1 && n.DaXoa == false).OrderByDescending(n => n.LuotXem);
            ViewBag.ListMTBM = listSanPhamMTBM;
            return View();
        }
        [ChildActionOnly]
        public ActionResult MenuPartial()
        {
            var listSP = db.SanPhams;
            return PartialView(listSP);
        }
        //Load Câu Hỏi bí mật
        public List<string> LoadCauHoi()
        {
            List<string> listCauHoi = new List<string>();
            listCauHoi.Add("Con Vật mà bạn yêu thích là gì ? ");
            listCauHoi.Add("Bố bạn tên là gì ? ");
            listCauHoi.Add("Mẹ bạn tên là gì ? ");
            listCauHoi.Add("Sở thích của bạn là gì ? ");
            listCauHoi.Add("Trò chơi mà bạn yêu thích nhất là gì ? ");
            listCauHoi.Add("Bộ phim mà bạn thích nhất là gì ? ");
            listCauHoi.Add("Ca sĩ mà bạn thích nhất là gì ? ");
            return listCauHoi;
        }
        //GET đăng ký
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());

            return View();
        }
        //POST đăng ký
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "TaiKhoan, MatKhau, DiaChi,Email,SDT,CauHoi,CauTraLoi")]ThanhVien thanhVien)
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            if (ModelState.IsValid)
            {
                if (this.IsCaptchaValid("Captcha không đúng"))
                {
                    db.ThanhViens.Add(thanhVien);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        //GET đăng nhập
        public ActionResult Login()
        {
            return View();
        }
        //POST đăng nhập
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Username, string Password)
        {
            ThanhVien thanhvien = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == Username && n.MatKhau == Password);
            if (ModelState.IsValid)
            {
                if (thanhvien != null)
                {
                    Session["TaiKhoan"] = thanhvien;
                    return RedirectToAction("Index");
                }

            }
           
           
                ViewBag.ThongBao="Đăng nhập thất bại";
                
            
            return View();
        }

        //Tìm kiếm 
        public ActionResult Search(string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            var sanpham = from m in db.SanPhams
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                sanpham = sanpham.Where(s => s.TenSP.Contains(searchString));
                
            }

            //Phân trang
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.SearchString = searchString;
            return View(sanpham.OrderBy(n => n.TenSP).ToPagedList(pageNumber, pageSize));
        }

        //Đăng Xuất
        public ActionResult Logout()
        {
            Session.Remove("TaiKhoan");
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