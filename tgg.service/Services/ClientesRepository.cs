using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telefones_model.Model;
using Telefones_Service.Services.Interfaces;
using Tgg.data;
using Tgg.data.Repositorio;

namespace Telefones_Service.Services
{
    public class ClientesRepository : Repository<Cliente>, IClientesRepository
    {
        public ClientesRepository(Contexto contexto) : base(contexto)
        {
        }
    }
}
