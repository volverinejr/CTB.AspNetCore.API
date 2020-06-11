using System.Collections.Generic;
using Domain.Modulos.Infracao.Infracao;
using Domain.Modulos.Infracao.Infracao.Command;
using Domain.Shared.Entidade;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("v1/infracao")]
    public class InfracaoController : ControllerBase
    {

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
            [FromServices] InfracaoService service
        )
        {
            GenericResult result = service.Exec(command);

            return StatusCode(result.Status, result);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(
            int id,
            [FromServices] InfracaoService service
        )
        {
            InfracaoDeleteCommand command = new InfracaoDeleteCommand(id);

            GenericResult result = service.Exec(command);

            return StatusCode(result.Status, result);
        }


        [HttpGet("{id}")]
        public InfracaoModel GetByIdExterno(
            int id,
            [FromServices] InfracaoService service
        )
        {
            return service.GetById(id, "");
        }


        [HttpGet("{pagina}/{qtd}/{campo}/{ordem}/{filtro}")]
        [Route("pesquisa")]
        public IEnumerable<InfracaoModel> GetAll(
            [FromQuery] short pagina,
            [FromQuery] short qtd,
            [FromQuery] string campo,
            [FromQuery] short ordem,
            [FromQuery] string filtro,
            [FromServices] InfracaoService service
        )
        {
            Pesquisa pesquisa = new Pesquisa(pagina, qtd, campo, ordem, filtro);

            return service.GetAll(pesquisa);
        }


        [HttpGet]
        [Route("totalderegistros")]
        public int GetTotalRegistros(
            [FromServices] InfracaoService service
        )
        {
            return service.GetTotalDeRegistros();
        }


    }
}