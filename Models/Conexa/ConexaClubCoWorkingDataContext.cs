using System;
using Microsoft.EntityFrameworkCore;

namespace AccessMaiiaConexa.Models.Conexa
{
    public partial class ConexaClubCoWorkingDataContext : DbContext
    {
        public ConexaClubCoWorkingDataContext() : base()
        {
        }

        public ConexaClubCoWorkingDataContext(DbContextOptions<ConexaClubCoWorkingDataContext> options)
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
