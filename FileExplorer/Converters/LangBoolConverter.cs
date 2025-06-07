#region Using Statements

using System.Globalization;
using System.Windows.Data;

#endregion

namespace FileExplorer.Converters
{
    public class LangBoolConverter : IValueConverter
    {
        #region Public Methods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == (string)parameter)
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
                return (string)parameter;
            return null;
        }

        #endregion
    }
}
