using TodoApi.Models;
using System.Collections.Generic;

namespace TodoApi.Services;

public interface ITodoRepository
{
    IEnumerable<TodoItem> GetAll();
    TodoItem? Get(int id);
    TodoItem Add(TodoItem item);
    bool Delete(int id);
}
