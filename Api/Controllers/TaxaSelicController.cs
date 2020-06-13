using System;
using System.Collections.Generic;
using Domain.Modulos.TaxaSelic;
using Domain.Modulos.TaxaSelic.Command;
using Domain.Shared.Entidade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Controllers
{

    [ApiController]
    [Route("v1/taxaselic")]
    public class TaxaSelicController : ControllerBase
    {
        public const int CACHEEMMINUTOS = 5;

        [HttpPost]
        public IActionResult Insert(
            [FromBody] TaxaSelicInsertCommand command,
            [FromServices] TaxaSelicService service
        )
        {
            GenericResult result = service.Exec(command);

            return StatusCode(result.Status, result);
        }


        [HttpPut]
        public IActionResult Update(
            [FromBody] TaxaSelicUpdateCommand command,
            [FromServices] TaxaSelicService service,
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
            [FromServices] TaxaSelicService service,
            [FromServices] IMemoryCache cache
        )
        {
            TaxaSelicDeleteCommand command = new TaxaSelicDeleteCommand(id);

            GenericResult result = service.Exec(command);

            if (result.Status == 204)
            {
                cache.Remove(id);
            }

            return StatusCode(result.Status, result);
        }


        [HttpGet("{id:int:min(1)}")]
        public TaxaSelicModel GetByIdExterno(
            int id,
            [FromServices] TaxaSelicService service,
            [FromServices] IMemoryCache cache
        )
        {
            TaxaSelicModel taxaSelicModel;

            if (!cache.TryGetValue(id, out taxaSelicModel))
            {
                taxaSelicModel = service.GetById(id, "");

                if (taxaSelicModel != null)
                {
                    var opcoesDoCache = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(CACHEEMMINUTOS)
                    };
                    cache.Set(id, taxaSelicModel, opcoesDoCache);
                }
            }

            return taxaSelicModel;
        }


        [HttpGet]
        [Route("pesquisa/{pagina:int:min(0)}/{qtd:int:max(500)}/{campo:alpha}/{ordem:int:range(-1, 1)}/{filtro?}")]
        public IEnumerable<TaxaSelicModel> GetAll(
            short pagina,
            short qtd,
            string campo,
            short ordem,
            [FromServices] TaxaSelicService service,
            string filtro = ""
        )
        {
            Pesquisa pesquisa = new Pesquisa(pagina, qtd, campo, ordem, filtro);

            return service.GetAll(pesquisa);
        }


        [HttpGet]
        [Route("totalderegistros")]
        public int GetTotalRegistros(
            [FromServices] TaxaSelicService service
        )
        {
            return service.GetTotalDeRegistros();
        }

    }
}