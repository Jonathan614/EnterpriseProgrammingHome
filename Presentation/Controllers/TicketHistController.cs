using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Models;
using System.Diagnostics;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Presentation.Controllers
{
    public class TicketHistController : Controller
    {
        private ILogger<TicketController> logger;
        private TicketsService ticketsService;
        private FlightsService flightsService;
        private IWebHostEnvironment hostService;
        private readonly UserManager<CustomUser> userManager;

        public TicketHistController(UserManager<CustomUser> userManager, TicketsService _ticketsService, FlightsService _flightsService, IWebHostEnvironment _host)
        {
            ticketsService = _ticketsService;
            flightsService = _flightsService;
            hostService = _host;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPurchasedTickets()
        {
            var user = await userManager.GetUserAsync(User);

            var passport = "";

            if (user != null)
            {
                passport = user.passport;
                
            }

            var purchasedTickets = ticketsService.GetTickets(passport).ToList();

            return View("PurchaseHist", purchasedTickets);
        }
    }
}