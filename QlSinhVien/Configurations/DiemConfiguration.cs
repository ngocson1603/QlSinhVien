using QlSinhVien.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QlSinhVien.Configurations
{
    public class DiemConfiguration : IEntityTypeConfiguration<Diem>
    {
        public void Configure(EntityTypeBuilder<Diem> builder)
        {
            builder.ToTable(nameof(Diem));
            builder.HasKey(x => x.Id);
        }
    }
}
