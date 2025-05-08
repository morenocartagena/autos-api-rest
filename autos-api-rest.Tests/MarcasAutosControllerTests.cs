using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using autos_api_rest.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using autos_api_rest.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using Microsoft.AspNetCore.TestHost;

namespace autos_api_rest.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<PostgreSqlDbContext>));
                services.RemoveAll(typeof(PostgreSqlDbContext));

                services.AddDbContext<PostgreSqlDbContext>(options =>
                    options.UseInMemoryDatabase("InMemoryDbForTesting"));

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<PostgreSqlDbContext>();
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();
                }
            });
        }
    }

    public class MarcasAutosControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public MarcasAutosControllerTests(CustomWebApplicationFactory factory)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetMarcasAutos_ReturnsExpectedData()
        {
            var response = await _client.GetAsync("/autos-api-rest/v1/listar-marcas-autos");
            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();
            var marcas = JsonConvert.DeserializeObject<List<MarcaAuto>>(jsonString);
            Assert.NotNull(marcas);
            Assert.NotEmpty(marcas);
            var expectedMarcas = new List<string> { "Hyundai", "Nissan", "Toyota", "Ford", "Chevrolet" };
            var actualMarcas = marcas.Select(m => m.Marca).ToList();
            Assert.Equal(expectedMarcas.Count, actualMarcas.Count);
            Assert.All(expectedMarcas, marca => Assert.Contains(marca, actualMarcas));
        }
    }
}
