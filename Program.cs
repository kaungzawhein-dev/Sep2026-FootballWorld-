
using FootballWorld.Services;
using Microsoft.OpenApi.Models;
using Sep2026.FootballWorld.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Register Database Context
builder.Services.AddScoped<FootballDbContext>();

// Register Football Service
builder.Services.AddScoped<FootballService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FootballWorld API",
        Version = "v1",
        Description = "Football World Information API"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "FootballWorld API V1"
        );
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
