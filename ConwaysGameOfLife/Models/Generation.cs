using System;
using System.Text;

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
