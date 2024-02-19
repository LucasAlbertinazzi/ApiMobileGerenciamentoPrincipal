using API_AppGerenciamento.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar a configuração do aplicativo
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

// Obter a string de conexão do arquivo de configuração
var connectionString = builder.Configuration.GetConnectionString("ConnectionDbContext");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adicionar o DbContext ao contêiner
builder.Services.AddDbContext<GerenciamentoContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
