using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServidorProjeto;

namespace ServidorProjeto.Map
{
    public class AdministracaoMap : IEntityTypeConfiguration<Administracao>
    {
        public void Configure(EntityTypeBuilder<Administracao> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.IsAdm).HasDefaultValue(true);
        }
    }
}
