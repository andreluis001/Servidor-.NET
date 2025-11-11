using ServidorProjeto; // PARA ENCONTRAR Atividade
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServidorProjeto.Models;

namespace ServidorProjeto.Map
{
    public class AtividadeMap : IEntityTypeConfiguration<Atividade>
    {
        public void Configure(EntityTypeBuilder<Atividade> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Descricao).HasMaxLength(1000);
            builder.Property(x => x.Link).HasMaxLength(500);

            // Discriminator para herança
            builder.HasDiscriminator<string>("TipoAtividade")
                   .HasValue<Atividade>("Atividade")
                   .HasValue<Transparencia>("Transparencia");

        }
    }
}
