using System.ComponentModel.DataAnnotations;
using Tgg.domain.Model;

namespace Tgg.Api.DTO.Model
{
    public class PedidoDto
    {
        [Key]
        public int pedidoId { get; set; }
        public string? NomeComprador { get; set; }
        public double? PrecoTotal { get; set; }
        public string? Telefone { get; set; }
        public int? ProdutoId { get; set; }
        public Produto Produto { get; set; }
    }
}
