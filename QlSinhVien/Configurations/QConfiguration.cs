using QlSinhVien.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QlSinhVien.Configurations
{
    public class QConfiguration : IEntityTypeConfiguration<Q>
    {
        public void Configure(EntityTypeBuilder<Q> builder)
        {
            builder.ToTable(nameof(Q));
            builder.HasKey(o => new { o.IdQ, o.IdExpt });
            //builder.HasAlternateKey(x => x.Protect);
        }
    }
}
