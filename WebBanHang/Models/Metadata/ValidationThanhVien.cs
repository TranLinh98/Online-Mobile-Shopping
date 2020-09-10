using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.DynamicData;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    [MetadataType(typeof(ValidationThanhVien))]
    public partial class ThanhVien
    {
        internal sealed class ValidationThanhVien
        {

            [Key]
            [Display(Name = "Mã thành viên")]
            public int MaThanhVien { get; set; }

            [Required(ErrorMessage = "Tài Khoản không được để trống")]
            [StringLength(100)]
            public string TaiKhoan { get; set; }

            [Required(ErrorMessage = "Mật khẩu không được để trống")]
            [StringLength(100)]
            [DataType(DataType.Password)]
            public string MatKhau { get; set; }

            [Display(Name = "Họ Tên")]

            [StringLength(250)]
            public string HoTen { get; set; }

            [Display(Name = "Địa Chỉ")]
            [StringLength(250)]
            public string DiaChi { get; set; }

            [StringLength(250)]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Display(Name = "Số điện thoại")]
            [StringLength(12, MinimumLength = 9)]
            public string SDT { get; set; }

            [Display(Name = "Câu hỏi bí mật")]
            public string CauHoi { get; set; }

            [Display(Name = "Câu trả lời")]
            public string CauTraLoi { get; set; }

            [Display(Name = "Mã loại thành viên")]
            public int? MaLoaiTV { get; set; }
          

        }


    }
}