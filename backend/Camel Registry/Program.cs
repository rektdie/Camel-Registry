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

// CORS
builder.Services.AddCors(o => {
    o.AddDefaultPolicy(p => p
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod());
});

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

app.MapGet("api/camels", async (CamelDb db) =>
{
    return await db.Camels.ToListAsync();
});

app.MapGet("api/camels/{id}", async (int id, CamelDb db) =>
{
    var camel = await db.Camels.FindAsync(id);

    return camel is null
        ? Results.NotFound()
        : Results.Ok(camel);
});

app.MapPut("api/camels/{id}", async (int id, Camel inputCamel, CamelDb db) =>
{
    var camel = await db.Camels.FindAsync(id);

    if (camel is null)
        return Results.NotFound();

    camel.Name = inputCamel.Name;
    camel.Color = inputCamel.Color;
    camel.HumpCount = inputCamel.HumpCount;
    camel.LastFed = inputCamel.LastFed;

    await db.SaveChangesAsync();

    return Results.Ok(camel);
});

app.MapDelete("/api/camels/{id}", async (int id, CamelDb db) =>
{
    var camel = await db.Camels.FindAsync(id);
    if (camel is null)
        return Results.NotFound();

    db.Camels.Remove(camel);
    await db.SaveChangesAsync();

    return Results.NoContent();
});


app.Run();

public partial class Program { }