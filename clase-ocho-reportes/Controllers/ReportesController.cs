using clase_cinco_biblioteca.Controllers;
using clase_ocho_reportes.Mediadores;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace clase_ocho_reportes.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : CustomeControllerBase
    {
        private readonly IMediator _mediator;

        public ReportesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{tipoReporte}/{id}")]
        public async Task<IActionResult> Get(string tipoReporte, int id)
        {
            var respuesta = await _mediator.Send(new GenerarReporteComando(tipoReporte, id));
            return RespuestaPersonalizada(respuesta);
        }
    }
}
