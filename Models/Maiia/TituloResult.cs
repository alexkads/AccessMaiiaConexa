using System;
using System.Collections.Generic;

namespace AccessMaiiaConexa.Models.Maiia
{
    public partial class TituloResult
    {
        public string cnpj { get; set; }
        public string razao { get; set; }
        public string status { get; set; }
        public string unidade { get; set; }
        public string nomeunidade { get; set; }
        public string tipo { get; set; }
        public int numcorte { get; set; }
        public string tipoentidade { get; set; }
        public int id { get; set; }
        public DateTime? vencimento { get; set; }
        public double valor { get; set; }
        public double pago { get; set; }
        public int entidadeid { get; set; }
        public string produto { get; set; }
        public string parcela { get; set; }
        public string telefones_fixo { get; set; }
        public string telefones_celular { get; set; }
        public string emails { get; set; }
        public string ruadecorrepondencia { get; set; }
        public string numerodecorrepondencia { get; set; }
        public string complementodecorrepondencia { get; set; }
        public string bairrodecorrepondencia { get; set; }
        public string cepdecorrepondencia { get; set; }
        public string cidadedecorrepondencia { get; set; }
        public string ie { get; set; }
        public string rg { get; set; }

    }
}
