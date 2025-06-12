using clase_tres_api_categoria.Modelos;
using clase_tres_api_categoria.Persistencia;
using MediatR;

namespace clase_tres_api_categoria.Mediadores
{
    public record ListarCategoriasComando() : IRequest<List<Categoria>>;

    public class ListarCategoriasHandler : IRequestHandler<ListarCategoriasComando, List<Categoria>>
    {
        private readonly ICategoriaPersistencia _persistencia;

        public ListarCategoriasHandler(ICategoriaPersistencia persistencia)
        {
            _persistencia = persistencia;
        }

        public async Task<List<Categoria>> Handle(ListarCategoriasComando request, CancellationToken cancellationToken)
        {
            List<Categoria> categorias = await _persistencia.Listar();

            return categorias;
        }
    }
}
