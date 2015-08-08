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
using ConwaysGameOfLife.ViewModels;

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
                        
            var generationViewModel = new GenerationViewModel(
                GetInitialGenerationFromUI()
            );

            DataContext = generationViewModel;
        }

        private Generation GetInitialGenerationFromUI()
        {
            // TODO: Build a Generation object from current UI state

            var generation = new Generation(100);
            generation.SetCell(0, 0, true);

            return generation;
        }
    }
}
