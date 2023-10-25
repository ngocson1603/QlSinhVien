using QlSinhVien.Configurations;
using QlSinhVien.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace QlSinhVien.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DiemConfiguration());
            modelBuilder.ApplyConfiguration(new KhoiLopConfiguration());
            modelBuilder.ApplyConfiguration(new HocSinhConfiguration());
            modelBuilder.ApplyConfiguration(new LopHocConfiguration());
            modelBuilder.ApplyConfiguration(new DiemHocSinhConfiguration());
            modelBuilder.ApplyConfiguration(new QConfiguration());
            modelBuilder.ApplyConfiguration(new ExptConfiguration());
        }

        public DbSet<KhoiLop> KhoiLops { get; set; }
        public DbSet<HocSinh> HocSinhs { get; set; }
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<Diem> Diems { get; set; }
        public DbSet<DiemHocSinh> DiemHocSinhs { get; set; }
        public DbSet<Q> Qs { get; set; }
        public DbSet<Expt> Expts { get; set; }
    }
}
