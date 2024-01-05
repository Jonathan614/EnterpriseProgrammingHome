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
            TicketViewModel myModel = new TicketViewModel();



            return View(myModel);
        }

        /*[HttpPost] //Post method is called after the end user fills in the data and presses Submit button
        public IActionResult Create(CreateItemViewModel data, IFormFile file)
        {*/
            /*string username = User.Identity.Name; //that gives you the username (email) of the currently logged in user

            logService.LogMessage($"{username} is trying to create a new item with name {data.Name}", "info");
            if (ModelState.IsValid) //we are making sure that the validators are fired
            {
                logService.LogMessage($"Item with {data.Name} had correct inputs", "info");
                string uniqueFilename = "";
                //......
                try
                {
                    //----------------Image upload------------------------------




                    //1. to check whether image has been received successfully 
                    if (file != null)
                    {
                        logService.LogMessage($"Item with {data.Name} has a file", "info");

                        //2. generate a unique filename for the image
                        uniqueFilename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        logService.LogMessage($"Item with {data.Name} now has id: {uniqueFilename}", "info");
                        //3. identify a place (in the wwwroot > Images) where to store the actual physical file
                        //hostService enables you to get the full path where to store the image on the web server
                        string absolutePath = hostService.WebRootPath + @"\Images\" + uniqueFilename;
                        logService.LogMessage($"Item with {data.Name} and id: {uniqueFilename} will be stored at {absolutePath}", "info");
                        //4. save the actual file
                        using (var stream = System.IO.File.Create(absolutePath)) //this will create an empty stream at location : absolutePath
                        {
                            logService.LogMessage($"Item with {data.Name} and id: {uniqueFilename} is saving the file", "info");

                            file.CopyTo(stream); //this will copy the data from the uploaded file (parameter) into  empty stream at absolute Path
                        } //will close all streams
                        logService.LogMessage($"Item with {data.Name} and id: {uniqueFilename} has file saved successfully", "info");
                        //5. form the ImagePath and store assign it in the model above

                        data.ImagePath = "/Images/" + uniqueFilename; //the first / indicates asp.net to start locating the image from the root folder

                    }
                    else
                    {
                        logService.LogMessage($"Item with {data.Name} did not have a file", "info");
                    }

                    //-------------------------------------------------

                    //1. implement the create method by
                    //2. applying dependency injection and ask for ItemsService
                    //3. itemsService.AddNewItem(......)
                    itemsService.AddNewItem(data.Name, data.Price, data.CategoryId, data.Stock, data.ImagePath);
                    logService.LogMessage($"Item with {data.Name} and id: {uniqueFilename} is saved in db", "info");

                    //ViewBag doesn't survive redirections
                    //  TempData["Message"] = "Item added successfully";

                    //ViewBag vs Tempdata
                    //ViewBag doesnt survive redirections
                    //Tempdata survives one redirection

                    ViewBag.Message = "Item added successfully";
                }
                catch (Exception ex) //innerException
                {
                    if (ex.InnerException != null)
                    {
                        logService.LogMessage($"Item with {data.Name} and id: {uniqueFilename} had an inner error. Error: {ex.InnerException.Message}", "error");
                    }
                    logService.LogMessage($"Item with {data.Name} and id: {uniqueFilename} had an error. Error: {ex.GetType().ToString()} {ex.Message}", "error");
                    //ViewBag : a dynamic object, it allows you to declare properties on the fly
                    //log the exception
                    ViewBag.Error = "There was a problem adding a new item. make sure all the fields are correctly filled";
                }
            }
            else
            {
                logService.LogMessage($"Item with {data.Name} may have some incorrect inputs - Price:{data.Price} | Stock: {data.Stock} | Category: {data.CategoryId}", "warning");
            }
            // return RedirectToAction("Create");
            var categories = categoriesService.GetCategories();
            CreateItemViewModel myModel = new CreateItemViewModel();
            myModel.Categories = categories;*/

        /*    return View("Book", myModel);
        }

        public IActionResult List()
        {
            var list = flightsService.ListFlights();
            return View(list); //connection to the database is opened only here 
        }

        public IActionResult Book(int id)
        {
            var myFoundItem = flightsService.GetFlight(id);
            if (myFoundItem == null)
            {
                //redirecting the user to the List action/view
                //solution 1:
                ViewBag.Error = "Item was not found"; //ViewBag doesn't survive redirections
                var list = flightsService.ListFlights();
                return View("List", list);

                //solution 2:
                //return RedirectToAction("List");

            }
            else
                return View(myFoundItem); //this will automatically redirect the user to a View called "Details"
        }*/

        /*public IActionResult Search(string keyword)
        {
            var list = itemsService.Search(keyword);
            return View("List", list); //to locate a page/view bearing the same name of the action

        }*/


        /*//allow only deletion of items with stock == 0 and the user is ryanattard@gmail.com
        [StockValidation]
        public IActionResult Delete(int id)
        {
            itemsService.DeleteItem(id);
            return RedirectToAction("List");
        }*/


        /*[HttpGet]
        public IActionResult Edit(int id)
        {
            var item = itemsService.GetItem(id);

            CreateItemViewModel myModel = new CreateItemViewModel();
            myModel.CategoryId = item.CategoryId;
            myModel.ImagePath = item.ImagePath;
            myModel.Name = item.Name;
            myModel.Price = item.Price;
            myModel.Stock = item.Stock;


            if (item != null)
            {
                myModel.Categories = categoriesService.GetCategories();
                return View(myModel);
            }
            else
            {
                return RedirectToAction("List");
            }

        }

        [HttpPost] //this method will be triggered after a Submit button inside a form is clicked
        public IActionResult Edit(int id, CreateItemViewModel data, IFormFile file)
        {
            //to do the comments
            try
            {
                if (file != null)
                {
                    //we get the old path and delete the old image

                    string uniqueFilename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string absolutePath = hostService.WebRootPath + @"\Images\" + uniqueFilename;
                    using (var stream = System.IO.File.Create(absolutePath)) //this will create an empty stream at location : absolutePath
                    {
                        file.CopyTo(stream);
                    }
                    data.ImagePath = "/Images/" + uniqueFilename; //the first / indicates asp.net to start locating the image from the root folder
                }
                else
                {
                    //set the original image path if nothing is uploaded
                }


                itemsService.UpdateItem(id, data.Name, data.Price, data.CategoryId, data.Stock, data.ImagePath);

                ViewBag.Message = "Item updated successfully";
            }
            catch (Exception ex)
            {
                //ViewBag : a dynamic object, it allows you to declare properties on the fly
                //log the exception
                ViewBag.Error = "There was a problem updating item. make sure all the fields are correctly filled";
            }


            var categories = categoriesService.GetCategories();
            CreateItemViewModel myModel = new CreateItemViewModel();
            myModel.Categories = categories;

            return View("Edit", myModel);
        }*/
    }
}