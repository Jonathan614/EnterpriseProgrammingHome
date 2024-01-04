using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private FlightsService flightsService;

        public HomeController(FlightsService _flightsService, ILogger<HomeController> logger)
        {
            flightsService = _flightsService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var flights = flightsService.ListFlights();

            return View(flights);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ErrorMessage(string message)
        {
            TempData["Error"] = message;
            return View("Error");
        }
    }
}