# Rover
Mars Rover project for interview challenge implemented using C# asp.net latest LTS (long Term Support) version.


# Mars Rover

This project is a C# asp.Net Core MVC web application that simulates the movement of robotic rovers on a rectangular plateau on Mars. The program takes input from a textArea that specifies the dimensions of the plateau and the starting positions and movement instructions for each rover. The output of the program is the final positions of the rovers.

# Design Decisions

The solution consists of three classes: Plateau, Position, and Rover. The Plateau class represents the rectangular plateau on Mars and has a method for checking whether a given position is valid. The Position class represents the position of a rover on the plateau and includes its x and y coordinates and orientation. The Rover class represents a robotic rover and includes its current position, a reference to the plateau on which it is exploring, and methods for executing instructions to move the rover.

To handle the input and output of the program, I used the asp.net Razor view templates to render a front-end and send data to the backend controller using HttpPost

# Software Requirements
To develop the solution, you will need the following software:

.NET 6.0 or later
A text editor or integrated development environment (IDE), such as Visual Studio Code or Visual Studio

# Getting Started
To run the program, follow these steps:

1. Clone this repository to your local machine.

2. Navigate to the project directory in a terminal or command prompt.

3. Run the following command to build the project:


dotnet build
Run the following command to run the program:

dotnet run --project MarsRover <input-file> <output-file>
Replace <input-file> with the path to the input file and <output-file> with the path to the output file.

For example:

css
Copy code
dotnet run --project MarsRover input.txt output.txt
Check the output file to see the final positions of the rovers.

Sample Input and Output
Here is an example of the input and output files for the program:

Input (input.txt)
Copy code
5 5
1 2 N
LMLMLMLMM
3 3 E
MMRMMRMRRM
Output (output.txt)
Copy code
1 3 N
5 1 E
This output indicates that the first rover ended up at position (1, 3) facing north, and the second rover ended up at position (5, 1) facing east.
