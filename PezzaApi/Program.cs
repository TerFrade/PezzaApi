using DataAccess;
using Microsoft.EntityFrameworkCore;

using PezzaApi.Menu.Interfaces;
using PezzaApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Configure DbContext with in-memory database
services.AddDbContext<PezzaDbContext>(options =>
    options.UseInMemoryDatabase("PezzaDb"));

// Add services to the container.
services.AddScoped<IPizzaHandler, PizzaHandler>();

services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("CorsPolicy");
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pezza API v1");
        c.RoutePrefix = "swagger";
    });
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();