using clase_dos.Modelos;
using clase_dos.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clase_dos.Mediadores
{
    public record EliminarCategoriaComando(int Id) : IRequest;

    public class EliminarCategoriaHandler : IRequestHandler<EliminarCategoriaComando>
    {
        private readonly AppDbContext _context;

        public EliminarCategoriaHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task Handle(EliminarCategoriaComando request, CancellationToken cancellationToken)
        {
            Categoria? categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == request.Id);

            if (categoria is null)
                throw new DirectoryNotFoundException("No se encontro la categoria a eliminar");

            _context.Remove(categoria);
            await _context.SaveChangesAsync();
            return;
        }
    }

}
