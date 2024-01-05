using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class FlightController : Controller
    {
        private TicketsService ticketsService;
        private FlightsService flightsService;
        private IWebHostEnvironment hostService;
        public FlightController(TicketsService _ticketsService, FlightsService _flightsService, IWebHostEnvironment _host)
        {
            ticketsService = _ticketsService;
            flightsService = _flightsService;
            hostService = _host;

        }

        [HttpGet] 
        public IActionResult Details(int id)
        {
            var flight = flightsService.GetFlight(id);

            if (flight == null)
            {
                return NotFound();
            }

            return View("Details", flight);
        }
    }
}