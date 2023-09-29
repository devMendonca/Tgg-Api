using Tgg.Models;

namespace Tgg.Services.Interfaces
{
    public interface IPedidos
    {
        Task<IEnumerable<PedidosViewModel>> GetPedidosAsync();
        Task<PedidosViewModel> GetPedidosByIdAsync(int id);
        Task<PedidosViewModel> CriarPedidoAsyn(PedidosViewModel pedido);
        Task<bool> AtualizarPedido(int id, PedidosViewModel pedido);
        Task<bool> DeletarPedido(int id);
    }
}
