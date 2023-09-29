using Tgg.Models;

namespace Tgg.Services.Interfaces
{
    public interface IProdutos
    {
        Task<IEnumerable<ProdutoViewModel>> GetProdutosAsync(string token);
        Task<ProdutoViewModel> GetProdutoPorId(int id, string token);
        Task<ProdutoViewModel> CriarProduto(ProdutoViewModel produtoVm, string token);
        Task<bool> AutalizaProduto(int id, ProdutoViewModel produtoVm, string token);
        Task<bool> DeletarProduto(int id, string token);

    }
}
