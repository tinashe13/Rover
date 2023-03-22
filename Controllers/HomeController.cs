using MarsRover.Models;
using Microsoft.AspNetCore.Mvc;
using Rover.Models;
using System.Diagnostics;
using System.Linq.Expressions;

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
            string[] message;
            if (!string.IsNullOrEmpty(instruction))
            {
                message = instruction.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                // Handle the NullReferenceException
                ViewBag.ErrorMessage = "Please enter Rover instruction to explore";
                return View("index");
            }


            //split the input to gee the upper right co-ordinates of the plateau using a character array
            int[] PlateauArr;

            if (message[0].Trim().Split(' ').All(x => int.TryParse(x, out int _)))
            {
                PlateauArr = message[0].Trim().Split(' ').Select(int.Parse).ToArray();
            }
            else
            {
                // Handle the FormatException
                ViewBag.ErrorMessage = "The first line of your input must be 2 whole numbers with a single space in between";
                return View("index");
            }

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

                //initiate objects
                var plateau = new Plateau(PlateauArr[0], PlateauArr[1]);
                var roverPosition = new Position(xCoodinate, yCoodinate, RoverOrientation);
                var roverInstructions = RoverInstructions[i];

                // Explore rover
                var rover = new Mars_Rover(roverPosition, plateau);

                try
                {
                    rover.ExecuteInstructions(roverInstructions.ToUpper());
                }
                catch (Exception ex)
                {
                    if (ex is InvalidOperationException || ex is ArgumentException)
                    {
                        // Handle the exception here
                        ViewBag.ErrorMessage = ex.Message;
                        return View("index");
                    }
                    else
                    {
                        // Handle other types of exceptions here
                        ViewBag.ErrorMessage = "Please check your input for correct format";
                    }
                    throw;
                }

                roverResponses.Add(rover.CurrentPosition.ToString());
                //Console.WriteLine(roverResponses[0]);      
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