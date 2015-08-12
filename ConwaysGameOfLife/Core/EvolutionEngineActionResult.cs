using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGameOfLife.Core
{
    public class EvolutionEngineActionResult
    {
        public bool EvolutionEnded { get; private set; }
        public int GenerationNumber { get; private set; }

        public EvolutionEngineActionResult(bool evolutionEnded, int generationNumber)
        {
            EvolutionEnded = evolutionEnded;
            GenerationNumber = generationNumber;
        }
    }
}
