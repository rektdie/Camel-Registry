using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CamelRegistry.Tests;

public class ApiTests
{
    [Fact]
    public async Task PostCamel()
    {
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var camel = new
        {
            name = "Lajos",
            color = 0u,
            humpCount = 1,
            lastFed = DateTime.UtcNow
        };

        var response = await client.PostAsJsonAsync("/api/camels", camel);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task GetCamels()
    {
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var response = await client.GetAsync("/api/camels");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetCamelById()
    {
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var camel = new
        {
            name = "Egon",
            color = 0u,
            humpCount = 1,
            lastFed = DateTime.UtcNow
        };

        var post = await client.PostAsJsonAsync("/api/camels", camel);
        var created = await post.Content.ReadFromJsonAsync<Camel>();

        var get = await client.GetAsync($"/api/camels/{created!.Id}");

        Assert.Equal(HttpStatusCode.OK, get.StatusCode);
    }

    [Fact]
    public async Task PutCamel()
    {
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var create = new
        {
            name = "Egon",
            color = 0u,
            humpCount = 1,
            lastFed = DateTime.UtcNow
        };

        var post = await client.PostAsJsonAsync("/api/camels", create);

        var created = await post.Content.ReadFromJsonAsync<Camel>();

        var update = new
        {
            id = created!.Id,
            name = "Szilveszter",
            color = 123u,
            humpCount = 2,
            lastFed = DateTime.UtcNow
        };

        var put = await client.PutAsJsonAsync($"/api/camels/{created.Id}", update);
        Assert.Equal(HttpStatusCode.OK, put.StatusCode);
    }

    [Fact]
    public async Task DeleteCamel()
    {
        var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();

        var create = new
        {
            name = "Karcsi",
            color = 0u,
            humpCount = 2,
            lastFed = DateTime.UtcNow
        };

        var post = await client.PostAsJsonAsync("/api/camels", create);

        var created = await post.Content.ReadFromJsonAsync<Camel>();

        var del = await client.DeleteAsync($"/api/camels/{created!.Id}");
        Assert.Equal(HttpStatusCode.NoContent, del.StatusCode);

        var get = await client.GetAsync($"/api/camels/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, get.StatusCode);
    }

}
