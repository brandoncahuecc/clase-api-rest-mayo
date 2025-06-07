using clase_dos.Modelos;
using clase_dos.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clase_dos.Mediadores
{
    public record ActualizarCategoriaComando(int Id, string Nombre, string Descripcion, bool Estado) : IRequest<Categoria>;

    public class ActualizarCategoriaHandler : IRequestHandler<ActualizarCategoriaComando, Categoria>
    {
        private readonly AppDbContext _context;

        public ActualizarCategoriaHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Categoria> Handle(ActualizarCategoriaComando request, CancellationToken cancellationToken)
        {
            Categoria? categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (categoria is null)
                throw new Exception("No se encontro la categoria que necesita actualizar");

            categoria.Nombre = request.Nombre;
            categoria.Descripcion = request.Descripcion;
            categoria.Estado = request.Estado;

            await _context.SaveChangesAsync();

            return categoria;
        }
    }
}
