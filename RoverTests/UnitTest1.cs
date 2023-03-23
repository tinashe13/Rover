using System;
using System.Collections.Generic;
using System.Linq;
using Rover.Models;
using Xunit;
using MarsRover.Models;

namespace MarsRoverTests
{
    public class MarsRoverTests
    {
        [Fact]
        public void MarsRover_StartsAtCorrectPosition()
        {
            // Arrange
            var startingPosition = new Position(1, 2, "N");
            var plateau = new Plateau(5, 5);

            // Act
            var rover = new Mars_Rover(startingPosition, plateau);

            // Assert
            Assert.Equal(startingPosition.ToString(), rover.CurrentPosition.ToString());
        }

        [Fact]
        public void MarsRover_TurnsLeft()
        {
            // Arrange
            var startingPosition = new Position(1, 2, "N");
            var plateau = new Plateau(5, 5);
            var rover = new Mars_Rover(startingPosition, plateau);

            // Act
            rover.TurnLeft();

            // Assert
            Assert.Equal("W", rover.CurrentPosition.Orientation);
        }

        [Fact]
        public void MarsRover_TurnsRight()
        {
            // Arrange
            var startingPosition = new Position(1, 2, "N");
            var plateau = new Plateau(5, 5);
            var rover = new Mars_Rover(startingPosition, plateau);

            // Act
            rover.TurnRight();

            // Assert
            Assert.Equal("E", rover.CurrentPosition.Orientation);
        }

        [Fact]
        public void MarsRover_MovesForward()
        {
            // Arrange
            var startingPosition = new Position(1, 2, "N");
            var plateau = new Plateau(5, 5);
            var rover = new Mars_Rover(startingPosition, plateau);

            // Act
            rover.MoveForward();

            // Assert
            var expectedPosition = new Position(1, 3, "N");
            Assert.Equal(expectedPosition.ToString(), rover.CurrentPosition.ToString());
        }

        [Fact]
        public void MarsRover_MovesAndTurns()
        {
            // Arrange
            var startingPosition = new Position(1, 2, "N");
            var plateau = new Plateau(5, 5);
            var rover = new Mars_Rover(startingPosition, plateau);

            // Act
            rover.ExecuteInstructions("LMLMLMLMM");

            // Assert
            var expectedPosition = new Position(1, 3, "N");
            Assert.Equal(expectedPosition.ToString(), rover.CurrentPosition.ToString());
        }

        [Fact]
        public void MarsRover_FallsOffPlateau()
        {
            // Arrange
            var startingPosition = new Position(5, 5, "N");
            var plateau = new Plateau(5, 5);
            var rover = new Mars_Rover(startingPosition, plateau);

            // Act and Assert
            Assert.Throws<InvalidOperationException>(() => rover.MoveForward());
        }

        [Fact]
        public void ExecuteInstructions_ChangesPositionCorrectly()
        {
            // Arrange
            var plateau = new Plateau(5, 5);
            var startingPosition = new Position(1, 2, "N");
            var marsRover = new Mars_Rover(startingPosition, plateau);

            // Act
            marsRover.ExecuteInstructions("LMLMLMLMM");

            // Assert
            Assert.Equal("1 3 N", marsRover.CurrentPosition.ToString());
        }

        [Fact]
        public void ExecuteInstructions_ThrowsException_WhenInvalidInstructionIsGiven()
        {
            // Arrange
            var plateau = new Plateau(5, 5);
            var startingPosition = new Position(0, 0, "N");
            var marsRover = new Mars_Rover(startingPosition, plateau);

            // Act and Assert
            Assert.Throws<ArgumentException>(() => marsRover.ExecuteInstructions("MLRXYZ"));
        }

    }
}