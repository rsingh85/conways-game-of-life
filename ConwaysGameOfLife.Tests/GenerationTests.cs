using Microsoft.VisualStudio.TestTools.UnitTesting;

using ConwaysGameOfLife.Models;

namespace ConwaysGameOfLife.Tests
{
    [TestClass]
    public class GenerationTests
    {
        [TestMethod]
        public void Should_ReturnNull_When_OutOfBoundsCellRetrieved()
        {
            // Arrange
            Generation generation = new Generation(universeSize: 2);

            // Act
            Cell cell = generation.GetCell(row: 3, column: 3);

            // Assert
            Assert.IsNull(cell);
        }

        [TestMethod]
        public void Should_SetAllCellsToDead_When_Initialised()
        {
            // Arrange
            Generation generation;
            
            // Act
            generation = new Generation(universeSize: 5);

            // Assert
            for (int row = 0; row < generation.UniverseSize; row++)
            {
                for (int column = 0; column < generation.UniverseSize; column++)
                {
                    Assert.AreEqual(generation.GetCell(row, column).Alive, false);
                }
            }
        }

        [TestMethod]
        public void Should_MakeCellAlive_When_DeadCellToggled()
        {
            // Arrange
            Generation initialGeneration = new Generation(universeSize: 2);

            // Act
            initialGeneration.ToggleCellLife(0, 0);

            // Assert
            Assert.AreEqual(initialGeneration.GetCell(0, 0).Alive, true);
        }

        [TestMethod]
        public void Should_MakeCellDead_When_AliveCellToggled()
        {
            // Arrange
            Generation initialGeneration = new Generation(universeSize: 2);
            initialGeneration.SetCell(0, 0, true);

            // Act
            initialGeneration.ToggleCellLife(0, 0);

            // Assert
            Assert.AreEqual(initialGeneration.GetCell(0, 0).Alive, false);
        }
    }
}
