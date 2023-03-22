namespace MarsRover.Models
{
    public class Position
    {
        //initiate position class parameters
        public int X { get; set; }
        public int Y { get; set; }
        public string Orientation { get; set; }

        public Position(int x, int y, string orientation)
        {
            X = x;
            Y = y;
            Orientation = orientation;
        }

        //override the default to string parameter
        public override string ToString()
        {
            return $"{X} {Y} {Orientation}";
        }
    }
}
