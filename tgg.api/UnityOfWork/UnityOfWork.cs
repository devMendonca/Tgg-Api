using Telefones_Service.Services;
using Telefones_Service.Services.Interfaces;
using Tgg.data;
using Tgg.Service.Services;
using Tgg.Service.Services.Interfaces;
using TggApi.UnityOfWork.Interfaces;

namespace TggApi.UnityOfWork
{
    public class UnityOfWork : IUnityOfWork
    {
        private ProdutoRepository _produtoRepository;
        private PedidosRepository _pedidosRepository;
        private ClientesRepository _clientesRepository;
        public Contexto _db;

        public UnityOfWork(Contexto db)
        {
            _db = db;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_db);
            }
        }
        public IPedidosRepository PedidosRepository
        {
            get
            {
                return _pedidosRepository = _pedidosRepository ?? new PedidosRepository(_db);
            }
        }

        public IClientesRepository ClientesRepository
        {
            get
            {
                return _clientesRepository = _clientesRepository ?? new ClientesRepository(_db);
            }
        }

       
        public async Task Commit()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
