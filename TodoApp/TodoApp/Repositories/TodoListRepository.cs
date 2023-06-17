using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Repositories;

public class TodoListRepository : ITodoListRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserHelper _userHelper;

    public TodoListRepository(ApplicationDbContext context, UserHelper userHelper)
    {
        _context = context;
        _userHelper = userHelper;
    }

    public IEnumerable<TodoList> GetTodoLists()
    {
        var userId = _userHelper.GetCurrentUserId();
        return _context.TodoList
            .Where(tl => tl.UserId == userId)
            .ToList();
    }

    public TodoList GetTodoListById(int id)
    {
        var userId = _userHelper.GetCurrentUserId();
        return _context.TodoList
            .Include(tl => tl.Todos)
            .FirstOrDefault(tl => tl.ID == id && tl.UserId == userId);
    }

    public void AddTodoList(TodoList todoList)
    {
        var userId = _userHelper.GetCurrentUserId();
        todoList.UserId = userId;
        _context.TodoList.Add(todoList);
        _context.SaveChanges();
    }

    public void UpdateTodoList(TodoList todoList)
    {
        var userId = _userHelper.GetCurrentUserId();
        var existingTodoList = GetTodoListById(todoList.ID);

        if (existingTodoList != null && existingTodoList.UserId == userId)
        {
            existingTodoList.Title = todoList.Title;
            _context.SaveChanges();
        }
    }

    public void DeleteTodoList(int id)
    {
        var userId = _userHelper.GetCurrentUserId();
        var todoList = _context.TodoList.FirstOrDefault(tl => tl.ID == id && tl.UserId == userId);
        if (todoList != null)
        {
            _context.TodoList.Remove(todoList);
            _context.SaveChanges();
        }
    }

    public IEnumerable<TodoList> GetTodoListsByUserId(string userId)
    {
        return _context.TodoList
            .Where(t => t.UserId == userId)
            .ToList();
    }

    public IEnumerable<Todo> GetTodosForList(int todoListId)
    {
        return _context.Todo
            .Where(t => t.ListID == todoListId)
            .ToList();
    }
}
