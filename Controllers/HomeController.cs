using MarsRover.Models;
using Microsoft.AspNetCore.Mvc;
using Rover.Models;
using System.Diagnostics;

namespace MarsRover.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
 
            return View();
        }

        [HttpPost]
        public IActionResult Index(String instruction)
        {
            //split the input by each new line
            string[] message = instruction.Split(Environment.NewLine,StringSplitOptions.RemoveEmptyEntries);
           
            //split the input to gee the upper right co-ordinates of the plateau using a character array
            int[] PlateauArr = message[0].Trim().Split(' ').Select(int.Parse).ToArray();

            //create new array to strore the values of the rover starting position and instruction seperate
            string[] RoverDetails = new string[message.Length - 1];
            for (int i = 1; i < message.Length; i++)
            {
                RoverDetails[i - 1] = message[i];
            }
            

            //create an array containing only the starting positions of the Rovers
            string[] StartingPosition = new string[RoverDetails.Length / 2];
            for (int i = 0, j = 0; i < RoverDetails.Length; i += 2, j++)
            {
                StartingPosition[j] = RoverDetails[i];
            }

            //create an array containing only the Movement commands for the Rovers
            string[] RoverInstructions = new string[RoverDetails.Length / 2];
            for (int i = 1, j = 0; i < RoverDetails.Length; i += 2, j++)
            {
                RoverInstructions[j] = RoverDetails[i];
            }

            var roverResponses = new List<string>();
            for (int i = 0; i < RoverInstructions.Length; i++)
            {

                string[] PositionString = StartingPosition[i].Trim().Split(' ');

                int xCoodinate = int.Parse(PositionString[0]);
                int yCoodinate = int.Parse(PositionString[1]);
                string RoverOrientation = PositionString[2].Trim().ToUpper();

                
                //initiate components
                var plateau = new Plateau(PlateauArr[0], PlateauArr[1]);
                var roverPosition = new Position(xCoodinate, yCoodinate, RoverOrientation);
                var roverInstructions = RoverInstructions[i];


                // Explore rover
                var rover = new Mars_Rover(roverPosition, plateau);
                rover.ExecuteInstructions(roverInstructions.ToUpper());
                roverResponses.Add(rover.CurrentPosition.ToString());
                Console.WriteLine(roverResponses[0]);
                
            }



            // Display responses
            ViewBag.RoverResponses = roverResponses;
            return View("index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}