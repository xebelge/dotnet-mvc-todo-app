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
        private readonly UserHelper _userHelper;

        public HomeController(ILogger<HomeController> logger, ITodoListRepository todoListRepository, UserHelper userHelper)
        {
            _logger = logger;
            _todoListRepository = todoListRepository;
            _userHelper = userHelper;
        }

        public IActionResult Index()
        {
            var userId = _userHelper.GetCurrentUserId();
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
    }
}
