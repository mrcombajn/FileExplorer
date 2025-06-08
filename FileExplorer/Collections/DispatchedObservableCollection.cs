using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Threading;

namespace FileExplorer.Collections
{
    public class DispatchedObservableCollection<T> : ObservableCollection<T>
    {
        public override event NotifyCollectionChangedEventHandler CollectionChanged;

        public DispatchedObservableCollection()
        {
        }

        public DispatchedObservableCollection(IEnumerable<T> collection) : base(new List<T>(collection ?? throw new ArgumentNullException(nameof(collection))))
        {
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var collectionChanged = CollectionChanged;
            if (collectionChanged != null)
            {
                Dispatcher dispatcher = (from NotifyCollectionChangedEventHandler handler
                in collectionChanged.GetInvocationList()
                                         let dispatcherObject = handler.Target as DispatcherObject
                                         where dispatcherObject != null
                                         select dispatcherObject.Dispatcher).FirstOrDefault();
                if (dispatcher != null && dispatcher.CheckAccess() == false)
                {
                    dispatcher.Invoke(DispatcherPriority.DataBind, (Action)(() => OnCollectionChanged(e)));
                }
                else
                {
                    foreach (NotifyCollectionChangedEventHandler handler in collectionChanged.GetInvocationList())
                        handler.Invoke(this, e);
                }
            }
        }
    }
}