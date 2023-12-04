using Microsoft.AspNetCore.Mvc;
using MvcCongresoTIC.Models;
using System.Diagnostics;

namespace MvcCongresoTIC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }        
        public IActionResult Inicio()
        {
            return View();
        }
        public IActionResult Participantes()
        {
            return RedirectToAction("Index", "Participantes");
        }
        public IActionResult Registro()
        {
            return RedirectToAction("Create", "Participantes");
        }
        public IActionResult Conferencias()
        {
            return RedirectToAction("Index", "Conferencias");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}