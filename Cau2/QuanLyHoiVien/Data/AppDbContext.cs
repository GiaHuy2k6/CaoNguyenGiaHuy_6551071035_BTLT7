using Microsoft.EntityFrameworkCore;
using QuanLyHoiVien.Models;

namespace QuanLyHoiVien.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<HoiVien> HoiViens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Thay đổi connection string phù hợp với SQL Server của bạn
            optionsBuilder.UseSqlServer(
                @"Server=.\SQLEXPRESS;Database=GymFitZone;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HoiVien>(entity =>
            {
                entity.ToTable("HoiVien");
                entity.HasKey(e => e.MaHV);
                entity.Property(e => e.MaHV).UseIdentityColumn();
                entity.Property(e => e.HoTen).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SDT).HasMaxLength(15);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.HangThanhVien).HasMaxLength(20);
                entity.Property(e => e.NgayDangKy).HasDefaultValueSql("GETDATE()");
            });
        }
    }
}
