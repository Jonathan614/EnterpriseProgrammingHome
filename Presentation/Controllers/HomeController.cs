using BusinessLogic.Services;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private FlightsService flightsService;
        private readonly UserManager<CustomUser> userManager;


        public HomeController(UserManager<CustomUser> userManager, FlightsService _flightsService, ILogger<HomeController> logger)
        {
            flightsService = _flightsService;
            _logger = logger;
            this.userManager = userManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var flights = flightsService.ListFlights();
            var user = await userManager.GetUserAsync(User);
            var admin = false;

            if ((user != null)&&(user.isAdmin))
            {
                admin = true;
            }
            ViewBag.Admin = admin;

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