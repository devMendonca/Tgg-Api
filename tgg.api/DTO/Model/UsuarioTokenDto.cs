using System.ComponentModel.DataAnnotations;

namespace Tgg.Api.DTO.Model
{
    public class UsuarioTokenDto
    {
        [Key]
        public Guid Id { get; set; }
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
