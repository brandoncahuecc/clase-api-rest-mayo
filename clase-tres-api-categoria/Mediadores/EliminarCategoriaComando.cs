using clase_cinco_biblioteca.Modelos;
using clase_tres_api_categoria.Modelos;
using clase_tres_api_categoria.Persistencia;
using MediatR;

namespace clase_tres_api_categoria.Mediadores
{
    public record EliminarCategoriaComando(int Id) : IRequest<Respuesta<bool>>;

    public class EliminarCategoriaHandler : IRequestHandler<EliminarCategoriaComando, Respuesta<bool>>
    {
        private readonly ICategoriaPersistencia _persistencia;

        public EliminarCategoriaHandler(ICategoriaPersistencia persistencia)
        {
            _persistencia = persistencia;
        }

        public async Task<Respuesta<bool>> Handle(EliminarCategoriaComando request, CancellationToken cancellationToken)
        {
            Respuesta<Categoria> categoria = await _persistencia.Buscar(request.Id);
            Respuesta<bool> respuesta = new();
            if (!categoria.EsExitoso)
                return respuesta.RespuestaError(404, categoria.Mensaje);

            respuesta = await _persistencia.Eliminar(request.Id);
            return respuesta;
        }
    }
}
