using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Telefones_model.Model;
using Tgg.Api.Controllers;
using Tgg.Api.DTO.Model;
using TggApi.UnityOfWork.Interfaces;
using Xunit;

namespace Tgg.Test
{

    public class ClientesControllerTest
    {

        [Fact]
        public async Task TesteClienteVálidoParaPersistencia()
        {
            
            var uofMock = new Mock<IUnityOfWork>();
            var loggerMock = new Mock<ILogger<ClientesController>>();
            var mapperMock = new Mock<IMapper>();

            var clientesController = new ClientesController(uofMock.Object, loggerMock.Object, mapperMock.Object);

            var clienteDto = new ClienteDto
            {
                NomeCompleto = "TESTE I",
                Ativo = true,
                DataCadastro = DateTime.UtcNow,
                DataNascimento = new DateTime(1992,04,10),
                Cpf = "40378024817",
                Rg = "487258162",
                Endereco = "Rua Flavio Matiazo, 57"
            };

            Cliente clienteExistente = null;

            uofMock.Setup(u => u.ClientesRepository.GetById(It.IsAny<Expression<Func<Cliente, bool>>>()))
            .ReturnsAsync(clienteExistente);

          
            var result = await clientesController.PostAsync(clienteDto);

          
            var okResult = Assert.IsType<OkObjectResult>(result);
            

            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task TesteExceptionParaClienteJaExistente()
        {
            var uofMock = new Mock<IUnityOfWork>();
            var loggerMock = new Mock<ILogger<ClientesController>>();
            var mapperMock = new Mock<IMapper>();

            var clientesController = new ClientesController(uofMock.Object, loggerMock.Object, mapperMock.Object);

            var clienteDto = new ClienteDto
            {
                NomeCompleto = "TESTE II",
                Ativo = true,
                DataCadastro = DateTime.UtcNow,
                DataNascimento = new DateTime(1992, 04, 10),
                Cpf = "40378024817",
                Rg = "487258162",
                Endereco = "Rua Flavio Matiazo, 57"
            };

            var clienteExistente = new Cliente
            {
               
            };

            uofMock.Setup(u => u.ClientesRepository.GetById(It.IsAny<Expression<Func<Cliente, bool>>>()))
            .ReturnsAsync(clienteExistente);

            var result = await clientesController.PostAsync(clienteDto);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorMessage = Assert.IsType<string>(badRequestResult.Value);

            Assert.Contains("Cliente Já Existente na base de dados. NOME:", errorMessage);
        }
    }
}