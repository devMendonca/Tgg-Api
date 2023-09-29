using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tgg.Api.DTO.Model;
using Tgg.domain.Model;
using TggApi.UnityOfWork.Interfaces;

namespace TggApi.Controllers
{
   // [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[Controller]")]
    [ApiController]

    public class ProdutosController : ControllerBase
    {
        private readonly IUnityOfWork _uof;
        private readonly ILogger<ProdutosController> _logger;
        private readonly IMapper _mapper;


        public ProdutosController(IUnityOfWork uof,
                                  ILogger<ProdutosController> logger,
                                  IMapper map)
        {
            _uof = uof;
            _logger = logger;
            _mapper = map;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDto>>> Get()
        {
            try
            {
                var produtos = await _uof.ProdutoRepository.Get().ToListAsync();
                var produtosDto = _mapper.Map<List<ProdutoDto>>(produtos);

                if (produtos is null) return NotFound();

                return produtosDto;

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }

        [HttpGet("{id:int}", Name = "BuscarProdutoPeloId")]
        public async Task<ActionResult<ProdutoDto>> GetById(int id)
        {
            try
            {
                var prod = await _uof.ProdutoRepository.GetById(x => x.ProdutoId == id);
                var produtosDto = _mapper.Map<ProdutoDto>(prod);

                if (produtosDto is null) return NotFound("Produto não encontrado");

                return produtosDto;

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }

        [HttpGet("{nome:alpha}", Name = "ObterProdutoPeloNome")]
        public async Task<ActionResult<ProdutoDto>> GetProdutosPorNome(string nome)
        {
            try
            {
                var prod = await _uof.ProdutoRepository.GetProdutosPorNome(nome);
                var produtosDto = _mapper.Map<ProdutoDto>(prod);
                if (produtosDto is null) return NotFound("Produto não encontrado");

                return produtosDto;

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(ProdutoDto produto)
        {
            if (produto is null) return BadRequest();

            try
            {
                var prod = _mapper.Map<Produto>(produto);

                _uof.ProdutoRepository.Add(prod);
                await _uof.Commit();

                return Ok(prod);

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }


        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ProdutoDto produto)
        {
            try
            {
                if (id != produto.ProdutoId) return BadRequest();

                var prod = _mapper.Map<Produto>(produto);
                _uof.ProdutoRepository.Update(prod);
                await _uof.Commit();

                return Ok(prod);

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, Produto produto)
        {

            try
            {
                if (id != produto.ProdutoId) return BadRequest();

                var prod = _mapper.Map<Produto>(produto);

                _uof.ProdutoRepository.Update(prod);
                await _uof.Commit();

                return Ok(prod);

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
                var prod = await _uof.ProdutoRepository.GetById(x => x.ProdutoId == id);

                if (prod is null) return NotFound();

                _uof.ProdutoRepository.Delete(prod);
                await _uof.Commit();

                return Ok(prod);

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }



    }
}
