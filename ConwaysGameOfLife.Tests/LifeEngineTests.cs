﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using ConwaysGameOfLife.Core;
using ConwaysGameOfLife.Models;

namespace ConwaysGameOfLife.Tests
{
    [TestClass]
    public class LifeEngineTests
    {        
        [TestMethod]
        public void Should_KillCell_When_CellHasFewerThanTwoNeighbors()
        {
            // Arrange 
            Generation initialGeneration = new Generation(universeSize: 5);
            initialGeneration.ToggleCellLife(0, 0);
            initialGeneration.ToggleCellLife(0, 1);

            // Act
            EvolutionEngine engine = new EvolutionEngine(initialGeneration);
            engine.EvolveGeneration();

            // Assert
            Assert.AreEqual(initialGeneration.GetCell(0, 0).Alive, false);
            Assert.AreEqual(initialGeneration.GetCell(0, 1).Alive, false);
        }

        [TestMethod]
        public void Should_LetCellLive_When_CellHassTwoOrThreeLiveNeighbours()
        {
            // Arrange 
            Generation initialGeneration = new Generation(universeSize: 5);
            initialGeneration.ToggleCellLife(0, 0);
            initialGeneration.ToggleCellLife(0, 1);
            initialGeneration.ToggleCellLife(0, 2);

            // Act
            EvolutionEngine engine = new EvolutionEngine(initialGeneration);
            engine.EvolveGeneration();

            // Assert
            Assert.AreEqual(initialGeneration.GetCell(0, 1).Alive, true);
        }

        [TestMethod]
        public void Should_KillCell_When_CellHasMoreThanTreeLiveNeighbours()
        {
            // Arrange 
            Generation initialGeneration = new Generation(universeSize: 5);
            initialGeneration.ToggleCellLife(0, 0);
            initialGeneration.ToggleCellLife(0, 1);
            initialGeneration.ToggleCellLife(0, 2);
            initialGeneration.ToggleCellLife(1, 0);
            initialGeneration.ToggleCellLife(1, 1);

            // Act
            EvolutionEngine engine = new EvolutionEngine(initialGeneration);
            engine.EvolveGeneration();

            // Assert
            Assert.AreEqual(initialGeneration.GetCell(0, 1).Alive, false);
        }

        [TestMethod]
        public void Should_GiveLifeToCell_When_CellHasTreeLiveNeighbours()
        {
            // Arrange 
            Generation initialGeneration = new Generation(universeSize: 5);
            initialGeneration.ToggleCellLife(0, 0);
            initialGeneration.ToggleCellLife(0, 2);
            initialGeneration.ToggleCellLife(1, 0);

            // Act
            EvolutionEngine engine = new EvolutionEngine(initialGeneration);
            engine.EvolveGeneration();

            // Assert
            Assert.AreEqual(initialGeneration.GetCell(0, 1).Alive, true);
        }
    }
}
