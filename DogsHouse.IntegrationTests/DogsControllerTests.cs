using System.Net;
using System.Net.Http.Json;
using DogsHouse.Application.Dto;
using Xunit;

namespace DogsHouse.IntegrationTests;

public class DogsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DogsControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Ping_ReturnsOk()
    {
        var response = await _client.GetAsync("/ping");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Dogshouseservice.Version1.0.1", content);
    }

    [Fact]
    public async Task CreateDog_And_GetDogs_Works()
    {
        var newDog = new CreateDogDto
        {
            Name = "TestDog",
            Color = "Black",
            TailLength = 10,
            Weight = 20
        };

        var postResponse = await _client.PostAsJsonAsync("/dog", newDog);
        Assert.Equal(HttpStatusCode.OK, postResponse.StatusCode);

        var getResponse = await _client.GetAsync("/dogs");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var dogs = await getResponse.Content.ReadFromJsonAsync<List<DogDto>>();
        Assert.Contains(dogs, d => d.Name == "TestDog");
    }
}
