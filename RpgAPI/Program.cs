global using RpgAPI.Models;
using Microsoft.EntityFrameworkCore;
using RpgAPI.Data;
using RpgAPI.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Registering DataContext and configuring connection to our SQL Server database using a ConnectionString
builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registering AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Registering our CharacterService Class
/* Now the Web API knows that it has to use the CharacterService Class whenever A Controller 
 *    wants to Inject the ICharacterService Interface!!! 
 */
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
