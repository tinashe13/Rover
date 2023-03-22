namespace MarsRover.Models
{
    public class Plateau
    {
        //intiate parameters for the Plateau class
        public int UpperRightX { get; }
        public int UpperRightY { get; }

        public Plateau(int upperRightX, int upperRightY)
        {
            UpperRightX = upperRightX;
            UpperRightY = upperRightY;
        }

        //check if position is within the plateau parameters
        public bool IsValidPosition(Position position)
        {
            return position.X >= 0 && position.X <= UpperRightX &&
                   position.Y >= 0 && position.Y <= UpperRightY;
        }
    }
}
