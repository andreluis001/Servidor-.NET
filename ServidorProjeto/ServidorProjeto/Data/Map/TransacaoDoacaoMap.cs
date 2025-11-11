using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServidorProjeto.Models; // ✅ Certifique-se de usar este namespace

namespace ServidorProjeto.Map
{
    public class TransacaoDoacaoMap : IEntityTypeConfiguration<TransacaoDoacao>
    {
        public void Configure(EntityTypeBuilder<TransacaoDoacao> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Valor).HasColumnType("decimal(18,2)");
            builder.Property(x => x.DataTransacao).IsRequired();
        }
    }
}
