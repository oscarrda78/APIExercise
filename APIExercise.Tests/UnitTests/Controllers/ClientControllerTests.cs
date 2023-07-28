using NUnit.Framework;
using Moq;
using APIExercise.API.Controllers;
using APIExercise.Core.Interfaces.Services;
using APIExercise.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace APIExercise.Tests.UnitTests.Controllers
{
    public class ClientControllerTests
    {
        private Mock<IClientService> _mockService;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<ClientController>> _mockLogger;
        private ClientController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IClientService>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<ClientController>>();

            _mockService.Setup(service => service.GetAllAsync())
                .ReturnsAsync(new List<ClientReadDto>
                {
                    new ClientReadDto { Id = Guid.NewGuid() }
                });

            var clientId = Guid.NewGuid();
            _mockService.Setup(service => service.GetByIdAsync(clientId))
                .ReturnsAsync(new ClientReadDto { Id = clientId});

            _mockMapper.Setup(mapper => mapper.Map<ClientReadDto>(It.IsAny<object>()))
                .Returns(new ClientReadDto { });

            _controller = new ClientController(_mockService.Object, _mockLogger.Object);
        }

        [Test]
        public void GetAllClients_ReturnsOk_WithListOfClients()
        {
            var result = _controller.GetAllClients().Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ClientReadDto>>(result.Value);
        }

        [Test]
        public void GetClientById_ReturnsClient_WhenIdIsValid()
        {
            var clientId = Guid.NewGuid();

            var result = _controller.GetClientById(clientId).Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ClientReadDto>(result.Value);
            Assert.AreEqual(clientId, (result.Value as ClientReadDto).Id);
        }

        [Test]
        public void CreateClient_ReturnsCreatedAtAction_WhenCreationIsSuccessful()
        {
            var clientCreateDto = new ClientCreateDto {};

            var result = _controller.CreateClient(clientCreateDto).Result as CreatedAtActionResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ClientReadDto>(result.Value);
        }

        [Test]
        public void UpdateClient_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            var clientId = Guid.NewGuid();
            var clientUpdateDto = new ClientUpdateDto {  };

            var result = _controller.UpdateClient(clientId, clientUpdateDto).Result as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode); 
        }

        [Test]
        public void DeleteClient_ReturnsNoContent_WhenDeletionIsSuccessful()
        {
            var clientId = Guid.NewGuid();

            var result = _controller.DeleteClient(clientId).Result as NoContentResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(204, result.StatusCode);
        }
    }
}
