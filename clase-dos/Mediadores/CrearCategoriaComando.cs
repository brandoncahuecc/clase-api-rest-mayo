using clase_dos.Modelos;
using clase_dos.Persistencia;
using MediatR;

namespace clase_dos.Mediadores
{
    public record CrearCategoriaComando(string Nombre, string Descripcion) : IRequest<Categoria>;

    public class CrearCategoriaHandler : IRequestHandler<CrearCategoriaComando, Categoria>
    {
        private readonly AppDbContext _context;

        public CrearCategoriaHandler(AppDbContext context)
        {
            _context = context;
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

            await _context.AddAsync(categoria);
            await _context.SaveChangesAsync(cancellationToken);

            return categoria;
        }
    }

}
