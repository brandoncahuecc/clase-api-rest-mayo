using clase_tres_api_categoria.Modelos;
using clase_tres_api_categoria.Persistencia;
using MediatR;

namespace clase_tres_api_categoria.Mediadores
{
    public record CrearCategoriaComando(string Nombre, string Descripcion) : IRequest<Categoria>;

    public class CrearCategoriaHandler : IRequestHandler<CrearCategoriaComando, Categoria>
    {
        private readonly ICategoriaPersistencia _persistencia;

        public CrearCategoriaHandler(ICategoriaPersistencia persistencia)
        {
            _persistencia = persistencia;
        }

        public async Task<Categoria> Handle(CrearCategoriaComando request, CancellationToken cancellationToken)
        {
            Categoria categoria = new()
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                FechaCreacion = DateTime.Now,
                Estado = true
            };

            categoria = await _persistencia.Crear(categoria);

            return categoria;
        }
    }
}
