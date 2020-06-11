using System.Collections.Generic;
using Domain.Modulos.Infracao.Natureza;
using Domain.Modulos.Infracao.Natureza.Command;
using Domain.Shared.Entidade;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/natureza")]
    public class NaturezaController : ControllerBase
    {

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
            [FromServices] NaturezaService service
        )
        {
            GenericResult result = service.Exec(command);

            return StatusCode(result.Status, result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(
            int id,
            [FromServices] NaturezaService service
        )
        {
            NaturezaDeleteCommand command = new NaturezaDeleteCommand(id);

            GenericResult result = service.Exec(command);

            return StatusCode(result.Status, result);
        }


        [HttpGet("{id}")]
        public NaturezaResult GetByIdExterno(
            int id,
            [FromServices] NaturezaService service
        )
        {
            return service.GetById(id, "");
        }


        [HttpGet("{pagina}/{qtd}/{campo}/{ordem}/{filtro}")]
        [Route("pesquisa")]
        public IEnumerable<NaturezaResult> GetAll(
            [FromQuery] short pagina,
            [FromQuery] short qtd,
            [FromQuery] string campo,
            [FromQuery] short ordem,
            [FromQuery] string filtro,
            [FromServices] NaturezaService service
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