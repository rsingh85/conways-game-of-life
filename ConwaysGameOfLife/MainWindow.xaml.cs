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

            BuildGridUI(generationViewModel);
        }

        private void BuildGridUI(GenerationViewModel generationViewModel)
        {
            const int WorldSize = 10;

            WorldGrid.ShowGridLines = true;

            for (int row = 0; row < WorldSize; row++)
            {
                // Add a row
                WorldGrid.RowDefinitions.Add(new RowDefinition());

                for (int column = 0; column < WorldSize; column++)
                {
                    // If it's the first row, add a column
                    if (row == 0)
                        WorldGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    Cell cell = generationViewModel.CurrentGeneration.GetCell(row, column);
                    
                    TextBlock cellTextBlock = new TextBlock();
                    cellTextBlock.DataContext = cell;
                    cellTextBlock.Background = Brushes.Red;

                    Binding cellAliveBinding = new Binding();
                    cellAliveBinding.Source = cell;
                    cellAliveBinding.Path = new PropertyPath("Alive");
                    cellAliveBinding.Mode = BindingMode.TwoWay;
                    cellAliveBinding.Converter = new BooleanToVisibilityConverter();

                    cellTextBlock.SetBinding(TextBlock.VisibilityProperty, cellAliveBinding);

                    Grid.SetRow(cellTextBlock, row);
                    Grid.SetColumn(cellTextBlock, column);

                    WorldGrid.Children.Add(cellTextBlock);
                }
            }
        }

        private Generation GetInitialGenerationFromUI()
        {
            // TODO: Build a Generation object from current UI state

            var generation = new Generation(100);
            generation.SetCell(0, 0, true);
            generation.SetCell(0, 1, true);

            return generation;
        }

        private void AButton_Click(object sender, RoutedEventArgs e)
        {
            ((Cell)DataContext).Alive = false;
        }
    }
}
