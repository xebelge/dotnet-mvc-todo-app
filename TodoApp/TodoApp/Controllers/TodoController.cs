using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoApp.Models;
using TodoApp.Repositories;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITodoListRepository _todoListRepository;

        public TodoController(ITodoRepository todoRepository, ITodoListRepository todoListRepository)
        {
            _todoRepository = todoRepository;
            _todoListRepository = todoListRepository;
        }

        [HttpGet]
        public IActionResult Create(int listId)
        {
            var todoList = _todoListRepository.GetTodoListById(listId);
            if (todoList == null)
            {
                return NotFound();
            }

            var todo = new Todo
            {
                ListID = listId
            };

            return View(todo);
        }

        [HttpPost]
        public IActionResult Create(Todo todo)
        {
                _todoRepository.Add(todo);
                return RedirectToAction("ShowTodos", "TodoList", new { id = todo.ListID });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var todo = _todoRepository.GetTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        [HttpPost]
        public IActionResult Edit(Todo todo)
        {
                _todoRepository.Update(todo);
            return RedirectToAction("ShowTodos", "TodoList", new { id = todo.ListID });
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var todo = _todoRepository.GetTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }

            return View(todo);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var todo = _todoRepository.GetTodoById(id);
            if (todo != null)
            {
                _todoRepository.Delete(id);
            }

            return RedirectToAction("ShowTodos", "TodoList", new { id = todo.ListID });
        }

        [HttpPost]
        public IActionResult Complete(int id)
        {
            var todo = _todoRepository.GetTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Completed = true;
            _todoRepository.Update(todo);

            return RedirectToAction("ShowTodos", "TodoList", new { id = todo.ListID });
        }

        [HttpPost]
        public IActionResult UndoComplete(int id)
        {
            var todo = _todoRepository.GetTodoById(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Completed = false;
            _todoRepository.Update(todo);

            return RedirectToAction("ShowTodos", "TodoList", new { id = todo.ListID });
        }

        [HttpGet]
        public IActionResult ShowTodos(int id)
        {
            var todoList = _todoListRepository.GetTodoListById(id);
            if (todoList == null)
            {
                return NotFound();
            }

            var todos = _todoRepository.GetTodosByListId(id);
            ViewBag.TodoListTitle = todoList.Title;
            ViewBag.ListID = id;

            var completedTodos = todos.Where(t => t.Completed);
            ViewBag.CompletedTodos = completedTodos;

            return View(todos);
        }
    }
}
