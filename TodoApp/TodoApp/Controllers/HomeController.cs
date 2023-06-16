using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Security.Claims;
using TodoApp.Models;
using TodoApp.Repositories;

namespace TodoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITodoListRepository _todoListRepository;

        public HomeController(ILogger<HomeController> logger, ITodoListRepository todoListRepository)
        {
            _logger = logger;
            _todoListRepository = todoListRepository;
        }

        public IActionResult Index()
        {
            var userId = GetCurrentUserId();
            var todoLists = _todoListRepository.GetTodoListsByUserId(userId);
            return View(todoLists);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetCurrentUserId()
        {
            var userIdString = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return userIdString;
        }
    }
}
