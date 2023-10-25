using QlSinhVien.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QlSinhVien.Configurations
{
    public class HocSinhConfiguration : IEntityTypeConfiguration<HocSinh>
    {
        public void Configure(EntityTypeBuilder<HocSinh> builder)
        {
            builder.ToTable(nameof(HocSinh));
            builder.HasKey(o => new { o.Id});
            builder.HasOne(o => o.LopHoc).WithMany(y => y.HocSinhs).HasForeignKey(x => x.IdLop).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
