using clase_cinco_biblioteca.Modelos;
using clase_tres_api_categoria.Modelos;
using clase_tres_api_categoria.Persistencia;
using MediatR;

namespace clase_tres_api_categoria.Mediadores
{
    public record ListarCategoriasComando() : IRequest<Respuesta<List<Categoria>>>;

    public class ListarCategoriasHandler : IRequestHandler<ListarCategoriasComando, Respuesta<List<Categoria>>>
    {
        private readonly ICategoriaPersistencia _persistencia;
        private readonly ICachePersistencia _cache;

        public ListarCategoriasHandler(ICategoriaPersistencia persistencia, ICachePersistencia cache)
        {
            _persistencia = persistencia;
            _cache = cache;
        }

        public async Task<Respuesta<List<Categoria>>> Handle(ListarCategoriasComando request, CancellationToken cancellationToken)
        {
            Respuesta<List<Categoria>> respuesta = new();

            List<Categoria> categoriasCache = await _cache.GetCache<List<Categoria>>("Categorias");

            if (categoriasCache is not null && categoriasCache.Count > 0)
                return respuesta.RespuestaExito(categoriasCache);

            respuesta = await _persistencia.Listar();

            if (respuesta.EsExitoso)
                await _cache.SetCache("Categorias", respuesta.Data);

            return respuesta;
        }
    }
}
