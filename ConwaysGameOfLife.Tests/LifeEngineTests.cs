using System;
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
    }
}
