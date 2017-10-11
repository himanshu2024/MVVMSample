using SampleProject.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace SampleProject
{
    public class EditButton : Button
    {
        public EditButton()
            : base()
        {
            //CheckedInCommand = new EditButton();
            TappedCommand = new DelegateCommand<object>(hi);
        }

        public DelegateCommand<object> TappedCommand { get; set; }

        private async void hi(object obj)
        {
            if (obj is string)
            {
                //Debug.WriteLine(obj.ToString());
                MessageDialog dialog = new MessageDialog(obj.ToString());
                await dialog.ShowAsync();
            }
        }

        //public ICommand CheckedInCommand { get; set; }


        //public bool CanExecute(object parameter)
        //{
        //    return false;
        //}

        public event EventHandler CanExecuteChanged;

        //void ICommand.Execute(object parameter)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
