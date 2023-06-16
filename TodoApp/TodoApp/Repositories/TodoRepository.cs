﻿using TodoApp.Data;
using TodoApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TodoApp.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TodoRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<Todo> GetTodos()
        {
            return _context.Todo.ToList();
        }

        public Todo GetTodoById(int id)
        {
            return _context.Todo.FirstOrDefault(t => t.ID == id);
        }

        public void Add(Todo todo)
        {
            _context.Todo.Add(todo);
            _context.SaveChanges();
        }

        public void Update(Todo todo)
        {
            var userId = GetCurrentUserId();
            var existingTodo = _context.Todo
                .FirstOrDefault(t => t.ID == todo.ID && t.TodoList.UserId == userId);

            if (existingTodo != null)
            {
                existingTodo.Title = todo.Title;
                existingTodo.Description = todo.Description;
                existingTodo.Completed = todo.Completed;
                _context.SaveChanges();
            }
        }


        public void Delete(int id)
        {
            var todo = _context.Todo.FirstOrDefault(t => t.ID == id);
            if (todo != null)
            {
                _context.Todo.Remove(todo);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Todo> GetTodosByListId(int listId)
        {
            return _context.Todo.Where(t => t.ListID == listId).ToList();
        }
        private string GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userId;
        }
    }
}
