using System;
using System.Collections.Generic;
using Domain.Modulos.Infracao.Natureza;
using Domain.Modulos.Infracao.Natureza.Command;
using Domain.Shared.Entidade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/natureza")]
    public class NaturezaController : ControllerBase
    {
        public const int CACHEEMMINUTOS = 5;

        [HttpPost]
        public IActionResult Insert(
            [FromBody] NaturezaInsertCommand command,
            [FromServices] NaturezaService service
        )
        {
            GenericResult result = service.Exec(command);

            return StatusCode(result.Status, result);
        }


        [HttpPut]
        public IActionResult Update(
            [FromBody] NaturezaUpdateCommand command,
            [FromServices] NaturezaService service,
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
            [FromServices] NaturezaService service,
            [FromServices] IMemoryCache cache
        )
        {
            NaturezaDeleteCommand command = new NaturezaDeleteCommand(id);

            GenericResult result = service.Exec(command);

            if (result.Status == 204)
            {
                cache.Remove(id);
            }

            return StatusCode(result.Status, result);
        }


        [HttpGet("{id:int:min(1)}")]
        public NaturezaModel GetByIdExterno(
            int id,
            [FromServices] NaturezaService service,
            [FromServices] IMemoryCache cache
        )
        {
            NaturezaModel naturezaModel;

            if (!cache.TryGetValue(id, out naturezaModel))
            {
                naturezaModel = service.GetById(id, "");

                if (naturezaModel != null)
                {
                    var opcoesDoCache = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(CACHEEMMINUTOS)
                    };
                    cache.Set(id, naturezaModel, opcoesDoCache);
                }
            }

            return naturezaModel;
        }


        [HttpGet]
        [Route("pesquisa/{pagina:int:min(0)}/{qtd:int:max(500)}/{campo:alpha}/{ordem:int:range(-1, 1)}/{filtro?}")]
        public IEnumerable<NaturezaModel> GetAll(
            short pagina,
            short qtd,
            string campo,
            short ordem,
            [FromServices] NaturezaService service,
            string filtro=""
        )
        {
            Pesquisa pesquisa = new Pesquisa(pagina, qtd, campo, ordem, filtro);

            return service.GetAll(pesquisa);
        }


        [HttpGet]
        [Route("totalderegistros")]
        public int GetTotalRegistros(
            [FromServices] NaturezaService service
        )
        {
            return service.GetTotalDeRegistros();
        }


    }
}