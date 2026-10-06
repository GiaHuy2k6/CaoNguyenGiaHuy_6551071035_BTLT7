using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyHoiVien.Models
{
    [Table("HoiVien")]
    public class HoiVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHV { get; set; }

        [Required]
        [MaxLength(100)]
        public string HoTen { get; set; } = string.Empty;

        /// <summary>1 = Nam, 0 = Nữ</summary>
        public bool GioiTinh { get; set; }

        public DateTime? NgaySinh { get; set; }

        [MaxLength(15)]
        public string? SDT { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        /// <summary>Basic / VIP / Premium</summary>
        [MaxLength(20)]
        public string? HangThanhVien { get; set; }

        public DateTime NgayDangKy { get; set; } = DateTime.Now;

        /// <summary>1 = Đang hoạt động, 0 = Tạm ngưng</summary>
        public bool TrangThai { get; set; } = true;
    }
}
