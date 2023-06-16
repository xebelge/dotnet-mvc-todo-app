using TodoApp.Models;
using System.Collections.Generic;

namespace TodoApp.Repositories
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetTodos();
        Todo GetTodoById(int id);
        void Add(Todo todo);
        void Update(Todo todo);
        void Delete(int id);
        IEnumerable<Todo> GetTodosByListId(int listId);
    }
}
