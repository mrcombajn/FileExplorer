#region Using Statements

using System.ComponentModel;
using System.Runtime.CompilerServices;

#endregion

namespace FileExplorer.Components
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Fields and Constants

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Protected Methods

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
