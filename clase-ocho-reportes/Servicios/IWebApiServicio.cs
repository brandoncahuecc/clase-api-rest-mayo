using clase_cinco_biblioteca.Modelos;
using clase_ocho_reportes.Modelos;
using Newtonsoft.Json;

namespace clase_ocho_reportes.Servicios
{
    public interface IWebApiServicio
    {
        Task<List<Categoria>> ObtenerCategorias();
        Task<string> ObtenerLogos();
    }

    public class WebApiServicio : IWebApiServicio
    {
        private readonly IHttpClientFactory _clientFactory;

        public WebApiServicio(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<List<Categoria>> ObtenerCategorias()
        {
            var cliente = _clientFactory.CreateClient("CategoriasCliente");
            var response = await cliente.GetAsync("Categorias");

            response.EnsureSuccessStatusCode();

            string respuesta = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Categoria>>(respuesta);
        }

        public async Task<string> ObtenerLogos()
        {
            var cliente = _clientFactory.CreateClient("ImagenesCliente");
            var response = await cliente.GetByteArrayAsync("67602131-categories-stamp-sign-text-word-logo-red.jpg");

            string respuesta = Convert.ToBase64String(response);

            return $"data:image/jpg;base64, {respuesta}";
        }
    }
}
