using QlSinhVien.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QlSinhVien.Configurations
{
    public class ExptConfiguration : IEntityTypeConfiguration<Expt>
    {
        public void Configure(EntityTypeBuilder<Expt> builder)
        {
            builder.ToTable(nameof(Expt));
            builder.HasKey(o => new { o.IdExpt, o.IdQ });
            //builder.HasAlternateKey(x => x.Protect);
        }
    }
}
