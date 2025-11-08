using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServidorProjeto.Models;

namespace ServidorProjeto.Map
{
    public class DoacaoMap : IEntityTypeConfiguration<Doacao>
    {
        public void Configure(EntityTypeBuilder<Doacao> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.description).HasMaxLength(2000);
            builder.Property(x => x.Unite_price).HasColumnType("decimal(18,2)");
        }
    }
}
