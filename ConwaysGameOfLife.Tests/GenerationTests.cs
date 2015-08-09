using System;
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
            Generation generation = new Generation(worldSize: 2);

            // Act
            Cell cell = generation.GetCell(row: 3, column: 3);

            // Assert
            Assert.IsNull(cell);
        }

        [TestMethod]
        public void Should_SetAllCellsToDead_When_Initialised()
        {
            // Arrange and Act
            Generation generation = new Generation(worldSize: 5);

            // Assert
            for (int row = 0; row < generation.WorldSize; row++)
            {
                for (int column = 0; column < generation.WorldSize; column++)
                {
                    Assert.AreEqual(generation.GetCell(row, column).Alive, false);
                }
            }
        }
    }
}
