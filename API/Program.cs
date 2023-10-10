using FolhaPagamento.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// servico que vai ficar disponivel para ser chamado em algum lugar da aplicacao
builder.Services.AddDbContext<AppDataContext>( // vou passar minha classe ""contexto" | quando um objeto do meu contexto for construído eu passo as opções
    options => options.UseSqlite("Data Source= taskmanager.db;Cache=shared") // tem que saber a string de conexão do sqlite
    // ecommerce.db = arquivo que vai ser criado no meu projeto
); 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
