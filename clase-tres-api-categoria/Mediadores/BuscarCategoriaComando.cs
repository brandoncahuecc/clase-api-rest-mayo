using clase_cinco_biblioteca.Modelos;
using clase_tres_api_categoria.Modelos;
using clase_tres_api_categoria.Persistencia;
using MediatR;

namespace clase_tres_api_categoria.Mediadores
{
    public record BuscarCategoriaComando(int Id) : IRequest<Respuesta<Categoria>>;

    public class BuscarCategoriaHandler : IRequestHandler<BuscarCategoriaComando, Respuesta<Categoria>>
    {
        private readonly ICategoriaPersistencia _persistencia;

        public BuscarCategoriaHandler(ICategoriaPersistencia persistencia)
        {
            _persistencia = persistencia;
        }

        public async Task<Respuesta<Categoria>> Handle(BuscarCategoriaComando request, CancellationToken cancellationToken)
        {
            Respuesta<Categoria> categoria = await _persistencia.Buscar(request.Id);
            return categoria;
        }
    }
}
