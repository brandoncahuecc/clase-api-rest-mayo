using clase_tres_api_categoria.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace clase_tres_api_categoria.Controllers
{
    public class CustomeControllerBase : ControllerBase
    {
        public IActionResult RespuestaPersonalizada<T>(Respuesta<T> respuesta)
        {
            return StatusCode(respuesta.CodigoEstado, respuesta.EsExitoso ? respuesta.Data : respuesta.Mensaje);
        }
    }
}
