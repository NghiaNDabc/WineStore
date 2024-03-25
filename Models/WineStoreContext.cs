using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WineStore.Models
{
    public partial class WineStoreContext : DbContext
    {
        public WineStoreContext()
            : base("name=WineStoreContext")
        {
        }

        public virtual DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
        public virtual DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public virtual DbSet<DanhMuc> DanhMucs { get; set; }
        public virtual DbSet<DonHang> DonHangs { get; set; }
        public virtual DbSet<GioHang> GioHangs { get; set; }
        public virtual DbSet<HinhThucThanhToan> HinhThucThanhToans { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<SanPham> SanPhams { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.maDonHang)
                .IsFixedLength();

            modelBuilder.Entity<ChiTietGioHang>()
                .Property(e => e.maGioHang)
                .IsFixedLength();

            modelBuilder.Entity<DanhMuc>()
                .Property(e => e.maDanhMuc)
                .IsFixedLength();

            modelBuilder.Entity<DanhMuc>()
                .Property(e => e.moTa)
                .IsUnicode(false);

            modelBuilder.Entity<DanhMuc>()
                .HasMany(e => e.SanPhams)
                .WithRequired(e => e.DanhMuc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.maDonHang)
                .IsFixedLength();

            modelBuilder.Entity<DonHang>()
                .Property(e => e.ghiChu)
                .IsUnicode(false);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.maKhacHang)
                .IsFixedLength();
            modelBuilder.Entity<DonHang>()
                .Property(e => e.hoTenNguoiNhan)
                .IsFixedLength();
            modelBuilder.Entity<DonHang>()
                .Property(e => e.sdtNguoiNhan)
                .IsFixedLength();
            modelBuilder.Entity<DonHang>()
                .Property(e => e.emailNguoiNhan)
                .IsFixedLength();
            modelBuilder.Entity<DonHang>()
                .Property(e => e.maHinhThucThanhToan)
                .IsFixedLength();

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GioHang>()
                .Property(e => e.maGioHang)
                .IsFixedLength();

            modelBuilder.Entity<GioHang>()
                .Property(e => e.maKhacHang)
                .IsFixedLength();

            modelBuilder.Entity<GioHang>()
                .HasMany(e => e.ChiTietGioHangs)
                .WithRequired(e => e.GioHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HinhThucThanhToan>()
                .Property(e => e.maHinhThucThanhToan)
                .IsFixedLength();

            modelBuilder.Entity<HinhThucThanhToan>()
                .Property(e => e.moTa)
                .IsUnicode(false);

            modelBuilder.Entity<HinhThucThanhToan>()
                .HasMany(e => e.DonHangs)
                .WithRequired(e => e.HinhThucThanhToan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.maKhacHang)
                .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.tenDangNhap)
                .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.matKhau)
                .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.sdt)
                .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.email)
                .IsFixedLength();

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.DonHangs)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.GioHangs)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.maDanhMuc)
                .IsFixedLength();

            modelBuilder.Entity<SanPham>()
                .Property(e => e.moTa)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.giaNhap)
                .HasPrecision(8, 2);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.giaBan)
                .HasPrecision(8, 2);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.image)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.Vintage)
                .IsFixedLength();

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietDonHangs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietGioHangs)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Admin>()
                    .Property(e => e.taiKhoan)
                    .IsFixedLength();
            modelBuilder.Entity<Admin>()
                        .Property(e => e.matKhau)
                        .IsFixedLength();
        }
    }
}
