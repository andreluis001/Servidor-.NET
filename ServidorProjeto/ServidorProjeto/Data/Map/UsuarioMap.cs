using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServidorProjeto.Models;

namespace ServidorProjeto.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Senha)
                .IsRequired();

            // Configuração para o campo IsAdm (bool → int)
            builder.Property(x => x.IsAdm)
                .HasConversion<int>() // converte bool para 0/1 no banco
                .HasDefaultValue(0); // padrão: usuário comum

            //Configuração da herança (Discriminator)
            builder.HasDiscriminator<string>("TipoUsuario")
                   .HasValue<Usuario>("Usuario")
                   .HasValue<Administracao>("Administracao");
        }
    }
}
