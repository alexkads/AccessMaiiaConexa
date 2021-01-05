using System;
using System.Collections.Generic;

namespace AccessMaiiaConexa.Models.Maiia
{
    public partial class Entidade
    {
        public int id { get; set; }
        public DateTime registro { get; set; }
        public string tipo { get; set; }
        public string status { get; set; }
        public string primeirocontato { get; set; }
        public string email { get; set; }
        public string razao { get; set; }
        public string cnpj { get; set; }
        public ICollection<EntidadeDetalhe> EntidadeDetalhes { get; set; }
        public ICollection<Titulo> Titulos { get; set; }
    }
}
