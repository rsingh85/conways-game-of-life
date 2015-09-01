using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ConwaysGameOfLife.Infrastructure
{
    /// <summary>
    /// Enables binding conversion between a cell's living status and a colour for whether
    /// the cell is dead or alive.
    /// </summary>
    public class LifeToColourConverter : IValueConverter
    {
        /// <summary>
        /// Gets the colour that represents alive.
        /// </summary>
        public SolidColorBrush AliveColour { get; private set; }

        /// <summary>
        /// Gets the colour that represents dead.
        /// </summary>
        public SolidColorBrush DeadColour { get; private set; }

        public LifeToColourConverter(SolidColorBrush aliveColour, SolidColorBrush deadColour)
        {
            AliveColour = aliveColour;
            DeadColour = deadColour;
        }

        /// <summary>
        /// Converts a boolean value that indicates a cell's living status to a colour.
        /// </summary>
        /// <param name="value">A boolean value indicating a cell's living status.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        /// <returns>A SolidColorBrush instance (AliveCellColour or DeadCellColour).</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool alive = false;

            if (value is bool)
                alive = (bool) value;
            
            return alive ? AliveColour : DeadColour;
        }

        /// <summary>
        /// Converts a cell's colour back to a boolean that indicates whether the cell is dead or alive.
        /// </summary>
        /// <param name="value">A SolidColorBrush indicating a cell's living status.</param>
        /// <param name="targetType">Target type.</param>
        /// <param name="parameter">Parameter.</param>
        /// <param name="culture">Culture.</param>
        /// <returns>A boolean value that indicates a cell's living status.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush)
                return ((SolidColorBrush) value) == AliveColour;

            return false;
        }
    }
}
