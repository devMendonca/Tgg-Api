using AutoMapper;
using Telefones_model.Model;
using Tgg.Api.DTO.Model;
using Tgg.domain.Model;

namespace Tgg.Api.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<Pedido, PedidoDto>().ReverseMap();
            CreateMap<Cliente, ClienteDto>().ReverseMap();

        }
    }
}
