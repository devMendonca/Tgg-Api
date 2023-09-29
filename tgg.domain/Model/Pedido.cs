using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tgg.domain.Model
{
    public class Pedido 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int pedidoId { get; set; }
        public string NomeComprador { get; set; }
        public double PrecoTotal { get; set; }
        public string Telefone { get; set; }
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; }

    }
}
