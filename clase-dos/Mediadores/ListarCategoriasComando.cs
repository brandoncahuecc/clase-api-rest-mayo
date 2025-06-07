using clase_dos.Modelos;
using clase_dos.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace clase_dos.Mediadores
{
    public record ListarCategoriasComando() : IRequest<List<Categoria>>;

    public class ListarCategoriasHandler : IRequestHandler<ListarCategoriasComando, List<Categoria>>
    {
        private readonly AppDbContext _context;

        public ListarCategoriasHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> Handle(ListarCategoriasComando request, CancellationToken cancellationToken)
        {
            List<Categoria> categorias = await _context.Categorias.ToListAsync();

            return categorias;
        }
    }
}
