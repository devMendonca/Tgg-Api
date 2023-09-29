using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telefones_model.Model;
using Tgg.data.Repositorio.Interfaces;

namespace Telefones_Service.Services.Interfaces
{
    public interface IClientesRepository : IRepository<Cliente>
    {
    }
}
