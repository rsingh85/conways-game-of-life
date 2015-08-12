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
    public class EvolutionEngine
    {
        /// <summary>
        /// Gets the current generation.
        /// </summary>
        public Generation CurrentGeneration { get; private set; }

        /// <summary>
        /// Gets the current generation number.
        /// </summary>
        public int GenerationNumber { get; private set; }

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
        /// <returns>Evolution result.</returns>
        public EvolutionResult EvolveToNextGeneration()
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

                    if (cell.Alive &&
                            (numberOfAliveNeighbors < UnderPopulationThreshold ||
                                numberOfAliveNeighbors > OverPopulationThreshold))
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
                GenerationNumber++;

                Parallel.ForEach(
                    cellLifeChangeTupleList,
                    tuple => CurrentGeneration.SetCell(tuple.Item1, tuple.Item2, tuple.Item3)
                );
            }

            return new EvolutionResult(
                populationCount: CurrentGeneration.PopulationCount,
                evolutionEnded: !cellLifeChangeTupleList.Any(),
                generationNumber: GenerationNumber
            );
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