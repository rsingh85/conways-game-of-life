namespace ConwaysGameOfLife.Models
{
    /// <summary>
    /// Represents a single cell, its position and whether it is dead or alive.
    /// </summary>
    public class Cell : ObservableBase
    {
        public int Row { get; set; }
        public int Column { get; set; }

        private bool alive;
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

        public Cell(int row, int column, bool alive)
        {
            Row = row;
            Column = column;
            Alive = alive;
        }

        public override string ToString()
        {
            return string.Format(
                "Cell ({0},{1}) - {2}", Row, Column, Alive ? "Alive" : "Dead"
            );
        }
    }
}
