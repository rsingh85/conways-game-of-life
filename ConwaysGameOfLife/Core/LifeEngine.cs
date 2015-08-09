using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ConwaysGameOfLife.Models;

namespace ConwaysGameOfLife.Core
{
    public class LifeEngine
    {
        public Generation CurrentGeneration { get; private set; }

        public LifeEngine(Generation initialGeneration)
        {
            CurrentGeneration = initialGeneration;
        }

        public void EvolveToNextGeneration()
        {
            const int UnderPopulationThreshold = 2,
                OverPopulationThreshold = 3,
                ReproductionThreshold = 3;

            IList<Tuple<int, int, bool>> cellLifeChangeTupleList = new List<Tuple<int, int, bool>>();

            for (int row = 0; row < CurrentGeneration.WorldSize; row++)
            {
                for (int column = 0; column < CurrentGeneration.WorldSize; column++)
                {
                    Cell cell = CurrentGeneration.GetCell(row, column);

                    int numberOfAliveNeighbors = GetNumberOfAliveNeighbors(CurrentGeneration, cell);

                    if (numberOfAliveNeighbors < UnderPopulationThreshold || numberOfAliveNeighbors > OverPopulationThreshold)
                        cellLifeChangeTupleList.Add(new Tuple<int, int, bool>(row, column, false));
                    else if (!cell.Alive && numberOfAliveNeighbors == ReproductionThreshold)
                        cellLifeChangeTupleList.Add(new Tuple<int, int, bool>(row, column, true));
                }
            }

            Parallel.ForEach(
                cellLifeChangeTupleList, 
                tuple => CurrentGeneration.SetCell(tuple.Item1, tuple.Item2, tuple.Item3)
            );
        }

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
