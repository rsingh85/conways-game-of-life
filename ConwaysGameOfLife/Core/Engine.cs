using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConwaysGameOfLife.Models;

namespace ConwaysGameOfLife.Core
{
    public class Engine
    {
        public Generation CurrentGeneration { get; private set; }

        public Engine(Generation initialGeneration)
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

                    int numberOfAliveNeighbors = CurrentGeneration.GetNumberOfAliveNeighbors(cell);

                    if (numberOfAliveNeighbors < UnderPopulationThreshold ||
                            numberOfAliveNeighbors > OverPopulationThreshold)
                    {
                        cellLifeChangeTupleList.Add(new Tuple<int, int, bool>(row, column, false));
                    }
                    else if (!cell.Alive && numberOfAliveNeighbors == ReproductionThreshold)
                    {
                        cellLifeChangeTupleList.Add(new Tuple<int, int, bool>(row, column, true));
                    }
                }
            }

            Parallel.ForEach<Tuple<int, int, bool>>(
                cellLifeChangeTupleList, 
                tuple => CurrentGeneration.SetCell(tuple.Item1, tuple.Item2, tuple.Item3)
            );
        }
    }
}
