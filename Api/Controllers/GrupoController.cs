using System;
using System.Collections.Generic;
using Domain.Modulos.Infracao.Grupo;
using Domain.Modulos.Infracao.Grupo.Command;
using Domain.Shared.Entidade;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/grupo")]
    public class GrupoController : ControllerBase
    {
        public const int CACHEEMMINUTOS = 5;

        [HttpPost]
        public IActionResult Insert(
            [FromBody] GrupoInsertCommand command,
            [FromServices] GrupoService service
        )
        {
            GenericResult result = service.Exec(command);

            return StatusCode(result.Status, result);
        }


        [HttpPut]
        public IActionResult Update(
            [FromBody] GrupoUpdateCommand command,
            [FromServices] GrupoService service,
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
            [FromServices] GrupoService service,
            [FromServices] IMemoryCache cache
        )
        {
            GrupoDeleteCommand command = new GrupoDeleteCommand(id);

            GenericResult result = service.Exec(command);

            if (result.Status == 204)
            {
                cache.Remove(id);
            }

            return StatusCode(result.Status, result);
        }


        [HttpGet("{id:int:min(1)}")]
        public GrupoModel GetByIdExterno(
            int id,
            [FromServices] GrupoService service,
            [FromServices] IMemoryCache cache
        )
        {
            GrupoModel grupoModel;

            if (!cache.TryGetValue(id, out grupoModel))
            {
                grupoModel = service.GetById(id, "");

                if (grupoModel != null)
                {
                    var opcoesDoCache = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(CACHEEMMINUTOS)
                    };
                    cache.Set(id, grupoModel, opcoesDoCache);
                }
            }

            return grupoModel;
        }


        [HttpGet]
        [Route("pesquisa/{pagina:int:min(0)}/{qtd:int:max(500)}/{campo:alpha}/{ordem:int:range(-1, 1)}/{filtro?}")]
        public GenericPesquisa GetAll(
            short pagina,
            short qtd,
            string campo,
            short ordem,
            [FromServices] GrupoService service,
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