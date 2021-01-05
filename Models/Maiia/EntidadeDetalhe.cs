using System;
using System.Collections.Generic;

namespace AccessMaiiaConexa.Models.Maiia
{
    public partial class EntidadeDetalhe
    {
        public int id { get; set; }
        public int relacionamento { get; set; }
        public string unidade { get; set; }
        public Entidade Entidade { get; set; }
        public ICollection<Contato> Contatos { get; set; }
    }
}
