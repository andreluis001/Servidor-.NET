using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServidorProjeto.Models;

namespace ServidorProjeto.Map
{
    public class OuvidoriaMap : IEntityTypeConfiguration<Ouvidoria>
    {
        public void Configure(EntityTypeBuilder<Ouvidoria> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Assunto).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Mensagem).IsRequired().HasMaxLength(2000);
        }
    }
}
