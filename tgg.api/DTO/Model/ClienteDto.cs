using System.ComponentModel.DataAnnotations;
using Telefones_model.Model;

namespace Tgg.Api.DTO.Model
{
    public class ClienteDto
    {
        public int ClienteId { get; set; }
        public string NomeCompleto { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Endereco { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }
    }
}

