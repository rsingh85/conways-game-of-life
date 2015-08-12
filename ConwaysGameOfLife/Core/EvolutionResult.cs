using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Core
{
    public class EvolutionResult
    {
        public int PopulationCount { get; private set; }
        public bool EvolutionEnded { get; private set; }
        public int GenerationNumber { get; private set; }

        public EvolutionResult(int populationCount, bool evolutionEnded, int generationNumber)
        {
            PopulationCount = populationCount;
            EvolutionEnded = evolutionEnded;
            GenerationNumber = generationNumber;
        }
    }
}
