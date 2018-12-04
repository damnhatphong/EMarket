using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EMarket.Areas.Admin.Models
{
    public partial class EMarketContext : DbContext
    {
        public EMarketContext()
        {
        }

        public EMarketContext(DbContextOptions<EMarketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HangHoa> HangHoa { get; set; }
        public virtual DbSet<Loai> Loai { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCap { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoan { get; set; }
        public virtual DbSet<ThongTinTaiKhoan> ThongTinTaiKhoan { get; set; }
        public virtual DbSet<TopSelling> TopSelling { get; set; }
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=EMarket;Integrated Security=True;");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HangHoa>(entity =>
            {
                entity.Property(e => e.HangHoaId).HasColumnName("HangHoaID");

                entity.Property(e => e.Hinh)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LoaiId).HasColumnName("LoaiID");

                entity.Property(e => e.NhaCungCapId).HasColumnName("NhaCungCapID");

                entity.Property(e => e.TenHangHoa)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.Loai)
                    .WithMany(p => p.HangHoa)
                    .HasForeignKey(d => d.LoaiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HangHoa_LoaiID");

                entity.HasOne(d => d.NhaCungCap)
                    .WithMany(p => p.HangHoa)
                    .HasForeignKey(d => d.NhaCungCapId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HangHoa_NhaCungCap");
            });

            modelBuilder.Entity<Loai>(entity =>
            {
                entity.Property(e => e.LoaiId).HasColumnName("LoaiID");

                entity.Property(e => e.TenLoai)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.Property(e => e.NhaCungCapId).HasColumnName("NhaCungCapID");

                entity.Property(e => e.MoTa).HasMaxLength(200);

                entity.Property(e => e.TenNhaCungCap)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<TaiKhoan>(entity =>
            {
                entity.Property(e => e.TaiKhoanId).HasColumnName("TaiKhoanID");

                entity.Property(e => e.NgayDk)
                    .HasColumnName("NgayDK")
                    .HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ThongTinTaiKhoanId).HasColumnName("ThongTinTaiKhoanID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ThongTinTaiKhoan)
                    .WithMany(p => p.TaiKhoan)
                    .HasForeignKey(d => d.ThongTinTaiKhoanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TaiKhoan_ThongTinTaiKhoan");
            });

            modelBuilder.Entity<ThongTinTaiKhoan>(entity =>
            {
                entity.Property(e => e.ThongTinTaiKhoanId).HasColumnName("ThongTinTaiKhoanID");

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.HoVaTen)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.NgaySinh).HasColumnType("datetime");

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TopSelling>(entity =>
            {
                entity.Property(e => e.TopSellingId).HasColumnName("TopSellingID");

                entity.Property(e => e.HangHoaId).HasColumnName("HangHoaID");

                entity.HasOne(d => d.HangHoa)
                    .WithMany(p => p.TopSelling)
                    .HasForeignKey(d => d.HangHoaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TopSelling_HangHoa");
            });
        }
    }
}
