using TodoApi.Models;
using TodoApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/todos", (ITodoRepository repo) =>
{
    return Results.Ok(repo.GetAll());
});

app.MapPost("/api/todos", (TodoItemCreateDto dto, ITodoRepository repo) =>
{
    if (string.IsNullOrWhiteSpace(dto.Title))
        return Results.BadRequest("Title is required.");

    var item = new TodoItem { Title = dto.Title, IsDone = false };
    var created = repo.Add(item);
    return Results.Created($"/api/todos/{created.Id}", created);
});

app.MapDelete("/api/todos/{id:int}", (int id, ITodoRepository repo) =>
{
    return repo.Delete(id) ? Results.NoContent() : Results.NotFound();
});

app.Run();

public record TodoItemCreateDto(string Title);
