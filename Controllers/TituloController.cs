using AccessMaiiaConexa.Models.Maiia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AccessMaiiaConexa.Controllers
{

    [ApiController]
    public class TituloController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("titulos2")]
        public async Task<ActionResult<List<dynamic>>> Get([FromServices] MaiiaDataContext context)
        {

            //var result = await context
            //    .Titulos
            //    .Include(ed => ed.Entidade)
            //    .AsNoTracking()
            //    .Where(w =>
            //        w.pago < w.valor
            //        && w.tipo == "Boleto"
            //        && w.Entidade.tipo == "Cliente"
            //        && (
            //            w.Entidade.status == "Contratual"
            //            ||
            //            w.Entidade.status == "Esporadico"
            //            ||
            //            w.Entidade.status == "Pendente"
            //            ||
            //            w.Entidade.status == "Ativo"))
            //    .Join(context.CalendarioDeReservas.Where(w=> w.numcorte > 0), tt => tt.numcorte, cr => cr.numcorte, (titulo, reserva) => new { titulo, reserva })
            //    .Select(x => new
            //    {
            //        x.titulo.Entidade.cnpj,
            //        razao = htmlDecode(x.titulo.Entidade.razao),
            //        x.titulo.Entidade.status,
            //        x.titulo.Entidade.EntidadeDetalhes.FirstOrDefault().unidade,
            //        x.titulo.tipo,
            //        x.titulo.numcorte,
            //        produto = x.reserva.codigo,
            //        tipoentidade = x.titulo.Entidade.tipo,
            //        x.titulo.id,
            //        x.titulo.Vencimento,
            //        x.titulo.valor,
            //        x.titulo.pago,
            //        x.titulo.EntidadeId
            //    })
            //    .ToListAsync();

            var result = await context
                .Titulos
                .Include(ed => ed.Entidade)
                .AsNoTracking()
                .Where(w =>
                    w.pago < w.valor
                    && w.tipo == "Boleto"
                    && w.Entidade.tipo == "Cliente"
                    && (
                        w.Entidade.status == "Contratual"
                        ||
                        w.Entidade.status == "Esporadico"
                        ||
                        w.Entidade.status == "Pendente"
                        ||
                        w.Entidade.status == "Ativo"))
                .Select(x => new
                {
                    x.Entidade.cnpj,
                    razao = htmlDecode(x.Entidade.razao),
                    x.Entidade.status,
                    x.Entidade.EntidadeDetalhes.FirstOrDefault().unidade,
                    x.tipo,
                    x.numcorte,
                    tipoentidade = x.Entidade.tipo,
                    x.id,
                    x.Vencimento,
                    x.valor,
                    x.pago,
                    x.EntidadeId
                })
                .ToListAsync();


            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("titulos")]
        public async Task<ActionResult<List<dynamic>>> Get([FromServices] IConfiguration configuration)
        {
            using (var connection = new MySqlConnection(configuration.GetConnectionString("connectionStringMaiia")))
            {
                var queryString =
                @"SELECT
                ENT.cnpj,
                ENT.razao,
                ENT.status,
                ENT.unidade,
                UNID_ENT.primeirocontato as nomeunidade,
                TIT.tipo,
                TIT.numcorte,
                ENT.tipo as tipoentidade,
                TIT.id,
                TIT.vencimento,
                TIT.valor,
                TIT.pago,
                TIT.entidade as entidadeid,

                CASE
	                WHEN (SELECT descricao FROM produtos 
		                Inner Join calendariodereserva on produtos.codigo=calendariodereserva.codigo
		                Where calendariodereserva.numcorte = TIT.numcorte limit 1) <> ''
	                THEN
		                LEFT(concat(UNID_ENT.primeirocontato,'-',
		                (SELECT descricao FROM produtos 
		                Inner Join calendariodereserva on produtos.codigo=calendariodereserva.codigo
		                Where calendariodereserva.numcorte = TIT.numcorte limit 1)),100)
	                ELSE
		                LEFT(concat(UNID_ENT.primeirocontato,'-','Sem Produto-',TIT.numcorte),100)
                END as produto,
                concat('MÊS REF PARCELA(',date_format(TIT.vencimento, '%m/%Y'),')') as 'parcela',

                (select GROUP_CONCAT(DISTINCT REPLACE(CONT.telfixo, '/', ';') SEPARATOR ';')
	                from contatos as CONT where CONT.tipoendereco like 'Cob%' And CONT.relacionamento = ENT.id)
	                AS telefones_fixo,

                (select GROUP_CONCAT(DISTINCT REPLACE(CONT.telmovel, '/', ';') SEPARATOR ';')
	                from contatos as CONT where CONT.tipoendereco like 'Cob%' And CONT.relacionamento = ENT.id)
	                AS telefones_celular,
    
                (select GROUP_CONCAT(DISTINCT IF(CONT.email_cc Is Null or CONT.email_cc = '', CONT.email, concat(CONT.email, '; ', CONT.email_cc)) SEPARATOR '; ')
	                from contatos as CONT where CONT.relacionamento = ENT.id)
	                AS 'emails',

                (select CONT.logradouro from contatos as CONT where CONT.tipoendereco like 'Corres%' And CONT.relacionamento = ENT.id and (CONT.logradouro <> ENT.logradouro and CONT.numero <> ENT.numero) Limit 1) as 'ruadecorrepondencia',
                (select CONT.numero from contatos as CONT where CONT.tipoendereco like 'Corres%' And CONT.relacionamento = ENT.id and (CONT.logradouro <> ENT.logradouro and CONT.numero <> ENT.numero) Limit 1) as 'numerodecorrepondencia',
                (select CONT.complemento from contatos as CONT where CONT.tipoendereco like 'Corres%' And CONT.relacionamento = ENT.id and (CONT.logradouro <> ENT.logradouro and CONT.numero <> ENT.numero) Limit 1) as 'complementodecorrepondencia',
                (select CONT.bairro from contatos as CONT where CONT.tipoendereco like 'Corres%' And CONT.relacionamento = ENT.id and (CONT.logradouro <> ENT.logradouro and CONT.numero <> ENT.numero) Limit 1) as 'bairrodecorrepondencia',
                (select CONT.cep from contatos as CONT where CONT.tipoendereco like 'Corres%' And CONT.relacionamento = ENT.id and (CONT.logradouro <> ENT.logradouro and CONT.numero <> ENT.numero) Limit 1) as 'cepdecorrepondencia',
                (select CONT.municipio from contatos as CONT where CONT.tipoendereco like 'Corres%' And CONT.relacionamento = ENT.id and (CONT.logradouro <> ENT.logradouro and CONT.numero <> ENT.numero) Limit 1) as 'cidadedecorrepondencia',

                ENT.ie as 'ie',

                CASE
	                WHEN (length(ENT.cnpj) = 14)
	                THEN
		                (select CONT.rg 
		                from contatos as CONT where CONT.assina = true and CONT.relacionamento = ENT.id order by ENT.id limit 1)
	                ELSE null
                END as 'rg'

                FROM titulos as TIT
                INNER JOIN entidades as ENT on TIT.entidade = ENT.id
                INNER JOIN entidades_detalhes as ENTDET on ENT.ID = ENTDET.relacionamento
                INNER JOIN entidades as UNID_ENT on ENTDET.unidade = UNID_ENT.id
                where ENT.tipo = 'Cliente'
                and TIT.valor > TIT.pago
                and ENT.status in ('Contratual', 'Esporadico', 'Pendente', 'Ativo')
                and TIT.vencimento > '0000-00-00';";

                var result = await connection.QueryAsync<TituloResult>(queryString);
                var resultdeco = result.Select(x => new
                {
                    x.cnpj,
                    razao = htmlDecode(x.razao),
                    x.status,
                    x.unidade,
                    x.nomeunidade,
                    x.tipo,
                    x.numcorte,
                    x.tipoentidade,
                    x.id,
                    x.vencimento,
                    x.valor,
                    x.pago,
                    x.entidadeid,
                    x.produto,
                    x.parcela,
                    x.telefones_fixo,
                    x.telefones_celular,
                    x.emails,
                    x.ruadecorrepondencia,
                    x.numerodecorrepondencia,
                    x.complementodecorrepondencia,
                    x.bairrodecorrepondencia,
                    x.cepdecorrepondencia,
                    x.cidadedecorrepondencia,
                    x.ie,
                    x.rg
                });

                return Ok(resultdeco);
            }
        }

        private readonly Func<string, string> htmlDecode = value => HttpUtility.HtmlDecode(value);

        private readonly Func<MaiiaDataContext, int, string> QueryCalendario = (context, numcorte) =>
        {
            var calendario = context.CalendarioDeReservas.FirstOrDefault(cr => cr.numcorte == numcorte);
            return calendario?.codigo;
        };
    }
}
