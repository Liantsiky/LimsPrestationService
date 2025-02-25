using Microsoft.EntityFrameworkCore;
using LimsPrestationService.Data;
using LimsPrestationService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PrestationServiceContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySqlConnection"),
        new MySqlServerVersion(new Version(8, 0, 39)) // Use your MySQL version
    ));

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IEtatPrestationService, EtatPrestationService>();
builder.Services.AddScoped<IPrestationService, PrestationService>();

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // This line is crucial !

app.Run();

