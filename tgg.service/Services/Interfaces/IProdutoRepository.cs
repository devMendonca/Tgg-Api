using Microsoft.AspNetCore.Mvc;
using Tgg.data.Repositorio.Interfaces;
using Tgg.domain.Model;

namespace Tgg.Service.Services.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> GetProdutosPorNome(string nome);
    }
}
