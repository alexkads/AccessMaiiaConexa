using System;
using System.Collections.Generic;

namespace AccessMaiiaConexa.Models.Maiia
{
    public partial class Produto
    {
        public int id { get; set; }

        public string codigo { get; set; }

        public string descricao { get; set; }

        public string undmedia { get; set; }

        public string grupo { get; set; }

        public string categoria { get; set; }

        public int unidade { get; set; }
    }
}
