using Microsoft.EntityFrameworkCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Register DB
builder.Services.AddDbContext<CamelDb>();

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CamelValidator>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Create DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CamelDb>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Endpoints
app.MapPost("/api/camels", async (
    Camel camel,
    CamelDb db,
    FluentValidation.IValidator<Camel> validator) =>
{
    var validationResult = await validator.ValidateAsync(camel);

    if (!validationResult.IsValid)
    {
        return Results.BadRequest(validationResult.Errors);
    }

    db.Camels.Add(camel);
    await db.SaveChangesAsync();
    
    return Results.Created($"/api/camels/{camel.Id}", camel);
});

app.Run();

public partial class Program { }