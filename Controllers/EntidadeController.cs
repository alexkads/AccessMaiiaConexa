using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessMaiiaConexa.Models.Conexa;
using AccessMaiiaConexa.Models.Maiia;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccessMaiiaConexa.Controllers
{
    [Route("entidades")]
    [ApiController]
    public class EntidadeController : ControllerBase
    {
        //[HttpGet]
        //[Route("")]
        //public async Task<ActionResult<List<Entidade>>> Get([FromServices] MaiiaDataContext context)
        //{
        //    var entidades = await context
        //        .Entidades
        //        .Include(ed => ed.EntidadeDetalhes)
        //        .ThenInclude(ct => ct.Contatos)
        //        .Take(10)
        //        .AsNoTracking()
        //        .ToListAsync();

        //    return Ok(entidades);
        //}

        [HttpGet]
        [Route("{email}")]
        [AllowAnonymous]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]
        public async Task<ActionResult<dynamic>> GetByEmail(
            [FromServices] MaiiaDataContext contextMaiia,
            [FromServices] ConexaClubCoWorkingDataContext contextConexaClubCoWorking,
            [FromServices] ConexaVirtualOfficeDataContext contextConexaVirtualOffice,
            string email)
        {
            var clienteClub = await contextConexaClubCoWorking.Clientes.FirstOrDefaultAsync(x => x.emails.Contains(email));

            if (clienteClub != null)
                return Ok(new
                {
                    message = "Cliente foi localizada no Conexa (ClubCoWorking)",
                    ClientName = clienteClub.razaoSocial
                });

            var clienteVO = await contextConexaVirtualOffice.Clientes.FirstOrDefaultAsync(x => x.emails.Contains(email));

            if (clienteVO != null)
                return Ok(new
                {
                    message = "Cliente foi localizada no Conexa (VirtualOffice)",
                    ClientName = clienteVO.razaoSocial
                });

            var entidade = await contextMaiia
                .Entidades
                .Include(ed => ed.EntidadeDetalhes)
                .ThenInclude(ct => ct.Contatos)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.status != "Cancelado" && x.email.Contains(email));

            if (entidade != null)
                return Ok(new
                {
                    message = "Cliente foi localizada no Maiia",
                    ClientName = entidade.razao
                });

            var contato = await contextMaiia
                .Contatos
                .Include(e => e.EntidadeDetalhe)
                .Include(e => e.EntidadeDetalhe.Entidade)
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                x.email == email
                ||
                x.email.Contains(email)
                ||
                x.EntidadeDetalhe.Entidade.email.Contains(email));

            var IdEntidade = contato?.EntidadeDetalhe?.Entidade?.id;

            if (IdEntidade == null)
                return BadRequest(new
                {
                    message = "Cliente não encontrado",
                    ClientName = string.Empty
                });

            var contatoEntidade = await contextMaiia
                .Entidades
                .Include(ed => ed.EntidadeDetalhes)
                .ThenInclude(ct => ct.Contatos)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.id == IdEntidade);

            return Ok(new
            {
                message = "Cliente foi localizada no Maiia",
                ClientName = contatoEntidade.razao
            });
        }
    }
}
