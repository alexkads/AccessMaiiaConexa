using System;
namespace AccessMaiiaConexa.Models.Maiia
{
    public partial class Contato
    {
        public int id { get; set; }
        public int relacionamento { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public string email_cc { get; set; }
        public EntidadeDetalhe EntidadeDetalhe { get; set; }
    }
}
