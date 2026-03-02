using TodoApi.Models;
using TodoApi.Services;
using Xunit;

namespace TodoApi.Tests;

public class InMemoryTodoRepositoryTests
{
    [Fact]
    public void Add_Assigns_Id_And_Persists()
    {
        var repo = new InMemoryTodoRepository();
        var created = repo.Add(new TodoItem { Title = "Test" });

        Assert.True(created.Id > 0);
        Assert.Contains(repo.GetAll(), t => t.Id == created.Id);
    }

    [Fact]
    public void Delete_Removes_Item()
    {
        var repo = new InMemoryTodoRepository();
        var created = repo.Add(new TodoItem { Title = "Test" });

        var removed = repo.Delete(created.Id);

        Assert.True(removed);
        Assert.Null(repo.Get(created.Id));
    }
}
