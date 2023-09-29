using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Tgg.Api.DTO.Model;
using Tgg.domain.Model;
using TggApi.UnityOfWork.Interfaces;

namespace TggApi.Controllers
{
   // [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[Controller]")]
    [ApiController]
    [EnableQuery]
    public class PedidosController : Controller
    {
        private readonly IUnityOfWork _uof;
        private readonly ILogger<PedidosController> _logger;
        private readonly IMapper _mapper;

        public PedidosController(IUnityOfWork uof,
                                 ILogger<PedidosController> logger,
                                 IMapper mapper)
        {
            _uof = uof;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> Get()
        {
            try
            {
                var pedidos =await  _uof.PedidosRepository.Get().ToListAsync();
                var pedidosDto = _mapper.Map<List<PedidoDto>>(pedidos);

                if (pedidos is null) return NotFound();

                return pedidosDto;

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }
          
        }


        [HttpGet("{id:int}", Name = "ObterPedidos")]
        public async Task<ActionResult<PedidoDto>> GetById(int id)
        {
            try
            {
                var ped = await _uof.PedidosRepository.GetById(x => x.pedidoId == id);
                var pedidosDto = _mapper.Map<PedidoDto>(ped);

                if (ped is null) return NotFound();

                return pedidosDto;
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }
           
        }


        [HttpGet("PedidosProdutos")]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> GetPedidosProdutos()
        {
            try
            {
                var ped = await _uof.PedidosRepository.GetPedidosProdutos();
                var pedidosDto = _mapper.Map<List<PedidoDto>>(ped);

                return pedidosDto;
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(PedidoDto pedido)
        {
            try
            {
                var ped = _mapper.Map<Pedido>(pedido);
                if (ped is null) return BadRequest();

                _uof.PedidosRepository.Add(ped);
                await _uof.Commit();

                return Ok(ped);

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, PedidoDto pedido)
        {
            try
            {

                if (id != pedido.pedidoId) return BadRequest();

                var ped = _mapper.Map<Pedido>(pedido);

                _uof.PedidosRepository.Update(ped);
                await _uof.Commit();

                return Ok(ped);

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;

            }

        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, Pedido pedido)
        {
            try
            {
                if (id != pedido.pedidoId) return BadRequest();

                var ped = _mapper.Map<Pedido>(pedido);

                _uof.PedidosRepository.Update(ped);
                await _uof.Commit();

                return Ok(ped);

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }
          
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var ped = await  _uof.PedidosRepository.GetById(x => x.pedidoId == id);

                if (ped is null) return NotFound();

                _uof.PedidosRepository.Delete(ped);
               await _uof.Commit();

                return Ok(ped);

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }
         
        }
    }
}
