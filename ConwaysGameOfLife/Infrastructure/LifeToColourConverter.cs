using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace ConwaysGameOfLife.Infrastructure
{
    public class LifeToColourConverter : IValueConverter
    {
        public static SolidColorBrush AliveCellColour = Brushes.Green;
        public static SolidColorBrush DeadCellColour = Brushes.White;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool alive = false;

            if (value is bool)
            {
                alive = (bool)value;
            }

            return alive ? AliveCellColour : DeadCellColour;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SolidColorBrush)
            {
                return ((SolidColorBrush)value) == AliveCellColour;
            }

            return false;
        }
    }
}
