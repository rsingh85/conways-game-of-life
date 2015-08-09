using System;
using System.Text;

namespace ConwaysGameOfLife.Models
{
    /// <summary>
    /// Represents a particular generation of the game of life.
    /// </summary>
    public class Generation
    {
        /// <summary>
        /// A two-dimensional array representing the world.
        /// </summary>
        private readonly Cell[,] worldGrid;

        /// <summary>
        /// Get the size of the world.
        /// </summary>
        public int WorldSize { get; private set; }
        
        /// <summary>
        /// Initialises a new instance of a Generation.
        /// </summary>
        /// <param name="worldSize">Size of the world.</param>
        public Generation(int worldSize)
        {
            worldGrid = new Cell[worldSize, worldSize];
            WorldSize = worldSize;

            InitialiseWorldGrid();
        }

        /// <summary>
        /// Initialises the world grid.
        /// </summary>
        private void InitialiseWorldGrid()
        {
            for (int row = 0; row < WorldSize; row++)
                for (int column = 0; column < WorldSize; column++)
                    worldGrid[row, column] = new Cell(row, column, false);
        }

        /// <summary>
        /// Gets a specified cell.
        /// </summary>
        /// <param name="row">Row index of the cell.</param>
        /// <param name="column">Column index of the cell.</param>
        /// <returns>The specified cell.</returns>
        public Cell GetCell(int row, int column)
        {
            if (row < 0 || row >= WorldSize)
                return null;

            if (column < 0 || column >= WorldSize)
                return null;

            return worldGrid[row, column];
        }

        /// <summary>
        /// Sets a particular cell in the world to be dead or alive.
        /// </summary>
        /// <param name="row">Row index of the cell.</param>
        /// <param name="column">Column index of the cell.</param>
        /// <param name="alive">A boolean value that indicates if this cell is dead or alive.</param>
        public void SetCell(int row, int column, bool alive)
        {
            Cell cell = GetCell(row, column);

            if (cell == null)
                throw new ArgumentOutOfRangeException(
                    "The specified row and column do not map to a valid cell."
                );

            cell.Alive = alive;
        }

        /// <summary>
        /// Toggles the living status of a cell.
        /// </summary>
        /// <param name="row">Row index of the cell.</param>
        /// <param name="column">Colummn index of cell.</param>
        public void ToggleCellLife(int row, int column)
        {
            Cell cell = GetCell(row, column);
            cell.Alive = !cell.Alive;
        }

        /// <summary>
        /// Builds a string representation of this generation.
        /// </summary>
        /// <returns>String representation of this generation.</returns>
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
