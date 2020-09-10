using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Models;
namespace WebBanHang.Models
{
    public class ItemCart
    {
        public int MaSP { get; set; }

        public string TenSP { get; set; }

        public int SoLuong { get; set; }

        public decimal DonGia { get; set; }

        public decimal ThanhTien { get; set; }

        public string HinhAnh { get; set; }

        public ItemCart(int iMaSP)
        {
            using (WebBanHangDbContext db = new WebBanHangDbContext())
            {
                MaSP = iMaSP;
                SanPham sp = db.SanPhams.Single(i => i.MaSP == iMaSP);
                TenSP = sp.TenSP;
                HinhAnh = sp.HinhAnh;
                SoLuong = 1;
                DonGia = sp.DonGia.Value;
                ThanhTien = DonGia * SoLuong;
            }
           
        }
        public ItemCart(int iMaSP, int SL)
        {
            using (WebBanHangDbContext db = new WebBanHangDbContext())
            {
                MaSP = iMaSP;
                SanPham sp = db.SanPhams.Single(i => i.MaSP == iMaSP);
                TenSP = sp.TenSP;
                HinhAnh = sp.HinhAnh;
                SoLuong = SL;
                DonGia = sp.DonGia.Value;
                ThanhTien = DonGia * SoLuong;
            }
               
        }
        public ItemCart()
        {

        }
    }
}