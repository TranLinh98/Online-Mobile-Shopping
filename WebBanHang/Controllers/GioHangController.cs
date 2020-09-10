using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
namespace WebBanHang.Controllers
{
    public class GioHangController : Controller
    {
        private WebBanHangDbContext db = new WebBanHangDbContext();
       
        //Tính tổng số lượng sản phẩm trong giỏ hàng
        public double TinhTongSoLuong()
        {
            List<ItemCart> listGioHang = Session["GioHang"] as List<ItemCart>;
            if (listGioHang == null)
            {
                return 0;
            }
            return listGioHang.Sum(n => n.SoLuong);
        }

        //Tính tổng tiền của các sản phẩm trong giỏ hàng
        public decimal TinhTongTien()
        {
            List<ItemCart> listGioHang = Session["GioHang"] as List<ItemCart>;
            if (listGioHang == null)
            {
                return 0;
            }
            return listGioHang.Sum(n => n.ThanhTien);
        }

        public ActionResult GioHangPartial()
        {
            if (TinhTongSoLuong() == 0)
            {
                ViewBag.TongSoLuong = 0;
                ViewBag.TongTien = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TinhTongSoLuong();
            ViewBag.TongTien = TinhTongTien().ToString("#,##");
            
            return PartialView();
        }

        // Lấy giỏ hàng
        public List<ItemCart> LayGioHang()
        {
            //Giỏ hàng đã tồn tại
            List<ItemCart> listGioHang = Session["GioHang"] as List<ItemCart>;
            if (listGioHang == null)
            {
                //nếu session giỏ hàng chưa tồn tại thì tạo mới giỏ hàng
                listGioHang = new List<ItemCart>();
                Session["GioHang"] = listGioHang;

            }
            return listGioHang;
        }

        //  Thêm giỏ hàng
        public ActionResult ThemGioHang(int MaSP,string strURL)
        {
            //Kiểm tra sản phẩm có tồn tại trong database ko
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //trả về đường dẫn ko hợp lệ 404
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng
            List<ItemCart> listGioHang = LayGioHang();

            // sản phẩm đã tồn tại trong giỏ hàng
            ItemCart spcheck = listGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spcheck != null)
            {
                //Kiểm tra sản phẩm còn hàng hay không
                if (sp.SoLuongTon <= spcheck.SoLuong)
                {
                    return View("ThongBao");
                }

                spcheck.SoLuong ++;
                spcheck.ThanhTien = spcheck.DonGia * spcheck.SoLuong;
                return Redirect(strURL);
            }
           
            ItemCart itemcart = new ItemCart(MaSP);
            if (sp.SoLuongTon <= itemcart.SoLuong)
            {
                return View("ThongBao");
            }
            listGioHang.Add(itemcart);
            return Redirect(strURL);
        }
        

        // GET: Xem giỏ hàng
        public ActionResult XemGioHang()
        {
            //Lấy giỏ hàng
            List<ItemCart> listGioHang = LayGioHang();
            ViewBag.TongTien = TinhTongTien().ToString("#,##");
            return View(listGioHang);

        }

        // Chỉnh sửa giỏ hàng
        [HttpGet]
        public ActionResult SuaGioHang(int? MaSP)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Kiểm tra sản phẩm có tồn tại trong database ko
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //trả về đường dẫn ko hợp lệ 404
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng từ session
            List<ItemCart> listGioHang = LayGioHang();
            // sản phẩm đã tồn tại trong giỏ hàng
            ItemCart spcheck = listGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spcheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.GioHang = listGioHang;
            return View(spcheck);

        }
        //POST cập nhật lại giỏ hàng sau khi sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CapNhatGioHang([Bind(Include = "MaSP,TenSP,SoLuong,DonGia,ThanhTien,HinhAnh")] ItemCart Itgiohang)
        {
            if (ModelState.IsValid)
            {
                SanPham spcheck = db.SanPhams.Single(n => n.MaSP == Itgiohang.MaSP);
                if (spcheck.SoLuongTon <= Itgiohang.SoLuong)
                {
                    return View("ThongBao");
                }
                List<ItemCart> ListGH = LayGioHang();
                ItemCart ItGHUpdate = ListGH.Find(n => n.MaSP == Itgiohang.MaSP);
                ItGHUpdate.SoLuong = Itgiohang.SoLuong;
                ItGHUpdate.ThanhTien = ItGHUpdate.DonGia * ItGHUpdate.SoLuong;
            }

            return RedirectToAction("XemGioHang");
        }

        public ActionResult XoaGioHang(int? MaSP)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Kiểm tra sản phẩm có tồn tại trong database ko
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                //trả về đường dẫn ko hợp lệ 404
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng từ session
            List<ItemCart> listGioHang = LayGioHang();
            // sản phẩm đã tồn tại trong giỏ hàng
            ItemCart spcheck = listGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spcheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Xóa giỏ hàng
            listGioHang.Remove(spcheck);
            return RedirectToAction("XemGioHang");
        }

        //Đặt hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DatHang(KhachHang KH)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            //Thêm thông tin khách hàng chưa đăng nhập
            KhachHang khachhang = new KhachHang();          
            if (Session["TaiKhoan"] == null)
            {
                khachhang = KH;
                db.KhachHangs.Add(khachhang);
                db.SaveChanges();
            }
            //Thêm thông tin khách hàng đã đăng nhập (có tài khoản)
            else
            {
                ThanhVien thanhvien = Session["TaiKhoan"] as ThanhVien;
                khachhang.TenKH = thanhvien.HoTen;
                khachhang.DiaChi = thanhvien.DiaChi;
                khachhang.Email = thanhvien.Email;
                khachhang.SDT = thanhvien.SDT;
                khachhang.MaThanhVien = thanhvien.MaThanhVien;
                db.KhachHangs.Add(khachhang);
                db.SaveChanges();
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Thêm đơn đặt hàng vào database
            DonDatHang ddh = new DonDatHang();
            ddh.MaKH = khachhang.MaKH;
            ddh.NgatDat = DateTime.Now;
            ddh.TinhTrang = false;
            ddh.DaHuy = false;
            ddh.DaThanhToan = false;           
            ddh.UuDai = 0;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();

            //Thêm chi tiết đơn đặt hàng vào database
            List<ItemCart> listGioHang = LayGioHang();
            foreach(var item in listGioHang)
            {
                ChiTietDonDatHang ctddh = new ChiTietDonDatHang();
                ctddh.MaDDH = ddh.MaDDH;
                ctddh.MaSP = item.MaSP;
                ctddh.TenSP = item.TenSP;
                ctddh.Gia = item.DonGia;
                ctddh.SoLuong = item.SoLuong;
                db.ChiTietDonDatHangs.Add(ctddh);
                
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return View("ThongBaoThanhCong");
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