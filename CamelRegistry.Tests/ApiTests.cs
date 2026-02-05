using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace CamelRegistry.Tests;

public class UnitTest1
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
}
