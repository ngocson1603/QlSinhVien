using QlSinhVien.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QlSinhVien.Configurations
{
    public class DiemHocSinhConfiguration : IEntityTypeConfiguration<DiemHocSinh>
    {
        public void Configure(EntityTypeBuilder<DiemHocSinh> builder)
        {
            builder.ToTable(nameof(DiemHocSinh));
            builder.HasKey(o => new { o.IdHocSinh, o.IdDiem });
            builder.HasOne(o => o.HocSinh).WithMany(y => y.DiemHocSinhs).HasForeignKey(x => x.IdHocSinh).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.Diem).WithMany(y => y.DiemHocSinhs).HasForeignKey(x => x.IdDiem).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
