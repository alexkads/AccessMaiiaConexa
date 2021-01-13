using System;
using System.Collections.Generic;

namespace AccessMaiiaConexa.Models.Maiia
{
    public partial class CalendarioDeReserva
    {
        public int id { get; set; }

        public string codigo { get; set; }

        public int relacionamento { get; set; }

        public int unidade { get; set; }

        public int entidade { get; set; }

        public double qnt { get; set; }

        public double valor { get; set; }

        public string grupo { get; set; }

        public string categoria { get; set; }

        public int numcorte { get; set; }
    }
}
