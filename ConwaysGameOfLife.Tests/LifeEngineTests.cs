using Microsoft.VisualStudio.TestTools.UnitTesting;

using ConwaysGameOfLife.Core;
using ConwaysGameOfLife.Models;

namespace ConwaysGameOfLife.Tests
{
    [TestClass]
    public class LifeEngineTests
    {
        [TestMethod]
        public void Should_NotAlterInitialGenerationCells_When_Initialised()
        {
            // Arrange 
            Generation initialGeneration = new Generation(worldSize: 5);
            string expected = initialGeneration.ToString();

            // Act
            LifeEngine engine = new LifeEngine(initialGeneration);

            // Assert
            Assert.AreEqual(expected, engine.CurrentGeneration.ToString());
        }
        
        [TestMethod]
        public void Should_KillCell_When_CellHasFewerThanTwoNeighbors()
        {
            // Arrange 
            Generation initialGeneration = new Generation(worldSize: 5);
            initialGeneration.ToggleCellLife(0, 0);
            initialGeneration.ToggleCellLife(0, 1);

            // Act
            LifeEngine engine = new LifeEngine(initialGeneration);
            engine.EvolveToNextGeneration();

            // Assert
            Assert.AreEqual(initialGeneration.GetCell(0, 0).Alive, false);
            Assert.AreEqual(initialGeneration.GetCell(0, 1).Alive, false);
        }

        [TestMethod]
        public void Should_LetCellLive_When_CellHassTwoOrThreeLiveNeighbours()
        {
            // Arrange 
            Generation initialGeneration = new Generation(worldSize: 5);
            initialGeneration.ToggleCellLife(0, 0);
            initialGeneration.ToggleCellLife(0, 1);
            initialGeneration.ToggleCellLife(0, 2);

            // Act
            LifeEngine engine = new LifeEngine(initialGeneration);
            engine.EvolveToNextGeneration();

            // Assert
            Assert.AreEqual(initialGeneration.GetCell(0, 1).Alive, true);
        }

        [TestMethod]
        public void Should_KillCell_When_CellHasMoreThanTreeLiveNeighbours()
        {
            // Arrange 
            Generation initialGeneration = new Generation(worldSize: 5);
            initialGeneration.ToggleCellLife(0, 0);
            initialGeneration.ToggleCellLife(0, 1);
            initialGeneration.ToggleCellLife(0, 2);
            initialGeneration.ToggleCellLife(1, 0);
            initialGeneration.ToggleCellLife(1, 1);

            // Act
            LifeEngine engine = new LifeEngine(initialGeneration);
            engine.EvolveToNextGeneration();

            // Assert
            Assert.AreEqual(initialGeneration.GetCell(0, 1).Alive, false);
        }

        [TestMethod]
        public void Should_GiveLifeToCell_When_CellHasTreeLiveNeighbours()
        {
            // Arrange 
            Generation initialGeneration = new Generation(worldSize: 5);
            initialGeneration.ToggleCellLife(0, 0);
            initialGeneration.ToggleCellLife(0, 2);
            initialGeneration.ToggleCellLife(1, 0);

            // Act
            LifeEngine engine = new LifeEngine(initialGeneration);
            engine.EvolveToNextGeneration();

            // Assert
            Assert.AreEqual(initialGeneration.GetCell(0, 1).Alive, true);
        }
    }
}
