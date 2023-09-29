using Telefones_Service.Services.Interfaces;
using Tgg.Service.Services.Interfaces;

namespace TggApi.UnityOfWork.Interfaces
{
    public interface IUnityOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        IPedidosRepository PedidosRepository { get; }

        IClientesRepository ClientesRepository { get; }

        Task Commit();

        void Dispose();
    }
}
