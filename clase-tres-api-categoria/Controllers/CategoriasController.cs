using clase_cinco_biblioteca.Controllers;
using clase_cinco_biblioteca.Modelos;
using clase_tres_api_categoria.Mediadores;
using clase_tres_api_categoria.Modelos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clase_tres_api_categoria.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : CustomeControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(IMediator mediator, ILogger<CategoriasController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ListarCategorias()
        {
            _logger.LogDebug("Este es log debug");
            _logger.LogInformation("Este es log Information");
            _logger.LogCritical("Este es log Critical");
            _logger.LogError("Este es log Error");
            _logger.LogWarning("Este es log Warning");
            _logger.LogTrace("Este es log Trace");
            ListarCategoriasComando listar = new ListarCategoriasComando();
            Respuesta<List<Categoria>> respuesta = await _mediator.Send(listar);
            return RespuestaPersonalizada(respuesta);
        }

        [Authorize(Roles = "User, Guest")]
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarUnaCategoria(int id)
        {
            Respuesta<Categoria> respuesta = await _mediator.Send(new BuscarCategoriaComando(id));
            return RespuestaPersonalizada(respuesta);
        }

        [HttpPost]
        public async Task<IActionResult> CrearCategoria(CrearCategoriaComando request)
        {
            Respuesta<Categoria> respuesta = await _mediator.Send(request);
            return RespuestaPersonalizada(respuesta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, ActualizarCategoriaComando request)
        {
            request = new ActualizarCategoriaComando(id, request.Nombre, request.Descripcion, request.Estado);

            Respuesta<Categoria> respuesta = await _mediator.Send(request);
            return RespuestaPersonalizada(respuesta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            Respuesta<bool> resultado = await _mediator.Send(new EliminarCategoriaComando(id));
            return RespuestaPersonalizada(resultado);
        }
    }
}
