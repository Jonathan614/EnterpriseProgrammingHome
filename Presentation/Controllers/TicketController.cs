using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Models;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class TicketController : Controller
    {
        private ILogger<TicketController> logger;
        private TicketsService ticketsService;
        private FlightsService flightsService;
        private IWebHostEnvironment hostService;
        public TicketController(ILogger<TicketController> _logger, TicketsService _ticketsService, FlightsService _flightsService, IWebHostEnvironment _host)
        {
            logger = _logger;
            ticketsService = _ticketsService;
            flightsService = _flightsService;
            hostService = _host;

        }

        [HttpGet] //Get method is called to load the page with blank controls
        public IActionResult Book(int Id)
        {
            var flight = flightsService.GetFlight(Id);

            BookTicketViewModel myModel = new BookTicketViewModel();
            myModel.FlightVM = flight;


            myModel.flightId = flight.Id;

            return View(myModel);
        }

        [HttpPost]
        public IActionResult Book(BookTicketViewModel data, [FromServices] IWebHostEnvironment host)
        {

            logger.LogInformation("data.Flight:  " + data.FlightVM);
                
                ticketsService.AddTicket(data.row, data.column, data.flightId, data.passport, data.pricePaid, data.cancelled);

                return RedirectToAction("GetPurchasedTickets", "TicketHist");
      
        }



    }
}