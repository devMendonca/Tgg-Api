using Tgg.Models.Auth;

namespace Tgg.Services.Interfaces;

    public interface IAuth
    {
       Task<TokenAuth> AutenticaUsuario(User user);

    }

