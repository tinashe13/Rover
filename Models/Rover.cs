using MarsRover.Models;

namespace Rover.Models
{
    public class Mars_Rover
    {
        //initiate the Rover class
        public Position CurrentPosition { get; private set; }
        public Plateau Plateau { get; }

        public Mars_Rover(Position startingPosition, Plateau plateau)
        {
            CurrentPosition = startingPosition;
            Plateau = plateau;
        }


        //Execute instructions method
        public void ExecuteInstructions(string instructions)
        {
            foreach (var instruction in instructions)
            {
                switch (instruction)
                {
                    case 'L':
                        TurnLeft();
                        break;
                    case 'R':
                        TurnRight();
                        break;
                    case 'M':
                        MoveForward();
                        break;
                    default:
                        throw new ArgumentException($"Invalid instruction: {instruction}");
                }
            }
        }

        //Turn Left method to impliment to command 'L'
        public void TurnLeft()
        {
            switch (CurrentPosition.Orientation)
            {
                case "N":
                    CurrentPosition.Orientation = "W";
                    break;
                case "E":
                    CurrentPosition.Orientation = "N";
                    break;
                case "S":
                    CurrentPosition.Orientation = "E";
                    break;
                case "W":
                    CurrentPosition.Orientation = "S";
                    break;
                default: 
                    throw new ArgumentException($"Invalid Rover orientation: {CurrentPosition.Orientation}");
            }
        }

        //Turn Right method to impliment to command 'R'
        public void TurnRight()
        {
            switch (CurrentPosition.Orientation)
            {
                case "N":
                    CurrentPosition.Orientation = "E";
                    break;
                case "E":
                    CurrentPosition.Orientation = "S";
                    break;
                case "S":
                    CurrentPosition.Orientation = "W";
                    break;
                case "W":
                    CurrentPosition.Orientation = "N";
                    break;
                default:
                    throw new ArgumentException($"Invalid Rover orientation: {CurrentPosition.Orientation}");
            }
        }

        //Move Forward method to impliment to command 'M'
        public void MoveForward()
        {
            switch (CurrentPosition.Orientation)
            {
                case "N":
                    CurrentPosition.Y += 1;
                    break;
                case "E":
                    CurrentPosition.X += 1;
                    break;
                case "S":
                    CurrentPosition.Y -= 1;
                    break;
                case "W":
                    CurrentPosition.X -= 1;
                    break;
                default:
                    throw new ArgumentException($"Invalid Rover orientation: {CurrentPosition.Orientation}");
            }

            //If Rover falls out of bounds this snippet is triggered
            if (!Plateau.IsValidPosition(CurrentPosition))
            {
                throw new InvalidOperationException($"Rover has fallen off the plateau at position {CurrentPosition}");
            }
        }
    }
}
