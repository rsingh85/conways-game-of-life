using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ConwaysGameOfLife.Models;
using System.Linq;

namespace ConwaysGameOfLife.Core
{
    /// <summary>
    /// The engine that implements Conway's Game of Life rules.
    /// </summary>
    public class EvolutionEngine : IEvolutionEngine
    {
        /// <summary>
        /// Gets the current generation.
        /// </summary>
        private Generation CurrentGeneration { get; set; }

        /// <summary>
        /// Gets the current generation number.
        /// </summary>
        public int CurrentGenerationNumber { get; private set; }

        /// <summary>
        /// Initialises a new instance of the LifeEngine with a specified initial generation.
        /// </summary>
        /// <param name="initialGeneration">The initial generation to start from.</param>
        public EvolutionEngine(Generation initialGeneration)
        {
            CurrentGeneration = initialGeneration;
        }

        /// <summary>
        /// Applies Conway's life rules to evolve the current generation into the next generation.
        /// </summary>
        /// <returns>An EvolutionEngineActionResult.</returns>
        public EvolutionEngineActionResult EvolveGeneration()
        {
            const int UnderPopulationThreshold = 2,
                OverPopulationThreshold = 3,
                ReproductionThreshold = 3;

            IList<Tuple<int, int, bool>> cellLifeChangeTupleList = new List<Tuple<int, int, bool>>();

            for (int row = 0; row < CurrentGeneration.UniverseSize; row++)
            {
                for (int column = 0; column < CurrentGeneration.UniverseSize; column++)
                {
                    Cell cell = CurrentGeneration.GetCell(row, column);

                    int numberOfAliveNeighbors = GetNumberOfAliveNeighbors(CurrentGeneration, cell);

                    if (cell.Alive && (numberOfAliveNeighbors < UnderPopulationThreshold || numberOfAliveNeighbors > OverPopulationThreshold))
                    {
                        cellLifeChangeTupleList.Add(new Tuple<int, int, bool>(row, column, false));
                    }
                    else if (!cell.Alive && numberOfAliveNeighbors == ReproductionThreshold)
                    {
                        cellLifeChangeTupleList.Add(new Tuple<int, int, bool>(row, column, true));
                    }
                }
            }

            if (cellLifeChangeTupleList.Any())
            {
                CurrentGenerationNumber++;

                Parallel.ForEach(
                    cellLifeChangeTupleList,
                    tuple => CurrentGeneration.SetCell(tuple.Item1, tuple.Item2, tuple.Item3)
                );
            }

            return new EvolutionEngineActionResult(
                evolutionEnded: !cellLifeChangeTupleList.Any(),
                generationNumber: CurrentGenerationNumber
            );
        }
        
        /// <summary>
        /// Resets the current generation.
        /// </summary>
        /// <returns>An EvolutionEngineActionResult.</returns>
        public EvolutionEngineActionResult ResetGeneration()
        {
            CurrentGeneration.Reset();

            CurrentGenerationNumber = 0;

            return new EvolutionEngineActionResult(
                evolutionEnded: false,
                generationNumber: CurrentGenerationNumber
            );
        }
        
        /// <summary>
        /// Gets the current universe size.
        /// </summary>
        /// <returns>Universe size.</returns>
        public int GetUniverseSize()
        {
            return CurrentGeneration.UniverseSize;
        }

        /// <summary>
        /// Gets the specified cell in the current generation.
        /// </summary>
        /// <param name="row">Row index of cell.</param>
        /// <param name="column">COlumn index of cell.</param>
        /// <returns>The specified cell.</returns>
        public Cell GetCell(int row, int column)
        {
            return CurrentGeneration.GetCell(row, column);
        }

        /// <summary>
        /// Sets a particular cell in the current generation to be dead or alive.
        /// </summary>
        /// <param name="row">Row index of the cell.</param>
        /// <param name="column">Column index of the cell.</param>
        /// <param name="alive">A boolean value that indicates if this cell is dead or alive.</param>
        public void SetCell(int row, int column, bool alive)
        {
            CurrentGeneration.SetCell(row, column, alive);
        }

        /// <summary>
        /// Toggles the living status of a cell in the current generatio.
        /// </summary>
        /// <param name="row">Row index of the cell.</param>
        /// <param name="column">Colummn index of cell.</param>
        public void ToggleCellLife(int row, int column)
        {
            CurrentGeneration.ToggleCellLife(row, column);
        }

        /// <summary>
        /// Gets the number of neighbor cells that are alive for a particular cell.
        /// </summary>
        /// <param name="generation">The generation.</param>
        /// <param name="cell">The cell whose living neighbour are being counted.</param>
        /// <returns>Number of alive neighbours for the specified cell.</returns>
        private int GetNumberOfAliveNeighbors(Generation generation, Cell cell)
        {
            int numberOfAliveNeighbours = 0;

            List<Cell> neighboringCells = new List<Cell>
            {
                generation.GetCell(cell.Row - 1, cell.Column - 1),
                generation.GetCell(cell.Row - 1, cell.Column + 1),
                generation.GetCell(cell.Row, cell.Column + 1),
                generation.GetCell(cell.Row + 1, cell.Column + 1),
                generation.GetCell(cell.Row + 1, cell.Column),
                generation.GetCell(cell.Row + 1, cell.Column - 1),
                generation.GetCell(cell.Row, cell.Column - 1),
                generation.GetCell(cell.Row - 1, cell.Column)
            };

            neighboringCells.ForEach(
                neighboringCell => numberOfAliveNeighbours +=
                    (neighboringCell != null && neighboringCell.Alive) ? 1 : 0
            );

            return numberOfAliveNeighbours;
        }
    }
}