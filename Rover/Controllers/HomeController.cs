using Microsoft.AspNetCore.Mvc;
using Rover.Models;
using System.Diagnostics;

namespace Rover.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string[] instruction)
        {
            // Parse plateau coordinates
            //var plateauCoordinatesArray = instruction.Split(" ");


            // Explore each rover
            var roverResponses = new List<string>();
            for (int i = 0; i < instruction.Length; i += 2)
            {
                // Parse rover details

                //char[] delims = new[] { '\r', '\n' };

                var roverDetailsArray = instruction[i].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

                var plateauSize = instruction[0].Split(" ");
                var plateau = new Plateau(int.Parse(roverDetailsArray[0]), int.Parse(roverDetailsArray[1]));


                var roverPosition = new Position(int.Parse(roverDetailsArray[0]), int.Parse(roverDetailsArray[1]), roverDetailsArray[2]);
                var roverInstructions = instruction[i + 1];

                // Explore rover
                var rover = new Mars_Rover(roverPosition, plateau);
                rover.ExecuteInstructions(roverInstructions);
                roverResponses.Add(rover.CurrentPosition.ToString());
            }

            // Display responses
            ViewBag.RoverResponses = roverResponses;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}