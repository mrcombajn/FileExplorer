using System.Globalization;
using System.Windows.Data;

namespace FileExplorer
{
    public class CultureResources
    {
        private static ObjectDataProvider _provider;

        public Strings GetStringsInstance()
        {
            return new Strings();
        }

        public static ObjectDataProvider ResourceProvider
        {
            get
            {
                if (_provider == null)
                    _provider = (ObjectDataProvider)System.Windows.Application.Current.FindResource("Strings");

                return _provider;
            }
        }

        public static void ChangeCulture(CultureInfo culture)
        {
            ResourceProvider.Refresh();
        }

    }
}
