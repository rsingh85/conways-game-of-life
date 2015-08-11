namespace ConwaysGameOfLife.Models
{
    /// <summary>
    /// Represents a single observable cell, its position and whether it is dead or alive.
    /// </summary>
    public class Cell : ObservableBase
    {
        /// <summary>
        /// Gets the row index for this cell.
        /// </summary>
        public int Row { get; private set; }

        /// <summary>
        /// Gets the column index for this cell.
        /// </summary>
        public int Column { get; private set; }

        /// <summary>
        /// A boolean value that indicates if this cell is dead or alive.
        /// </summary>
        private bool alive;

        /// <summary>
        /// Gets or sets a value that indicates of this cell is dead or alive.
        /// </summary>
        public bool Alive
        {
            get { return alive; }
            set
            {
                if (value != alive)
                {
                    alive = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Initialises a new instance of a Cell.
        /// </summary>
        /// <param name="row">Row index of the cell.</param>
        /// <param name="column">Column index of the cell.</param>
        /// <param name="alive">A boolean value that indicates if this cell is dead or alive.</param>
        public Cell(int row, int column, bool alive)
        {
            Row = row;
            Column = column;
            Alive = alive;
        }

        /// <summary>
        /// Builds a string representation of this cell.
        /// </summary>
        /// <returns>String representation of this cell.</returns>
        public override string ToString()
        {
            return string.Format(
                "Cell ({0},{1}) - {2}", Row, Column, Alive ? "Alive" : "Dead"
            );
        }
    }
}
