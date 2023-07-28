using APIExercise.Core.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Text;

namespace APIExercise.Tests.IntegrationTests
{
    public class ClientControllerIntegrationTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task GetAllClients_ReturnsOkResponse()
        {
            var response = await _client.GetAsync("/clientes");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task GetClientById_ReturnsOkResponse()
        {
            // Use a known ID from your seed data or setup
            var clientId = new Guid("sample-guid-here");
            var response = await _client.GetAsync($"/clientes/{clientId}");
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task AddClient_ReturnsCreatedResponse()
        {
            var newClient = new ClientCreateDto
            {
                FirstName = "Test Name",
                PhoneNumber = "123456789",
                Address = new ClientAddresDto(),
                Password = "test1234"
            };

            var jsonContent = JsonConvert.SerializeObject(newClient);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/clientes", stringContent);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task UpdateClient_ReturnsNoContentResponse()
        {
            var clientId = new Guid("sample-guid-here");
            var updatedClient = new ClientUpdateDto
            {
                FirstName = "Updated Name",
                PhoneNumber = "987654321"
            };

            var jsonContent = JsonConvert.SerializeObject(updatedClient);
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"/clientes/{clientId}", stringContent);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task DeleteClient_ReturnsNoContentResponse()
        {
            var clientId = new Guid("sample-guid-to-delete");
            var response = await _client.DeleteAsync($"/clientes/{clientId}");
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
