using Microsoft.EntityFrameworkCore;
using MinimalAPI.Contexts;
using MinimalAPI.Handlers;
using MinimalAPI.Models;

namespace MinimalAPI
{
    public static class Endpoints
    {
        public static void SwEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/", context =>
            {
                context.Response.Redirect("/swagger");
                return Task.CompletedTask;
            });

            app.MapPost("/equipment", async (UpdateCreateCommand command, Context context) =>
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

            app.MapGet("/equipment/{id}", async (int id, Context context) =>
            {
                try
                {
                    var equipment = await context.Equipments.Include(e => e.Parameters).FirstOrDefaultAsync(e => e.Id == id);
                    return Results.Ok(equipment);
                }
                catch (KeyNotFoundException ex)
                {
                    return Results.NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPut("/equipment/{id:int}", async (int id, UpdateCreateCommand command, Context context) =>
            {
                try
                {
                    var handler = new CommandHandler(context);
                    await handler.Handle(id, command);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException ex)
                {
                    return Results.NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapDelete("/equipment/{id}", async (int id, Context context) =>
            {
                try
                {
                    var handler = new CommandHandler(context);
                    await handler.Handle(id);
                    return Results.NoContent();
                }
                catch (KeyNotFoundException ex)
                {
                    return Results.NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });
        }
    }
}
