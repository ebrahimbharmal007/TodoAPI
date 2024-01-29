using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI
{
    public static class TodoEndpoints
    {
        public static RouteGroupBuilder MapTodosApi(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAllTodos).WithName("GetAllToDos").WithOpenApi();
            group.MapGet("/complete", GetCompleteTodos).WithName("GetOnlyCompletedToDos").WithOpenApi();
            group.MapGet("/pending", GetPendingTodos).WithName("GetOnlyPendingToDos").WithOpenApi();
            group.MapGet("/{id}", GetTodo).WithName("GetAToDoItem").WithOpenApi();

            group.MapPost("/", CreateTodo).AddEndpointFilter(async (efiContext, next) =>
            {
                var addNewToDo = efiContext.GetArgument<ToDoItemDTO>(0);
                if (String.IsNullOrWhiteSpace(addNewToDo.title)) { return Results.BadRequest("Title value is null, empty or whitespace"); }
                else if (String.IsNullOrWhiteSpace(addNewToDo.description)) { return Results.BadRequest("Title value is null, empty or whitespace"); }
                else if (addNewToDo.priority < 0) { return Results.BadRequest("Priority cannot be a negative value"); }
                else if (addNewToDo.description.Length > 255) { return Results.BadRequest("Description cannot be greater than 255 characters"); }
                else if (addNewToDo.title.Length > 50) { return Results.BadRequest("Title cannot be greater than 50 characters"); }
                return await next(efiContext);
            }).WithOpenApi();


            group.MapPatch("/{id}/complete", CompleteToDo).WithOpenApi();

            group.MapDelete("/{id}", DeleteTodo).RequireAuthorization().RequireRateLimiting("fixed").WithOpenApi();

            return group;
        }

        public static async Task<IResult> GetAllTodos(TodoDb db)
        {
            return TypedResults.Ok(await db.Todos.ToArrayAsync());
        }

        public static async Task<IResult> GetCompleteTodos(TodoDb db)
        {
            return TypedResults.Ok(await db.Todos.Where(t => t.IsCompleted).ToListAsync());
        }

        public static async Task<IResult> GetPendingTodos(TodoDb db)
        {
            return TypedResults.Ok(await db.Todos.Where(t => !t.IsCompleted).ToListAsync());
        }

        public static async Task<IResult> GetTodo(Guid id, TodoDb db)
        {
            return await db.Todos.FindAsync(id)
                is ToDoItem todo
                    ? TypedResults.Ok(todo)
                    : TypedResults.NotFound();
        }

        public static async Task<IResult> CreateTodo(ToDoItemDTO addNewToDo, TodoDb db)
        {
            var todo = ToDoItem.Create(addNewToDo.title, addNewToDo.description, addNewToDo.priority);
            db.Todos.Add(todo);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/{todo.Id}", todo);
        }

        public static async Task<IResult> DeleteTodo(Guid id, TodoDb db)
        {
            if (await db.Todos.FindAsync(id) is ToDoItem todo)
            {
                db.Todos.Remove(todo);
                await db.SaveChangesAsync();
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }

        public static async Task<IResult> CompleteToDo(Guid id, TodoDb db)
        {
            var todo = await db.Todos.FindAsync(id);

            if (todo is null) return Results.NotFound();
            if (todo.IsCompleted) return Results.BadRequest("ToDo Item already marked as completed");

            todo.IsCompleted = true;

            await db.SaveChangesAsync();

            return TypedResults.Ok();
        }
    }

    public record ToDoItemDTO(string title, string description, int priority)
    {

    }

    public record UpdateToDoItemDTO(string title, string description, int priority)
    {

    }
}
