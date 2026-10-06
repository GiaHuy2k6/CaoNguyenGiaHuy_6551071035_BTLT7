using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cau3_App.Models;

public partial class SunriseHomestayContext : DbContext
{
    public SunriseHomestayContext()
    {
    }

    public SunriseHomestayContext(DbContextOptions<SunriseHomestayContext> options)
        : base(options)
    {
    }

    public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; }

    public virtual DbSet<Phong> Phongs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SunriseHomestay;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LoaiPhong>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__LoaiPhon__730A57597AF5ECF9");

            entity.ToTable("LoaiPhong");

            entity.Property(e => e.GiaMoiDem).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenLoai).HasMaxLength(100);
        });

        modelBuilder.Entity<Phong>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("PK__Phong__20BD5E5BB1A531D2");

            entity.ToTable("Phong");

            entity.Property(e => e.HinhAnh).HasMaxLength(255);
            entity.Property(e => e.SoPhong)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TinhTrang).HasMaxLength(20);

            entity.HasOne(d => d.MaLoaiNavigation).WithMany(p => p.Phongs)
                .HasForeignKey(d => d.MaLoai)
                .HasConstraintName("FK_Phong_LoaiPhong");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
