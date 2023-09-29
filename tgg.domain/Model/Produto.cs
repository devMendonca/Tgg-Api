using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tgg.domain.Model
{
    public class Produto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProdutoId { get; set; }
        public string? NomeProduto { get; set; }
        public double? Preco { get; set; }

        [JsonIgnore]
        public ICollection<Pedido>? Produtos { get; set; }

    }
}
