using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Models
{
    public class Generation
    {
        private Cell[,] WorldGrid { get; set; }

        public int WorldSize { get; private set; }

        public Generation(int worldSize)
        {
            WorldGrid = new Cell[worldSize, worldSize];
            WorldSize = worldSize;

            InitialiseWorldGrid();
        }

        private void InitialiseWorldGrid()
        {
            for (int row = 0; row < WorldSize; row++)
            {
                for (int column = 0; column < WorldSize; column++)
                {
                    WorldGrid[row, column] = new Cell(row, column, false);
                }
            }
        }

        public void SetCell(int row, int column, bool alive)
        {
            Cell cell = GetCell(row, column);

            if (cell == null)
                throw new ArgumentOutOfRangeException(
                    "The specified row and column do not map to a valid cell."
                );

            cell.Alive = alive;
        }

        public Cell GetCell(int row, int column)
        {
            if (row < 0 || row >= WorldSize)
                return null;

            if (column < 0 || column >= WorldSize)
                return null;

            return WorldGrid[row, column];
        }

        public int GetNumberOfAliveNeighbors(Cell cell)
        {
            int numberOfAliveNeighbours = 0;

            List<Cell> neighboringCells = new List<Cell>
            {
                GetCell(cell.Row - 1, cell.Column - 1),   // Top-left
                GetCell(cell.Row - 1, cell.Column),       // Top
                GetCell(cell.Row - 1, cell.Column + 1),   // Top-right
                GetCell(cell.Row, cell.Column + 1),       // Right
                GetCell(cell.Row + 1, cell.Column + 1),   // Bottom- right
                GetCell(cell.Row + 1, cell.Column),       // Bottom
                GetCell(cell.Row + 1, cell.Column - 1),   // Bottom-left
                GetCell(cell.Row, cell.Column - 1)        // Left
            };

            neighboringCells.ForEach(
                neighboringCell => numberOfAliveNeighbours += (neighboringCell != null && neighboringCell.Alive) ? 1 : 0
            );

            return numberOfAliveNeighbours;
        }
        
        public override string ToString()
        {
            var gridString = new StringBuilder();

            for (int row = 0; row < WorldSize; row++)
            {
                for (int column = 0; column < WorldSize; column++)
                {
                    gridString.Append(
                        string.Format("{0} ", GetCell(row, column).Alive ? "1" : "0")
                    );
                }

                gridString.AppendLine();
            }

            return gridString.ToString();
        }
    }
}
