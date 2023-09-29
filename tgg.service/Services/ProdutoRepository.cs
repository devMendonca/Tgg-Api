using Microsoft.EntityFrameworkCore;
using Tgg.data;
using Tgg.data.Repositorio;
using Tgg.domain.Model;
using Tgg.Service.Services.Interfaces;

namespace Tgg.Service.Services
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(Contexto contexto) : base(contexto)
        {

        }
        public async Task<Produto> GetProdutosPorNome(string nome)
        {
            return await Get().FirstOrDefaultAsync(x => x.NomeProduto == nome);
        }
    }
}
