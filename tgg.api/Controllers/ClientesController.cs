using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Telefones_model.Model;
using Tgg.Api.DTO.Model;
using TggApi.UnityOfWork.Interfaces;

namespace Tgg.Api.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IUnityOfWork _uof;
        private readonly ILogger<ClientesController> _logger;
        private readonly IMapper _mapper;

        public ClientesController(IUnityOfWork uof, ILogger<ClientesController> logger, IMapper mapper)
        {
            _uof = uof;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
        {
            try
            {
                var clientes = await _uof.ClientesRepository.Get().ToListAsync();
                var clientesDto = _mapper.Map<List<ClienteDto>>(clientes);

                if (clientes is null) return NotFound();

                return clientesDto;

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<ClienteDto>> GetById(int id)
        {
            try
            {
                var cliente = await _uof.ClientesRepository.GetById(x => x.ClienteId == id);
                var clienteDto = _mapper.Map<ClienteDto>(cliente);

                if (clienteDto is null) return NotFound("Produto não encontrado");

                return clienteDto;

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }

        [HttpPost]
        public async Task<ActionResult> PostAsync(ClienteDto cli)
        {
            if (cli is null) return BadRequest();

            try
            {
                var clienteExistente = await _uof.ClientesRepository.GetById(x => x.NomeCompleto == cli.NomeCompleto);

                if(null == clienteExistente)
                {
                    var cliente = _mapper.Map<Cliente>(cli);

                    _uof.ClientesRepository.Add(cliente);
                    await _uof.Commit();

                    return Ok(cliente);
                }
                else
                {
                    _logger.LogWarning($"Cliente Já Existente na base de dados. NOME: {clienteExistente.NomeCompleto}");
                    return BadRequest($"Cliente Já Existente na base de dados. NOME: {clienteExistente.NomeCompleto}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ClienteDto cli)
        {
            try
            {
                if (id != cli.ClienteId) return BadRequest();

                var cliente = _mapper.Map<Cliente>(cli);
                _uof.ClientesRepository.Update(cliente);
                await _uof.Commit();

                return Ok(cliente);

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, ClienteDto cli)
        {

            try
            {
                if (id != cli.ClienteId) return BadRequest();

                var cliente = _mapper.Map<Cliente>(cli);

                _uof.ClientesRepository.Update(cliente);
                await _uof.Commit();

                return Ok(cliente);

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
                var cli = await _uof.ClientesRepository.GetById(x => x.ClienteId == id);

                if (cli is null) return NotFound();

                _uof.ClientesRepository.Delete(cli);
                await _uof.Commit();

                return Ok(cli);

            }
            catch (Exception e)
            {
                _logger.LogError($"Erro: {e.Message}, {e.StackTrace}");
                throw;
            }

        }




    }
}
