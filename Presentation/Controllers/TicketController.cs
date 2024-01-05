using BusinessLogic.Services;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System.Diagnostics;

namespace Presentation.Controllers
{
    public class TicketController : Controller
    {
        private TicketsService ticketsService;
        private FlightsService flightsService;
        private IWebHostEnvironment hostService;
        public TicketController(TicketsService _ticketsService, FlightsService _flightsService, IWebHostEnvironment _host)
        {
            ticketsService = _ticketsService;
            flightsService = _flightsService;
            hostService = _host;

        }

        [HttpGet] //Get method is called to load the page with blank controls
        public IActionResult Book()
        {
            BookTicketViewModel myModel = new BookTicketViewModel();

            return View(myModel);
        }

        [HttpPost]
        public IActionResult Book(BookTicketViewModel myModel, int fId, [FromServices] IWebHostEnvironment host)
        {
            var flightDetails = flightsService.GetFlight(fId);

/*            try
            {*/
                myModel.flightId = fId;
                // Add the ticket
                ticketsService.AddTicket(myModel.row, myModel.column, myModel.flightId, myModel.passport, myModel.pricePaid, myModel.cancelled);

                ViewBag.Message = "Ticket booked successfully";
                
           /* }
            catch (Exception ex)
            {*/
                //ViewBag.Error = "There was a problem booking this flight. Make sure all the fields are correctly filled";
            //}

            return View("Book", new BookTicketViewModel());
        }

    }
}