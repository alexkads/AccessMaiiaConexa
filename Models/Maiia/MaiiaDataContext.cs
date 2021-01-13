using System;
using Microsoft.EntityFrameworkCore;

namespace AccessMaiiaConexa.Models.Maiia
{
    public partial class MaiiaDataContext : DbContext
    {
        public MaiiaDataContext() : base()
        {
        }

        public MaiiaDataContext(DbContextOptions<MaiiaDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Entidade> Entidades { get; set; }
        public virtual DbSet<EntidadeDetalhe> EntidadeDetalhes { get; set; }
        public virtual DbSet<Contato> Contatos { get; set; }
        public virtual DbSet<Titulo> Titulos { get; set; }
        public virtual DbSet<CalendarioDeReserva> CalendarioDeReservas { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entidade>()
                .ToTable("entidades")
                .HasMany(e => e.EntidadeDetalhes)
                .WithOne(ed => ed.Entidade)
                .HasForeignKey(ed => ed.relacionamento)
                .HasPrincipalKey(e => e.id);

            modelBuilder.Entity<Entidade>()
                .HasMany(e => e.Titulos)
                .WithOne(t => t.Entidade)
                .HasForeignKey(t => t.EntidadeId)
                .HasPrincipalKey(e => e.id);

            modelBuilder.Entity<EntidadeDetalhe>()
                .ToTable("entidades_detalhes")
                .HasMany(p => p.Contatos)
                .WithOne(b => b.EntidadeDetalhe)
                .HasForeignKey(p => p.relacionamento)
                .HasPrincipalKey(b => b.id);

            modelBuilder.Entity<Contato>()
                .ToTable("contatos");

            modelBuilder.Entity<Titulo>()
                .ToTable("titulos");

            //Mapeamento necessario, pois o nome da tabela e o mesmo do campo chave
            modelBuilder.Entity<Titulo>().Property(s => s.EntidadeId).HasColumnName("Entidade");

            modelBuilder.Entity<Produto>()
                .ToTable("produtos");

            modelBuilder.Entity<CalendarioDeReserva>()
                .ToTable("calendariodereserva");

        }
    }
}
