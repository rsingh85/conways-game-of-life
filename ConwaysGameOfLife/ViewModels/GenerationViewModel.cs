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
        private readonly LifeEngine engine;

        public RelayCommand<object> EvolveCommand { get; private set; }
        public RelayCommand<string> ToggleCellLifeCommand { get; private set; }

        public Generation CurrentGeneration
        {
            get { return engine.CurrentGeneration;  }
        }

        public GenerationViewModel(int worldSize)
        {
            engine = new LifeEngine(new Generation(worldSize));
            
            EvolveCommand = new RelayCommand<object>(
                _ => EvolveGeneration(), 
                _ => CanEvolveGeneration()
            );
            
            ToggleCellLifeCommand = new RelayCommand<string>(
                (cellRowColumn) => ToggleCellLife(cellRowColumn), 
                _ => CanToggleCellLife()
            );
        }

        private void EvolveGeneration()
        {
            engine.EvolveToNextGeneration();
        }

        private bool CanEvolveGeneration()
        {
            return true;
        }

        private void ToggleCellLife(string cellRowColumn)
        {
            string[] cellRowSplit = cellRowColumn.Split(',');

            int row = int.Parse(cellRowSplit[0]);
            int column = int.Parse(cellRowSplit[1]);

            Cell cell = engine.CurrentGeneration.GetCell(row, column);
            cell.Alive = !cell.Alive;
        }

        private bool CanToggleCellLife()
        {
            return true;
        }
    }
}
