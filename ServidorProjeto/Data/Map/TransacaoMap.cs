using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServidorProjeto.Models;

namespace ServidorProjeto.Map
{
    public class TransacaoMap : IEntityTypeConfiguration<TransacaoDoacao>
    {
        public void Configure(EntityTypeBuilder<TransacaoDoacao> builder)
        {
            builder.HasKey(x => x.Id); // usa propriedade, não método
            builder.Property(x => x.Valor).HasColumnType("decimal(18,2)");
            builder.Property(x => x.DataTransacao).IsRequired();

            // Se quiser, mapeie dados de navegação e enum também
            // builder.HasOne(x => x.DadosPagamento).WithOne().HasForeignKey<DadosBancarios>(...);
        }
    }
}
