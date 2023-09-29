using Microsoft.AspNetCore.Mvc;
using Tgg.Models.Auth;
using Tgg.Services.Interfaces;

namespace Tgg.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(User user)
        {
           if(!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Login Invalido");
                return View("Error");
            }

           var result = await _auth.AutenticaUsuario(user);

            if (result is null)
            {
                ModelState.AddModelError(string.Empty, "Login Invalido");
                return View("Error");
            }

            Response.Cookies.Append("X-Access-Token", result.Token, new CookieOptions()
            {
                Secure = true,
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            });

            return Redirect("/");
        }
    }
}
