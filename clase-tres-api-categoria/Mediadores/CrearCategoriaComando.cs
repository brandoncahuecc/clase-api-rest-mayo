using clase_tres_api_categoria.Modelos;
using clase_tres_api_categoria.Persistencia;
using MediatR;

namespace clase_tres_api_categoria.Mediadores
{
    public record CrearCategoriaComando(string Nombre, string Descripcion) : IRequest<Respuesta<Categoria>>;

    public class CrearCategoriaHandler : IRequestHandler<CrearCategoriaComando, Respuesta<Categoria>>
    {
        private readonly ICategoriaPersistencia _persistencia;

        public CrearCategoriaHandler(ICategoriaPersistencia persistencia)
        {
            _persistencia = persistencia;
        }

        public async Task<Respuesta<Categoria>> Handle(CrearCategoriaComando request, CancellationToken cancellationToken)
        {
            Categoria categoria = new()
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                FechaCreacion = DateTime.Now,
                Estado = true
            };

            Respuesta<Categoria> respuesta = await _persistencia.Crear(categoria);
            return respuesta;
        }
    }
}
