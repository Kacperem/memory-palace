using MemoryPalaceAPI.Entities;
using MemoryPalaceAPI.Seeders;
using MemoryPalaceAPI.Services;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MemoryPalaceDbContext>();
builder.Services.AddScoped<MemoryPalaceSeeder>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<ITwoDigitSystemService, TwoDigitSystemService>();

var app = builder.Build();

var scope = app.Services.CreateScope();

var seeder = scope.ServiceProvider.GetRequiredService<MemoryPalaceSeeder>();

seeder.Seed();

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
