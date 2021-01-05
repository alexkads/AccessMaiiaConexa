using System;
namespace AccessMaiiaConexa.Models.Conexa
{
    public partial class Cliente
    {
        public int idUser { get; set; }
        public string razaoSocial { get; set; }
        public string cnpj { get; set; }
        public string cpf { get; set; }
        public string emails { get; set; }
    }
}
