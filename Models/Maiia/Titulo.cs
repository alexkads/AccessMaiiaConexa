using System;
namespace AccessMaiiaConexa.Models.Maiia
{
    public partial class Titulo
    {
        public int id { get; set; }

        public DateTime Vencimento { get; set; }

        public double valor { get; set; }

        public double pago { get; set; }

        public int EntidadeId { get; set; }

        public string tipo { get; set; }

        public Entidade Entidade { get; set; }
    }
}
