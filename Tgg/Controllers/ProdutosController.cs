using Microsoft.AspNetCore.Mvc;
using Tgg.Models;
using Tgg.Services.Interfaces;

namespace Tgg.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutos _prd;
        private string token = string.Empty;

        public ProdutosController(IProdutos prd)
        {
            _prd = prd;
        }

        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
        {
            var result = await _prd.GetProdutosAsync(ObterToken());

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        public ActionResult NovoProduto()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> NovoProduto(ProdutoViewModel produto)
        {
            var result = await _prd.CriarProduto(produto, ObterToken());

            if (result is null) return View("Error");

            return View(result);
        }

        [HttpGet]
        public async Task<ActionResult> DeletarProduto(int id)
        {

            var result = await _prd.GetProdutoPorId(id, ObterToken());

            if (result is null) return View("Error");

            return View(result);
        }

        [HttpPost, ActionName("DeletarProduto")]

        public async Task<ActionResult> DeletarProdutoPorId(int id)
        {
            var result = await _prd.DeletarProduto(id, ObterToken());

            if (result) return RedirectToAction(nameof(Index));

            return View("Error");
        }

        [HttpGet]
        public async Task<ActionResult> AtualizarProduto(int id)
        {

            var result = await _prd.GetProdutoPorId(id, ObterToken());

            if (result is null) return View("Error");

            return View(result);
        }

        [HttpPost]

        public async Task<ActionResult> AtualizarProduto(int id, ProdutoViewModel produto)
        {
            var result = await _prd.AutalizaProduto(id, produto, ObterToken());

            if (result) return RedirectToAction(nameof(Index));

            return View("Error");
        }

        private string ObterToken()
        {
            if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
                token = HttpContext.Request.Cookies["X-Access-Token"].ToString();

            return token;
        }
    }
}
