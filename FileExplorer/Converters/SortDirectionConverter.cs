#region Using Statements

using System.Globalization;
using System.Windows.Data;
using FileExplorer.Enums;
using Binding = System.Windows.Data.Binding;

#endregion

namespace FileExplorer.Converters
{
    class SortDirectionConverter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString().Equals((string)parameter, StringComparison.InvariantCultureIgnoreCase) == true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked && parameter is string name)
            {
                return Enum.GetValues(typeof(SortDirection))
                    .Cast<SortDirection>()
                    .FirstOrDefault(e => e.ToString()
                    .Equals(name, StringComparison.InvariantCultureIgnoreCase));
            }

            return Binding.DoNothing;
        }

        #endregion
    }
}
