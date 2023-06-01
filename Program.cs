using Microsoft.EntityFrameworkCore;
using Tasks.Data;
using Tasks.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

app.MapGet("/v1/todos", (AppDbContext context) => {
    var todos = context.Todos.ToList();
    return Results.Ok(todos);
});

app.MapGet("/v1/todos/{id}/done", async (AppDbContext context, Guid id) => {
    
    var todo = await context.Todos.FirstOrDefaultAsync(item => item.Id == id);

    if (todo is null) {
        return Results.NotFound();
    }

    try {
        todo.Done = !todo.Done;
        
        context.Todos.Update(todo);
        context.SaveChanges();

        return Results.Ok(todo);
    }
    catch (Exception) {
        return Results.StatusCode(500);
    }
});

app.MapGet("/v1/todos/{id}", async (AppDbContext context, Guid id) => {
    var todo = await context.Todos.FirstOrDefaultAsync(item => item.Id == id);
    
    return todo is null ? Results.NotFound() : Results.Ok(todo);
});

app.MapPost("v1/todos", (AppDbContext context, CreateTodoViewModel model) => {
    var todo = model.MapTo();
    
    if (!model.IsValid) {
        return Results.BadRequest(model.Notifications);
    }

    try {
        context.Todos.Add(todo);
        context.SaveChanges();

        return Results.Created($"/v1/todos/{todo.Id}", todo);
    }
    catch (Exception) {
        return Results.StatusCode(500);
    }
});

app.MapPut("v1/todos/{id}", async (AppDbContext context, UpdateTodoViewModel model, Guid id) => {
    
    if (!model.IsValid) {
        return Results.BadRequest(model.Notifications);
    }

    var todo = await context.Todos.FirstOrDefaultAsync(item => item.Id == id);

    if (todo is null) {
        return Results.NotFound();
    }

    try {
        todo.Title = model.Title;

        context.Todos.Update(todo);
        context.SaveChanges();

        return Results.Ok(todo);
    }
    catch (Exception) {
        return Results.StatusCode(500);
    }
});


app.MapDelete("v1/todos/{id}", async (AppDbContext context, Guid id) => {

    var todo = await context.Todos.FirstOrDefaultAsync(item => item.Id == id);
    
    if (todo is null) {
        return Results.NotFound();
    }
    
    try {
        context.Todos.Remove(todo);
        context.SaveChanges();

        return Results.Ok();
    }
    catch (Exception) {
        return Results.StatusCode(500);
    }
});

app.Run("http://localhost:3000");
