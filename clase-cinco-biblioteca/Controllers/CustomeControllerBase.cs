using clase_cinco_biblioteca.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace clase_cinco_biblioteca.Controllers
{
    public class CustomeControllerBase : ControllerBase
    {
        public IActionResult RespuestaPersonalizada<T>(Respuesta<T> respuesta)
        {
            return StatusCode(respuesta.CodigoEstado, respuesta.EsExitoso ? respuesta.Data : respuesta.Mensaje);
        }
    }
}
