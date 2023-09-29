using Microsoft.AspNetCore.Mvc;
using Tgg.Models;
using Tgg.Services.Interfaces;

namespace Tgg.Controllers
{
    public class PedidosController : Controller
    {
        private readonly IPedidos _ped;

        public PedidosController(IPedidos ped)
        {
            _ped = ped;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidosViewModel>>> Index()
        {
            var result = await _ped.GetPedidosAsync();

            if (result is null) return View("Error");

            return View(result);
        }

        [HttpGet]
        public IActionResult NovoPedido()
        {
            return View();
        }

        [HttpPost, ActionName("NovoPedido")]
        public async Task<ActionResult<PedidosViewModel>> NovoPedido(PedidosViewModel pedido)
        {
            if (ModelState.IsValid)
            {
                var result = await _ped.CriarPedidoAsyn(pedido);

                if (result != null) return RedirectToAction(nameof(Index));
            }

            return View("Erro");
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarPedido(int id)
        {
            var result = await _ped.GetPedidosByIdAsync(id);

            if (result is null) return View("Error");

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarPedido(int id, PedidosViewModel pedido)
        {
            if (ModelState.IsValid)
            {
                var result = await _ped.AtualizarPedido(id, pedido);

                if (result) return RedirectToAction(nameof(Index));
            }

            else ViewBag.Erro = "Erro ao atualizar Pedido";

            return View(pedido);
        }

       

        [HttpGet]
        public async Task<IActionResult> DeletarPedido(int id)
        {
            var result = await _ped.GetPedidosByIdAsync(id);

            if (result is null) return View("Error");

            return View(result);
        }

        [HttpPost(), ActionName("DeletarPedido")]
        public async Task<IActionResult> DeletarPedidoById(int id)
        {
            var result = await _ped.DeletarPedido(id);

            if(result) return RedirectToAction(nameof(Index));

            return View("Error");
        }




    }
}
