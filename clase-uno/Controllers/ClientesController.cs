using clase_uno.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clase_uno.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private static List<Cliente> _clientes = new List<Cliente>();

        [HttpGet]
        public IActionResult GetCliente()
        {
            return Ok(_clientes);
        }

        [HttpPost]
        public IActionResult PostCliente(Cliente cliente)
        {
            int id = _clientes is null || _clientes.Count == 0
                ? 0 : _clientes.Max(c => c.Id);

            cliente.Id = id + 1;
            _clientes.Add(cliente);

            return Ok(cliente);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            Cliente? cliente = _clientes.FirstOrDefault(c => c.Id == id);

            if (cliente is null)
                return NotFound();

            _clientes.Remove(cliente);

            return Ok();
        }
    }
}
