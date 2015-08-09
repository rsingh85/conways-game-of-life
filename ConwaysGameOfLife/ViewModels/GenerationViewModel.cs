using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConwaysGameOfLife.Core;
using ConwaysGameOfLife.Models;

namespace ConwaysGameOfLife.ViewModels
{
    /// <summary>
    /// A view model to represent the current generation in
    /// the game of life.
    /// </summary>
    public class GenerationViewModel
    {
        private readonly Engine _engine;

        public Generation CurrentGeneration
        {
            get { return _engine.CurrentGeneration;  }
        }

        public GenerationViewModel(Generation initialGeneration)
        {
            _engine = new Engine(initialGeneration);
            
        }
    }
}
