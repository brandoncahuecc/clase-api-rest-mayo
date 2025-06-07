using clase_dos.Modelos;
using clase_dos.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clase_dos.Mediadores
{
    public record BuscarCategoriaComando(int Id) : IRequest<Categoria>;

    public class BuscarCategoriaHandler : IRequestHandler<BuscarCategoriaComando, Categoria>
    {
        private readonly AppDbContext _context;

        public BuscarCategoriaHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Categoria> Handle(BuscarCategoriaComando request, CancellationToken cancellationToken)
        {
            Categoria categoria = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == request.Id);
            
            return categoria;
        }
    }
}
