namespace Rover.Models
{
    public class Plateau
    {
        public int UpperRightX { get; }
        public int UpperRightY { get; }

        public Plateau(int upperRightX, int upperRightY)
        {
            UpperRightX = upperRightX;
            UpperRightY = upperRightY;
        }

        public bool IsValidPosition(Position position)
        {
            return position.X >= 0 && position.X <= UpperRightX &&
                   position.Y >= 0 && position.Y <= UpperRightY;
        }
    }

    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Orientation { get; set; }

        public Position(int x, int y, string orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }

        public override string ToString()
        {
            return $"{X} {Y} {Orientation}";
        }
    }

    public class Mars_Rover
    {
        public Position CurrentPosition { get; private set; }
        public Plateau Plateau { get; }

        public Mars_Rover(Position startingPosition, Plateau plateau)
        {
            CurrentPosition = startingPosition;
            Plateau = plateau;
        }

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

            if (!Plateau.IsValidPosition(CurrentPosition))
            {
                throw new InvalidOperationException($"Rover has fallen off the plateau at position {CurrentPosition}");
            }
        }
    }
}
