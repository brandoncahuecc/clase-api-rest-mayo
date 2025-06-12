using clase_tres_api_categoria.Mediadores;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clase_tres_api_categoria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListarCategorias()
        {
            ListarCategoriasComando listar = new ListarCategoriasComando();
            var respuesta = await _mediator.Send(listar);
            return Ok(respuesta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarUnaCategoria(int id)
        {
            var respuesta = await _mediator.Send(new BuscarCategoriaComando(id));
            return Ok(respuesta);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoria(CrearCategoriaComando request)
        {
            var respuesta = await _mediator.Send(request);
            return Ok(respuesta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, ActualizarCategoriaComando request)
        {
            request = new ActualizarCategoriaComando(id, request.Nombre, request.Descripcion, request.Estado);

            var respuesta = await _mediator.Send(request);
            return Ok(respuesta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            bool resultado = await _mediator.Send(new EliminarCategoriaComando(id));
            return Ok(resultado);
        }
    }
}
