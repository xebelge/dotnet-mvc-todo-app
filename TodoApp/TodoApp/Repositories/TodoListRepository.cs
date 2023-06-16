using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoApp.Data;
using TodoApp.Models;
using TodoApp.Repositories;

public class TodoListRepository : ITodoListRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TodoListRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public IEnumerable<TodoList> GetTodoLists()
    {
        var userId = GetCurrentUserId();
        return _context.TodoList.Where(tl => tl.UserId == userId).ToList();
    }

    public TodoList GetTodoListById(int id)
    {
        var userId = GetCurrentUserId();
        return _context.TodoList
            .Include(tl => tl.Todos)
            .FirstOrDefault(tl => tl.ID == id && tl.UserId == userId);
    }

    public void AddTodoList(TodoList todoList)
    {
        var userId = GetCurrentUserId();
        todoList.UserId = userId;
        _context.TodoList.Add(todoList);
        _context.SaveChanges();
    }

    public void UpdateTodoList(TodoList todoList)
    {
        var userId = GetCurrentUserId();
        var existingTodoList = _context.TodoList
            .Include(tl => tl.Todos) 
            .FirstOrDefault(tl => tl.ID == todoList.ID && tl.UserId == userId);

        if (existingTodoList != null)
        {
            existingTodoList.Title = todoList.Title;
            _context.SaveChanges();
        }
    }


    public void DeleteTodoList(int id)
    {
        var userId = GetCurrentUserId();
        var todoList = _context.TodoList.FirstOrDefault(tl => tl.ID == id && tl.UserId == userId);
        if (todoList != null)
        {
            _context.TodoList.Remove(todoList);
            _context.SaveChanges();
        }
    }

    public IEnumerable<TodoList> GetTodoListsByUserId(string userId)
    {
        return _context.TodoList.Where(t => t.UserId == userId).ToList();
    }

    public IEnumerable<Todo> GetTodosForList(int todoListId)
    {
        return _context.Todo
            .Where(t => t.ListID == todoListId)
            .ToList();
    }
    private string GetCurrentUserId()
    {
        var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId;
    }
}
