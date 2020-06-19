using System;
using System.Collections.Generic;
using Domain.Modulos.Infracao.Infracao;
using Domain.Modulos.Infracao.Infracao.Command;
using Domain.Shared.Entidade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/infracao")]
    public class InfracaoController : ControllerBase
    {
        public const int CACHEEMMINUTOS = 5;

        [HttpPost]
        public IActionResult Insert(
            [FromBody] InfracaoInsertCommand command,
            [FromServices] InfracaoService service
        )
        {
            GenericResult result = service.Exec(command);

            return StatusCode(result.Status, result);
        }


        [HttpPut]
        public IActionResult Update(
            [FromBody] InfracaoUpdateCommand command,
            [FromServices] InfracaoService service,
            [FromServices] IMemoryCache cache
        )
        {
            GenericResult result = service.Exec(command);

            if (result.Status == 200)
            {
                cache.Remove(command.Id);
            }

            return StatusCode(result.Status, result);
        }


        [HttpDelete("{id:int:min(1)}")]
        public IActionResult Delete(
            int id,
            [FromServices] InfracaoService service,
            [FromServices] IMemoryCache cache
        )
        {
            InfracaoDeleteCommand command = new InfracaoDeleteCommand(id);

            GenericResult result = service.Exec(command);

            if (result.Status == 204)
            {
                cache.Remove(id);
            }

            return StatusCode(result.Status, result);
        }


        [HttpGet("{id:int:min(1)}")]
        public InfracaoModel GetByIdExterno(
            int id,
            [FromServices] InfracaoService service,
            [FromServices] IMemoryCache cache
        )
        {
            InfracaoModel infracaoModel;

            if (!cache.TryGetValue(id, out infracaoModel))
            {
                infracaoModel = service.GetById(id, "");

                if (infracaoModel != null)
                {
                    var opcoesDoCache = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(CACHEEMMINUTOS)
                    };
                    cache.Set(id, infracaoModel, opcoesDoCache);
                }
            }

            return infracaoModel;
        }


        [HttpGet]
        [Route("pesquisa/{pagina:int:min(0)}/{qtd:int:max(500)}/{campo:alpha}/{ordem:int:range(-1, 1)}/{filtro?}")]
        public GenericPesquisa GetAll(
            short pagina,
            short qtd,
            string campo,
            short ordem,
            [FromServices] InfracaoService service,
            string filtro = ""
        )
        {
            Pesquisa pesquisa = new Pesquisa(pagina, qtd, campo, ordem, filtro);

            GenericPesquisa result = new GenericPesquisa(
                service.GetAll(pesquisa),
                service.GetTotalDeRegistros(pesquisa)
            );

            return result;
        }


    }
}