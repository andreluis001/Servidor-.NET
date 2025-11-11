using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServidorProjeto.Models;

namespace ServidorProjeto.Map
{
    public class TransparenciaMap : IEntityTypeConfiguration<Transparencia>
    {
        public void Configure(EntityTypeBuilder<Transparencia> builder)
        {
            builder.Property(x => x.NomeOriginal).HasMaxLength(255);
            builder.Property(x => x.CaminhoArquivo).IsRequired().HasMaxLength(500);
            builder.Property(x => x.DataUpload).IsRequired();

        }
    }
}
