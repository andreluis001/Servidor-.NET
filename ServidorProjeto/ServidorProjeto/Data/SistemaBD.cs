using Microsoft.EntityFrameworkCore;
using ServidorProjeto.Models;
using ServidorProjeto.Map;

namespace ServidorProjeto.Data
{
    public class SistemaBD : DbContext
    {
        public SistemaBD(DbContextOptions<SistemaBD> options) : base(options) { }

        // ===== DbSets =====
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Administracao> Administracoes { get; set; } // opcional, para consultas específicas
        public DbSet<Doador> Doadores { get; set; }

        public DbSet<Atividade> Atividades { get; set; } // inclui Transparencia via TPH
        public DbSet<ArquivoPdf> ArquivosPdf { get; set; }
        public DbSet<TipoAtv> TiposAtv { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Ouvidoria> Ouvidorias { get; set; }
        public DbSet<TransacaoDoacao> TransacoesDoacao { get; set; }
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ===== HERANÇA USUARIO =====
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Nome).IsRequired().HasMaxLength(255);
                entity.Property(x => x.Email).IsRequired().HasMaxLength(255);

                entity.HasDiscriminator<string>("TipoUsuario")
                      .HasValue<Usuario>("Usuario")
                      .HasValue<Administracao>("Administracao");
            });

            // ===== HERANÇA ATIVIDADE =====
            modelBuilder.Entity<Atividade>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Nome).IsRequired().HasMaxLength(255);
                entity.Property(x => x.Descricao).HasMaxLength(1000);
                entity.Property(x => x.Link).HasMaxLength(500);

                // Discriminator TPH
                entity.HasDiscriminator<string>("TipoAtividade")
                      .HasValue<Atividade>("Atividade")
                      .HasValue<Transparencia>("Transparencia");
            });

            // ===== MAPS =====
            modelBuilder.ApplyConfiguration(new AtividadeMap());
            modelBuilder.ApplyConfiguration(new TransparenciaMap());
            modelBuilder.ApplyConfiguration(new ArquivoPdfMap());
            modelBuilder.ApplyConfiguration(new TipoAtvMap());
            modelBuilder.ApplyConfiguration(new ContatoMap());
            modelBuilder.ApplyConfiguration(new OuvidoriaMap());

            // ===== TRANSACAO DOACAO =====
            modelBuilder.Entity<TransacaoDoacao>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Valor).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(x => x.DataTransacao).IsRequired();

                // DadosBancarios como owned entity
                entity.OwnsOne(x => x.DadosPagamento, db =>
                {
                    db.Property(d => d.NomeBanco).HasColumnName("NomeBanco").HasMaxLength(255);
                    db.Property(d => d.CodigoBanco).HasColumnName("CodigoBanco").HasMaxLength(50);
                    db.Property(d => d.Agencia).HasColumnName("Agencia").HasMaxLength(50);
                    db.Property(d => d.Conta).HasColumnName("Conta").HasMaxLength(50);
                    db.Property(d => d.Metodo).HasColumnName("Metodo").HasMaxLength(50);
                });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
