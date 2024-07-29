using Microsoft.EntityFrameworkCore;
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

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.MapPost("/equipment", async (CreateCommand command, Context context) =>
{
    var handler = new CommandHandler(context);
    var id = await handler.Handle(command);
    return Results.Created($"/equipment/{id}", id);
});

app.MapGet("/equipment", async (Context context) =>
{
    var equipmentList = await context.Equipments.Include(e => e.Parameters).ToListAsync();
    return Results.Ok(equipmentList);
});

app.MapPut("/equipment/{id:int}", async (int id, UpdateCommand command, Context context) =>
{
    try
    {
        var handler = new CommandHandler(context);
        await handler.Handle(id, command);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapDelete("/equipment/{id:int}", async (int id, Context context) =>
{
    try
    {
        var handler = new CommandHandler(context);
        await handler.Handle(id);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();