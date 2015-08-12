using ConwaysGameOfLife.Models;

namespace ConwaysGameOfLife.Core
{
    public interface IEvolutionEngine 
    {
        /// <summary>
        /// Gets the current generation number.
        /// </summary>
        int CurrentGenerationNumber { get; }

        /// <summary>
        /// Applies Conway's life rules to evolve the current generation into the next generation.
        /// </summary>
        /// <returns>An engine result.</returns>
        EvolutionEngineActionResult EvolveGeneration();

        /// <summary>
        /// Resets the current generation.
        /// </summary>
        /// <returns>An EvolutionEngineActionResult.</returns>
        EvolutionEngineActionResult ResetGeneration();

        /// <summary>
        /// Gets the current universe size.
        /// </summary>
        /// <returns>Universe size.</returns>
        int GetUniverseSize();
        
        /// <summary>
        /// Gets the specified cell in the current generation.
        /// </summary>
        /// <param name="row">Row index of cell.</param>
        /// <param name="column">COlumn index of cell.</param>
        /// <returns>The specified cell.</returns>
        Cell GetCell(int row, int column);

        /// <summary>
        /// Sets a particular cell in the current generation to be dead or alive.
        /// </summary>
        /// <param name="row">Row index of the cell.</param>
        /// <param name="column">Column index of the cell.</param>
        /// <param name="alive">A boolean value that indicates if this cell is dead or alive.</param>
        void SetCell(int row, int column, bool alive);

        /// <summary>
        /// Toggles the living status of a cell in the current generatio.
        /// </summary>
        /// <param name="row">Row index of the cell.</param>
        /// <param name="column">Colummn index of cell.</param>
        void ToggleCellLife(int row, int column);
    }
}
