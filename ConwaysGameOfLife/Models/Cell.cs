using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Models
{
    /// <summary>
    /// Represents a single cell, its position and whether it is dead or alive.
    /// </summary>
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        
        public bool Alive { get; set; }

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
