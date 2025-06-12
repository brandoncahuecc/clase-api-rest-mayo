using clase_tres_api_categoria.Modelos;
using clase_tres_api_categoria.Persistencia;
using MediatR;

namespace clase_tres_api_categoria.Mediadores
{
    public record EliminarCategoriaComando(int Id) : IRequest<bool>;

    public class EliminarCategoriaHandler : IRequestHandler<EliminarCategoriaComando, bool>
    {
        private readonly ICategoriaPersistencia _persistencia;

        public EliminarCategoriaHandler(ICategoriaPersistencia persistencia)
        {
            _persistencia = persistencia;
        }

        public async Task<bool> Handle(EliminarCategoriaComando request, CancellationToken cancellationToken)
        {
            Categoria? categoria = await _persistencia.Buscar(request.Id);

            if (categoria is null)
                throw new DirectoryNotFoundException("No se encontro la categoria a eliminar");

            bool respuesta = await _persistencia.Eliminar(request.Id);
            return respuesta;
        }
    }
}
