using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Models;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class AdminController : Controller
    {
        private ILogger<TicketController> logger;
        private TicketsService ticketsService;
        private FlightsService flightsService;
        private IWebHostEnvironment hostService;
        public AdminController(ILogger<TicketController> _logger, TicketsService _ticketsService, FlightsService _flightsService, IWebHostEnvironment _host)
        {
            logger = _logger;
            ticketsService = _ticketsService;
            flightsService = _flightsService;
            hostService = _host;

        }

        [HttpGet]
        [Authorize]
        public IActionResult Tickets(int flightId)
        {
            logger.LogInformation("flightId: " + flightId);
            ViewBag.flightId = flightId;
            string nullProp = null;//because GetTickets expects one parameter
            var tickets = ticketsService.GetTickets(nullProp)
            .Where(ticket => ticket.flightId == flightId )
            .Select(ticket => new TicketViewModel
            {
                Id = ticket.Id,
                row = ticket.row,
                column = ticket.column,
                pricePaid = ticket.pricePaid,
                passport = ticket.passport,
            });


            return View("Bookings",tickets);
        }

        [HttpGet]
        [Authorize]
        public IActionResult TicketDetails(int ticketId)
        {

            var ticket = ticketsService.GetTicket(ticketId);

            if (ticket == null)
            {
                return NotFound();
            }

            return View("TicketDetails", ticket);
        }
    }
}