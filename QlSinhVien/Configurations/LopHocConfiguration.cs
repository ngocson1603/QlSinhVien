using QlSinhVien.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QlSinhVien.Configurations
{
    public class LopHocConfiguration : IEntityTypeConfiguration<LopHoc>
    {
        public void Configure(EntityTypeBuilder<LopHoc> builder)
        {
            builder.ToTable(nameof(LopHoc));
            builder.HasKey(x => x.Id);
            //builder.HasAlternateKey(x => x.Protect);
            builder.HasOne(o => o.Khoi).WithMany(y => y.LopHocs).HasForeignKey(x => x.IdKhoiLop).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
