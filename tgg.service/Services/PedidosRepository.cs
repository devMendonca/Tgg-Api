using Microsoft.EntityFrameworkCore;
using Tgg.data;
using Tgg.data.Repositorio;
using Tgg.domain.Model;
using Tgg.Service.Services.Interfaces;

namespace Tgg.Service.Services
{
    public class PedidosRepository : Repository<Pedido>, IPedidosRepository
    {
        public PedidosRepository(Contexto contexto) : base(contexto)
        {
        }

        public async Task<IEnumerable<Pedido>> GetPedidosProdutos()
        {
            var prod = await Get().Include(x => x.Produto).ToListAsync();

            return prod;

        }
    }
}
