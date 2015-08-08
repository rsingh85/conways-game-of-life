using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ConwaysGameOfLife.Core;
using ConwaysGameOfLife.Models;

namespace ConradsGameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitialiseGame();
        }

        private void InitialiseGame()
        {
            const int WorldSize = 10;
            
            InitialiseGrid(WorldSize);

            Generation initialGeneration = new Generation(WorldSize);
            initialGeneration.SetCell(0, 0, true);

            Engine engine = new Engine(initialGeneration);

        }

        private void InitialiseGrid(int worldSize)
        {
            for (int row = 0; row < worldSize; row++)
            {
                for (int column = 0; column < worldSize; column++)
                {
                    
                }
            }
        }

        private void DrawGeneration(Generation generation)
        {
            for (int row = 0; row < generation.WorldSize; row++)
            {
                for (int column = 0; column< generation.WorldSize; column++)
                {
                    Cell cell = generation.GetCell(row, column);


                }
            }
        }
    }
}
