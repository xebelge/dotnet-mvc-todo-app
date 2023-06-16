using System.Collections.Generic;
using TodoApp.Models;

namespace TodoApp.Repositories
{
    public interface ITodoListRepository
    {
        IEnumerable<TodoList> GetTodoLists();
        TodoList GetTodoListById(int id);
        void AddTodoList(TodoList todoList);
        void UpdateTodoList(TodoList todoList);
        void DeleteTodoList(int id);
        IEnumerable<TodoList> GetTodoListsByUserId(string userId);
        IEnumerable<Todo> GetTodosForList(int todoListId);
    }
}

