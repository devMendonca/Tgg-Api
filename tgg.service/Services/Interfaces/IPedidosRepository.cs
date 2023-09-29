using Microsoft.AspNetCore.Mvc;
using Tgg.data.Repositorio.Interfaces;
using Tgg.domain.Model;

namespace Tgg.Service.Services.Interfaces
{
    public interface IPedidosRepository : IRepository<Pedido>
    {
        Task<IEnumerable<Pedido>> GetPedidosProdutos();
    }
}
