using clase_cinco_biblioteca.Modelos;
using clase_tres_api_categoria.Modelos;
using clase_tres_api_categoria.Persistencia;
using MediatR;

namespace clase_tres_api_categoria.Mediadores
{
    public record ActualizarCategoriaComando(int Id, string Nombre, string Descripcion, bool Estado) : IRequest<Respuesta<Categoria>>;

    public class ActualizarCategoriaHandler : IRequestHandler<ActualizarCategoriaComando, Respuesta<Categoria>>
    {
        private readonly ICategoriaPersistencia _persistencia;

        public ActualizarCategoriaHandler(ICategoriaPersistencia persistencia)
        {
            _persistencia = persistencia;
        }

        public async Task<Respuesta<Categoria>> Handle(ActualizarCategoriaComando request, CancellationToken cancellationToken)
        {
            Respuesta<Categoria> categoria = await _persistencia.Buscar(request.Id);

            if (!categoria.EsExitoso)
                throw new Exception("No se encontro la categoria que necesita actualizar");

            categoria.Data.Nombre = request.Nombre;
            categoria.Data.Descripcion = request.Descripcion;
            categoria.Data.Estado = request.Estado;

            await _persistencia.Actualizar(categoria.Data);
            return categoria;
        }
    }
}
