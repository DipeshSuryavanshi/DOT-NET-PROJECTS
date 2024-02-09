using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WebApplication1.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
//using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace WebApplication1.Controllers
{
    public class Home1Controller : Controller
    {
        private readonly RestClient _client;
        private readonly string _baseUrl = "http://localhost:7/api/";

        public Home1Controller()
        {
            _client = new RestClient(_baseUrl);
        }
       
        [HttpGet]
        public IActionResult Index()
        {
            string baseUrl = null;

            // Your desired API endpoint path
            string apiEndpointPath = "https://localhost:7122/api/Registration";

            // Create a RestClient with the base URL
            var client = new RestClient(baseUrl + apiEndpointPath);

            // Create a RestRequest and set the resource (API endpoint path)
            var request = new RestRequest(apiEndpointPath, Method.Get);

            // Execute the request
            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                Console.WriteLine($"Error: {response.ErrorMessage}, Content: {response.Content}");

                // Handle the error, log, and possibly return an error view
                return View("ErrorView");
            }
           
            var registrationsJson = response.Content;
            //var registration = JsonContent.
            var registrations = JsonConvert.DeserializeObject<IEnumerable<RegistrationMVC>>(registrationsJson);

            return View(registrations);
        }
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(RegistrationMVC registration)
        {
            // Full URL of the API endpoint
            string apiUrl = "https://localhost:7122/api/Registration";

            // Create a RestClient with the API endpoint URL
            var client = new RestClient(apiUrl);

            // Create a RestRequest and set the HTTP method to POST
            var request = new RestRequest(apiUrl,Method.Post);

            // Set the request content type (assuming it's JSON)
            request.AddHeader("Content-Type", "application/json");

            // Convert the Registration object to JSON and set it as the request body
            request.AddJsonBody(registration);

            // Execute the request
            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                Console.WriteLine($"Error: {response.ErrorMessage}, Content: {response.Content}");

                // Handle the error, log, and possibly return an error view
                return View("ErrorView");
            }

            // Assuming the API returns a single Registration object for a POST request
            var registrationJson = response.Content;
            var registeredUser = JsonConvert.DeserializeObject<RegistrationMVC>(registrationJson);

            return View("Index", registeredUser); // Assuming you have an Index2.cshtml view
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            string baseUrl = null;

            // Your desired API endpoint path
            string apiEndpointPath = $"https://localhost:7122/api/Registration/{id}";

            // Create a RestClient with the base URL
            var client = new RestClient(baseUrl + apiEndpointPath);

            // Create a RestRequest and set the resource (API endpoint path)
            var request = new RestRequest(apiEndpointPath, Method.Get);

            // Execute the request
            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                Console.WriteLine($"Error: {response.ErrorMessage}, Content: {response.Content}");
                return View("ErrorView");
            }

            var registrationJson = response.Content;
            var registration = JsonConvert.DeserializeObject<RegistrationMVC>(registrationJson);

            return View(registration);
        }
        [HttpPost]
        public IActionResult Edit(int id, RegistrationMVC registration)
        {
            string apiUrl = $"https://localhost:7122/api/Registration/{id}";
            var client = new RestClient(apiUrl);
            var request = new RestRequest(apiUrl, Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(registration);
            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                Console.WriteLine($"Error: {response.ErrorMessage}, Content: {response.Content}");
                return View("ErrorView");
            }

            return RedirectToAction("Index"); // Redirect to Index action
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            string apiUrl = $"https://localhost:7122/api/Registration/{id}";
            var client = new RestClient(apiUrl);
            var request = new RestRequest(apiUrl, Method.Delete);
            var response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                Console.WriteLine($"Error: {response.ErrorMessage}, Content: {response.Content}");
                // Return a JSON response indicating failure
                return Json(new { success = false, message = "Failed to delete item." });
            }

            // Return a JSON response indicating success and the ID of the deleted item
            return Json(new { success = true, message = "Item deleted successfully.", deletedId = id });
        }



    }
}

