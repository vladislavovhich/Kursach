using Lab_5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab_5.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/Home/ErrorPage/{code:int}")]
        public IActionResult ErrorPage(int code)
        {

            switch (code)
            {
                case 404:
                    ViewBag.Message = "Страница не найдена";
                    break;
                case 403:
                    ViewBag.Message = "Доступ запрещен";
                    break;
                default:
                    ViewBag.Message = $"Произошла ошибка в выполнении запроса. Код ошибки: {code}";
                    break;
            }

            return View();
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