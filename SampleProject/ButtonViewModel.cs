using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace SampleProject
{
    public class ButtonViewModel
    {
        public ButtonViewModel()
        {
            EditTappedCommand = new DelegateCommand<object>(EditDelegate);
            DeleteTappedCommand = new DelegateCommand<object>(DeleteEditDelegate);
            Collection.CollectionChanged += Collection_CollectionChanged;
        }

        void Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void DeleteEditDelegate(object obj)
        {
            if (obj is string)
            {
                string str = (string)obj;
                Collection.Remove(str);
            }
        }

        public ObservableCollection<string> _Collection;
        public ObservableCollection<string> Collection
        {
            get { return _Collection; }
            set { _Collection = value; }
        }


        public DelegateCommand<object> EditTappedCommand { get; set; }
        public DelegateCommand<object> DeleteTappedCommand { get; set; }

        private async void EditDelegate(object obj)
        {
            if (obj is string)
            {
                //Debug.WriteLine(obj.ToString());
                MessageDialog dialog = new MessageDialog(obj.ToString());
                await dialog.ShowAsync();
            }
        }

        void callapi()
        {
            Collection = new ObservableCollection<string>();
        }

    }
}
