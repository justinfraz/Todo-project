using TodoApi.Models;
using System.Collections.Concurrent;

namespace TodoApi.Services;

public class InMemoryTodoRepository : ITodoRepository
{
    private readonly ConcurrentDictionary<int, TodoItem> _items = new();
    private int _nextId = 1;

    public IEnumerable<TodoItem> GetAll() => _items.Values.OrderBy(t => t.Id);

    public TodoItem? Get(int id) =>
        _items.TryGetValue(id, out var item) ? item : null;

    public TodoItem Add(TodoItem item)
    {
        var id = Interlocked.Increment(ref _nextId);
        item.Id = id;
        _items[id] = item;
        return item;
    }

    public bool Delete(int id) =>
        _items.TryRemove(id, out _);
}
