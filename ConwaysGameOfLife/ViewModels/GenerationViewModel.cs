using ConwaysGameOfLife.Core;
using ConwaysGameOfLife.Models;
using ConwaysGameOfLife.Infrastructure;

namespace ConwaysGameOfLife.ViewModels
{
    /// <summary>
    /// A view model to represent the current generation in
    /// the game of life.
    /// </summary>
    public class GenerationViewModel
    {
        /// <summary>
        /// Life engine instance.
        /// </summary>
        private readonly LifeEngine engine;

        /// <summary>
        /// Gets the current generation world size.
        /// </summary>
        public int WorldSize { get { return engine.CurrentGeneration.WorldSize; } }

        /// <summary>
        /// RelayCommand for evolving the current generation.
        /// </summary>
        public RelayCommand<object> EvolveCommand { get; private set; }

        /// <summary>
        /// RelayCommand for toggling a particular cell's life.
        /// </summary>
        public RelayCommand<string> ToggleCellLifeCommand { get; private set; }

        /// <summary>
        /// Initialises a new instance of GenerationViewModel with the specified world size.
        /// </summary>
        /// <param name="worldSize">World size.</param>
        public GenerationViewModel(int worldSize)
        {
            EvolveCommand = new RelayCommand<object>(_ => EvolveGeneration());
            ToggleCellLifeCommand = new RelayCommand<string>((cellRowColumn) => ToggleCellLife(cellRowColumn));
            
            engine = new LifeEngine(new Generation(worldSize));
        }

        /// <summary>
        /// Gets the specified cell from the current generation.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        /// <returns></returns>
        public Cell GetCell(int row, int column)
        {
            return engine.CurrentGeneration.GetCell(row, column);
        }

        /// <summary>
        /// Evolves the current generation using the life engine.
        /// </summary>
        private void EvolveGeneration()
        {
            engine.EvolveToNextGeneration();
        }

        /// <summary>
        /// Makes a specfied cell alive or dead.
        /// </summary>
        /// <param name="cellRowColumn">Formatted string identifying a particular cell. Format is "rowIndex,columnIndex"<param>
        private void ToggleCellLife(string cellRowColumn)
        {
            string[] cellRowSplit = cellRowColumn.Split(',');

            int row = int.Parse(cellRowSplit[0]);
            int column = int.Parse(cellRowSplit[1]);

            engine.CurrentGeneration.ToggleCell(row, column);
        }
    }
}
