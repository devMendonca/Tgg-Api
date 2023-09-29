using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Tgg.domain.Model;

namespace Tgg.Api.DTO.Model
{
    public class ProdutoDto
    {

        [Key]
        public int ProdutoId { get; set; }
        public string? NomeProduto { get; set; }
        public double? Preco { get; set; }

        [JsonIgnore]
        public ICollection<Pedido> Produtos { get; set; }


    }
}
