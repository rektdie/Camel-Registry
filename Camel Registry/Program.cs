using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register DB
builder.Services.AddDbContext<CamelDb>();

var app = builder.Build();

// Create DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CamelDb>();
    db.Database.Migrate();
}

app.MapGet("/", () => "Hello World!");

app.Run();
