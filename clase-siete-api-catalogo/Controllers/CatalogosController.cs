using clase_siete_api_catalogo.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clase_siete_api_catalogo.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private static readonly List<Catalogo> _catalogos = new()
        {
            new Catalogo() { Codigo = 1, Descripcion = "Uno"},
            new Catalogo() { Codigo = 2, Descripcion = "Dos"},
            new Catalogo() { Codigo = 3, Descripcion = "Tres"},
            new Catalogo() { Codigo = 4, Descripcion = "Cuatro"},
            new Catalogo() { Codigo = 5, Descripcion = "Cinco"},
            new Catalogo() { Codigo = 6, Descripcion = "Seis"},
            new Catalogo() { Codigo = 7, Descripcion = "Siete"},
        };

        [HttpGet]
        public IActionResult Obtener()
        {
            return Ok(_catalogos);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerUno(int id)
        {
            Catalogo? catalogo = _catalogos.FirstOrDefault(c => c.Codigo == id);
            return Ok(catalogo);
        }
    }
}
