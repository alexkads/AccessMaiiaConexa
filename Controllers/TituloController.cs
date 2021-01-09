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

namespace AccessMaiiaConexa.Controllers
{
    [Authorize]
    [Route("titulos")]
    [ApiController]
    public class TituloController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<dynamic>>> Get([FromServices] MaiiaDataContext context)
        {
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
                .Select(x => new {
                    x.Entidade.cnpj,
                    razao = htmlDecode(x.Entidade.razao),
                    x.Entidade.status,
                    x.Entidade.EntidadeDetalhes.FirstOrDefault().unidade,
                    x.tipo,
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

        private readonly Func<string, string> htmlDecode = value => HttpUtility.HtmlDecode(value);
    }
}
