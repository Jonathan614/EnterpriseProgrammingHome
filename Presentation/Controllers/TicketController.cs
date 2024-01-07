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
        public IActionResult Book(BookTicketViewModel data, [FromServices] IWebHostEnvironment host, IFormFile file)
        {

            string uniqueFilename = "";

            try
            {

                if (file != null)
                {
                    logger.LogInformation($"Ticket ID {data.Id} has a file", "info");
                    uniqueFilename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    logger.LogInformation($"Ticket ID {data.Id} now has id: {uniqueFilename}", "info");
                    string absolutePath = hostService.WebRootPath + @"\Images\" + uniqueFilename;
                    logger.LogInformation($"Ticket ID {data.Id} and id: {uniqueFilename} will be stored at {absolutePath}", "info");
                    using (var stream = System.IO.File.Create(absolutePath)) 
                    {
                        logger.LogInformation($"Ticket ID {data.Id} and id: {uniqueFilename} is saving the file", "info");

                        file.CopyTo(stream); 
                    } 
                    logger.LogInformation($"Ticket ID {data.Id} and id: {uniqueFilename} has file saved successfully", "info");

                    data.passportPhotoPath = "/Images/" + uniqueFilename; 

                    ticketsService.AddTicket(data.row, data.column, data.flightId, data.passport, data.pricePaid, data.cancelled, data.passportPhotoPath);

                }
                else
                {
                    ticketsService.AddTicket(data.row, data.column, data.flightId, data.passport, data.pricePaid, data.cancelled, uniqueFilename);
                }
                

                ViewBag.Message = "Ticket booked successfully";
            }
            catch (Exception ex) //innerException
            {
                ViewBag.Error = "There was a problem adding a new Ticket. make sure all the fields are correctly filled";
            }
            return RedirectToAction("GetPurchasedTickets", "TicketHist");

        }
    }      
}
