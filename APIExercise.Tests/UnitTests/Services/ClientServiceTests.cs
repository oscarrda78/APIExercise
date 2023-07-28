using NUnit.Framework;
using Moq;
using APIExercise.Core.Interfaces.Repositories;
using APIExercise.Core.Entities;
using APIExercise.Core.DTOs;
using AutoMapper;
using APIExercise.Infrastructure.Implementations.Services;

namespace APIExercise.Tests.UnitTests.Services
{
    public class ClientServiceTests
    {
        private Mock<IClientRepository> _mockRepository;
        private Mock<IMapper> _mockMapper;
        private ClientService _service;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IClientRepository>();
            _mockMapper = new Mock<IMapper>();

            _mockRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<Client>
                {
                    new Client { },
                });
            var clientId = Guid.NewGuid();
            _mockRepository.Setup(repo => repo.GetByIdAsync(clientId))
                .ReturnsAsync(new Client { });

            _mockMapper.Setup(mapper => mapper.Map<ClientReadDto>(It.IsAny<object>()))
                .Returns(new ClientReadDto { });

            _service = new ClientService(_mockRepository.Object, _mockMapper.Object);
        }

        [Test]
        public void GetAllAsync_ReturnsListOfClients()
        {
            var result = _service.GetAllAsync().Result;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ClientReadDto>>(result);
            Assert.IsTrue(result.Any()); 
        }

        [Test]
        public void GetAsync_ReturnsClient_WhenIdIsValid()
        {
            var clientId = Guid.NewGuid();

            var result = _service.GetByIdAsync(clientId).Result;

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ClientReadDto>(result);
        }

        [Test]
        public void AddAsync_AddsClientSuccessfully()
        {
            var clientCreateDto = new ClientCreateDto { };

            var result = _service.AddAsync(clientCreateDto).Result;

        }

        [Test]
        public void UpdateAsync_UpdatesClientSuccessfully()
        {
            var clientId = Guid.NewGuid();
            var clientUpdateDto = new ClientUpdateDto {};

            _service.UpdateAsync(clientId, clientUpdateDto);

            _mockRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Client>()), Times.Once);
        }

        [Test]
        public void DeleteAsync_DeletesClientSuccessfully()
        {
            var clientId = Guid.NewGuid();

            _service.DeleteAsync(clientId);

            _mockRepository.Verify(repo => repo.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }
    }
}
