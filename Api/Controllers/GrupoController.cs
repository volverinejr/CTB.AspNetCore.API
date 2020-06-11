using System.Collections.Generic;
using Domain.Modulos.Infracao.Grupo;
using Domain.Modulos.Infracao.Grupo.Command;
using Domain.Shared.Entidade;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/grupo")]
    public class GrupoController : ControllerBase
    {

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
            [FromServices] GrupoService service
        )
        {
            GenericResult result = service.Exec(command);

            return StatusCode(result.Status, result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(
            int id,
            [FromServices] GrupoService service
        )
        {
            GrupoDeleteCommand command = new GrupoDeleteCommand(id);

            GenericResult result = service.Exec(command);

            return StatusCode(result.Status, result);
        }


        [HttpGet("{id}")]
        public GrupoResult GetByIdExterno(
            int id,
            [FromServices] GrupoService service
        )
        {
            return service.GetById(id, "");
        }


        [HttpGet("{pagina}/{qtd}/{campo}/{ordem}/{filtro}")]
        [Route("pesquisa")]
        public IEnumerable<GrupoResult> GetAll(
            [FromQuery] short pagina,
            [FromQuery] short qtd,
            [FromQuery] string campo,
            [FromQuery] short ordem,
            [FromQuery] string filtro,
            [FromServices] GrupoService service
        )
        {
            Pesquisa pesquisa = new Pesquisa(pagina, qtd, campo, ordem, filtro);

            return service.GetAll(pesquisa);
        }


        [HttpGet]
        [Route("totalderegistros")]
        public int GetTotalRegistros(
            [FromServices] GrupoService service
        )
        {
            return service.GetTotalDeRegistros();
        }


    }
}