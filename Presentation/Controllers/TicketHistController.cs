using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Models;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class TicketHistController : Controller
    {
        private ILogger<TicketController> logger;
        private TicketsService ticketsService;
        private FlightsService flightsService;
        private IWebHostEnvironment hostService;
        public TicketHistController(TicketsService _ticketsService, FlightsService _flightsService, IWebHostEnvironment _host)
        {
            ticketsService = _ticketsService;
            flightsService = _flightsService;
            hostService = _host;

        }

        [HttpGet]
        [Authorize]
        public IActionResult GetPurchasedTickets()
        {
            var passport = "67845";//to get from login deets

            var purchasedTickets = ticketsService.GetTickets(passport).ToList();

            return View("PurchaseHist", purchasedTickets);
        }
    }
}