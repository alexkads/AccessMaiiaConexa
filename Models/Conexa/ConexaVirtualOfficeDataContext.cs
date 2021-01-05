using System;
using Microsoft.EntityFrameworkCore;

namespace AccessMaiiaConexa.Models.Conexa
{
    public partial class ConexaVirtualOfficeDataContext : DbContext
    {
        public ConexaVirtualOfficeDataContext() : base()
        {
        }

        public ConexaVirtualOfficeDataContext(DbContextOptions<ConexaVirtualOfficeDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .ToTable("cliente")
                .HasKey(a => a.idUser);
        }
    }
}
