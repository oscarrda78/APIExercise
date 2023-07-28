using APIExercise.API.Mapping;
using APIExercise.API.Middleware;
using APIExercise.Core.Interfaces.Repositories;
using APIExercise.Core.Interfaces.Services;
using APIExercise.Infrastructure.Data;
using APIExercise.Infrastructure.Implementations.Repositories;
using APIExercise.Infrastructure.Implementations.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// DbContext
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
services.AddScoped<IAccountRepository, AccountRepository>();
services.AddScoped<IClientRepository, ClientRepository>();
services.AddScoped<ITransactionRepository, TransactionRepository>();

// Services
services.AddScoped<IAccountService, AccountService>();
services.AddScoped<IClientService, ClientService>();
services.AddScoped<ITransactionService, TransactionService>();

// Automapper
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
