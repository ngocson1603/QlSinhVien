using QlSinhVien.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QlSinhVien.Configurations
{
    public class KhoiLopConfiguration : IEntityTypeConfiguration<KhoiLop>
    {
        public void Configure(EntityTypeBuilder<KhoiLop> builder)
        {
            builder.ToTable(nameof(KhoiLop));
            builder.HasKey(x => x.Id);
            //builder.HasAlternateKey(x => x.Protect);
        }
    }
}
