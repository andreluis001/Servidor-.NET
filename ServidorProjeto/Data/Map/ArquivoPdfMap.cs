using ServidorProjeto; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServidorProjeto.Models;
namespace ServidorProjeto.Map
{
    public class ArquivoPdfMap : IEntityTypeConfiguration<ArquivoPdf>
    {
        public void Configure(EntityTypeBuilder<ArquivoPdf> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome)
                   .IsRequired()
                   .HasMaxLength(255);
        }
    }
}
