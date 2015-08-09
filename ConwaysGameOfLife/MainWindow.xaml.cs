using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using ConwaysGameOfLife.Models;
using ConwaysGameOfLife.ViewModels;
using ConwaysGameOfLife.Infrastructure;

namespace ConradsGameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GenerationViewModel generationViewModel;

        public MainWindow()
        {
            InitializeComponent();

            generationViewModel = new GenerationViewModel(
                ConwaysGameOfLife.Properties.Settings.Default.WorldSize
            );
            
            BuildGridUI(generationViewModel);

            DataContext = generationViewModel;
        }

        private void BuildGridUI(GenerationViewModel generationViewModel)
        { 
            int worldSize = generationViewModel.CurrentGeneration.WorldSize;

            for (int row = 0; row < worldSize; row++)
            {
                WorldGrid.RowDefinitions.Add(new RowDefinition());

                for (int column = 0; column < worldSize; column++)
                {
                    if (row == 0)
                        WorldGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    Cell cell = generationViewModel.CurrentGeneration.GetCell(row, column);

                    TextBlock cellTextBlock = CreateCellTextBlock(cell);
                                        
                    Grid.SetRow(cellTextBlock, row);
                    Grid.SetColumn(cellTextBlock, column);

                    WorldGrid.Children.Add(cellTextBlock);
                }
            }
        }

        private TextBlock CreateCellTextBlock(Cell cell)
        {
            TextBlock cellTextBlock = new TextBlock();
            cellTextBlock.DataContext = cell;
            cellTextBlock.Background = LifeToColourConverter.DeadCellColour;

            InputBinding cellTextBlockInputBinding = new InputBinding(
                generationViewModel.ToggleCellLifeCommand,
                new MouseGesture(MouseAction.LeftClick)
            );
            cellTextBlockInputBinding.CommandParameter = string.Format("{0},{1}", cell.Row, cell.Column);

            cellTextBlock.InputBindings.Add(cellTextBlockInputBinding);

            Binding cellAliveBinding = new Binding();
            cellAliveBinding.Source = cell;
            cellAliveBinding.Path = new PropertyPath("Alive");
            cellAliveBinding.Mode = BindingMode.TwoWay;
            cellAliveBinding.Converter = new LifeToColourConverter();

            cellTextBlock.SetBinding(TextBlock.BackgroundProperty, cellAliveBinding);
            return cellTextBlock;
        }
    }
}
