using Microsoft.EntityFrameworkCore;
using MinimalAPI;
using MinimalAPI.Contexts;
using MinimalAPI.Handlers;
using MinimalAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("EquipmentDb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.SwEndpoints();

app.Run();