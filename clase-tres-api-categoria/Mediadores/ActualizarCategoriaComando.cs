using clase_tres_api_categoria.Modelos;
using clase_tres_api_categoria.Persistencia;
using MediatR;

namespace clase_tres_api_categoria.Mediadores
{
    public record ActualizarCategoriaComando(int Id, string Nombre, string Descripcion, bool Estado) : IRequest<Categoria>;

    public class ActualizarCategoriaHandler : IRequestHandler<ActualizarCategoriaComando, Categoria>
    {
        private readonly ICategoriaPersistencia _persistencia;

        public ActualizarCategoriaHandler(ICategoriaPersistencia persistencia)
        {
            _persistencia = persistencia;
        }

        public async Task<Categoria> Handle(ActualizarCategoriaComando request, CancellationToken cancellationToken)
        {
            Categoria? categoria = await _persistencia.Buscar(request.Id);

            if (categoria is null)
                throw new Exception("No se encontro la categoria que necesita actualizar");

            categoria.Nombre = request.Nombre;
            categoria.Descripcion = request.Descripcion;
            categoria.Estado = request.Estado;

            await _persistencia.Actualizar(categoria);

            return categoria;
        }
    }
}
