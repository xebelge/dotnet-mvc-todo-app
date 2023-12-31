﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TodoApp.Models;
using TodoApp.Repositories;
using System.Threading.Tasks;
using System.Security.Claims;

namespace TodoApp.Controllers
{
    public class TodoListController : Controller
    {
        private readonly ITodoListRepository _todoListRepository;
        private readonly ITodoRepository _todoRepository;
        private readonly UserHelper _userHelper;

        public TodoListController(ITodoListRepository todoListRepository, ITodoRepository todoRepository, UserHelper userHelper)
        {
            _todoListRepository = todoListRepository;
            _todoRepository = todoRepository;
            _userHelper = userHelper;
        }

        public IActionResult Index()
        {
            var todoLists = _todoListRepository.GetTodoLists();
            return View(todoLists);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TodoList todoList)
        {
            todoList.UserId = _userHelper.GetCurrentUserId();
            _todoListRepository.AddTodoList(todoList);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var todoList = _todoListRepository.GetTodoListById(id);
            if (todoList == null)
            {
                return NotFound();
            }
            return View(todoList);
        }

        [HttpPost]
        public IActionResult Edit(TodoList todoList)
        {
            _todoListRepository.UpdateTodoList(todoList);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var todoList = _todoListRepository.GetTodoListById(id);
            if (todoList == null)
            {
                return NotFound();
            }
            return View(todoList);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _todoListRepository.DeleteTodoList(id);
            return RedirectToAction("Index");
        }

        public IActionResult ShowTodos(int id)
        {
            var todoList = _todoListRepository.GetTodoListById(id);
            if (todoList == null)
            {
                return NotFound();
            }

            var todos = _todoRepository.GetTodosByListId(id);
            return View(todos);
        }
    }
}
