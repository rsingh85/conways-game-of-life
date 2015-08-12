using ConwaysGameOfLife.Core;
using ConwaysGameOfLife.Models;
using ConwaysGameOfLife.Infrastructure;
using System;

namespace ConwaysGameOfLife.ViewModels
{
    /// <summary>
    /// A view model to represent the current generation in
    /// the game of life.
    /// </summary>
    public class GenerationViewModel : ObservableBase
    {
        /// <summary>
        /// Life engine instance.
        /// </summary>
        private readonly IEvolutionEngine engine;

        /// <summary>
        /// Gets the current universe size.
        /// </summary>
        public int UniverseSize { get { return engine.GetUniverseSize(); } }

        #region PopulationCount Property
        /// <summary>
        /// Count of the current population.
        /// </summary>
        private int populationCount;

        /// <summary>
        /// Number
        /// </summary>
        public int PopulationCount
        {
            get { return populationCount; }
            private set
            {
                populationCount = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region GenerationNumber Property
        /// <summary>
        /// Number of generations the current generation has evolved from.
        /// </summary>
        private int generationNumber;

        /// <summary>
        /// Gets the current generation number.
        /// </summary>
        public int GenerationNumber
        {
            get { return generationNumber; }
            private set
            {
                generationNumber = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region EvolutionEnded Property
        /// <summary>
        /// Indicates whether the generation has stopped evolving.
        /// </summary>
        private bool evolutionEnded;

        /// <summary>
        /// Gets a boolean which indicates if the generation has stopped evolving.
        /// </summary>
        public bool EvolutionEnded
        {
            get { return evolutionEnded; }
            private set
            {
                evolutionEnded = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Command Properties
        /// <summary>
        /// RelayCommand for evolving the current generation.
        /// </summary>
        public RelayCommand<object> EvolveCommand { get; private set; }

        /// <summary>
        /// Relay command for resetting the game of life.
        /// </summary>
        public RelayCommand<object> ResetCommand { get; private set; }

        /// <summary>
        /// RelayCommand for toggling a particular cell's life.
        /// </summary>
        public RelayCommand<string> ToggleCellLifeCommand { get; private set; }
        #endregion

        /// <summary>
        /// Initialises a new instance of GenerationViewModel with the specified universe size.
        /// </summary>
        /// <param name="universeSize">Universesize.</param>
        public GenerationViewModel(int universeSize)
        {
            engine = new EvolutionEngine(new Generation(universeSize));

            EvolveCommand = new RelayCommand<object>(
                _ => EvolveGeneration(), 
                _ => CanEvolveGeneration()
            );

            ResetCommand = new RelayCommand<object>(_ => ResetGameOfLife());

            ToggleCellLifeCommand = new RelayCommand<string>(
                (cellRowColumn) => ToggleCellLife(cellRowColumn),
                _ => CanToggleCellLife()
            );
        }

        /// <summary>
        /// Gets the specified cell from the current generation.
        /// </summary>
        /// <param name="row">Row index.</param>
        /// <param name="column">Column index.</param>
        /// <returns></returns>
        public Cell GetCell(int row, int column)
        {
            return engine.GetCell(row, column);
        }

        /// <summary>
        /// Evolves the current generation.
        /// </summary>
        private void EvolveGeneration()
        {
            EvolutionEngineActionResult result = engine.EvolveGeneration();
            
            GenerationNumber = result.GenerationNumber;
            EvolutionEnded = result.EvolutionEnded;
        }

        /// <summary>
        /// Resets the game of life.
        /// </summary>
        private void ResetGameOfLife()
        {
            EvolutionEngineActionResult result = engine.ResetGeneration();

            GenerationNumber = result.GenerationNumber;
            EvolutionEnded = result.EvolutionEnded;
        }

        /// <summary>
        /// Determines if the current generation can be evolved.
        /// </summary>
        /// <returns>A boolean value which indicates if the current generation can further evolve.</returns>
        private bool CanEvolveGeneration()
        {
            return !EvolutionEnded;
        }

        /// <summary>
        /// Makes a specfied cell alive or dead.
        /// </summary>
        /// <param name="cellRowColumn">Formatted string identifying a particular cell. Format is "rowIndex,columnIndex"<param>
        private void ToggleCellLife(string cellRowColumn)
        {
            string[] cellRowColumnSplit = cellRowColumn.Split(',');

            int row = int.Parse(cellRowColumnSplit[0]);
            int column = int.Parse(cellRowColumnSplit[1]);

            engine.ToggleCellLife(row, column);
        }

        /// <summary>
        /// Determines if the cell life can be toggled.
        /// </summary>
        /// <returns>A boolea value which indicates if the cell life can be toggled.</returns>
        private bool CanToggleCellLife()
        {
            return GenerationNumber == 0 && !EvolutionEnded;
        }
    }
}