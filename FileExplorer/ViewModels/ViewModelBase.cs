#region Using Statements

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

#endregion

namespace FileExplorer.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        #region Events

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
