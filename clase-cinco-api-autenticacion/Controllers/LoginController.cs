using clase_cinco_api_autenticacion.Mediadores;
using clase_cinco_api_autenticacion.Modelos;
using clase_cinco_biblioteca.Controllers;
using clase_cinco_biblioteca.Modelos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clase_cinco_api_autenticacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : CustomeControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(IniciarSesionComando request)
        {
            Respuesta<TokenJwt> respuesta = await _mediator.Send(request);
            return RespuestaPersonalizada(respuesta);
        }
    }
}
