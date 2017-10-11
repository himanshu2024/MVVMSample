using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SampleProject
{

    public class CyclingFlipView : FlipView
    {
        public async Task Cycle()
        {
            if (this.ItemsSource != null)
            {
                var list = (IList)this.ItemsSource;

                if (list.Count == 0)
                {
                    return;
                }

                SelectionChangedEventHandler handler = null;
                var tcs = new TaskCompletionSource<bool>();
                handler = (s, e) =>
                {
                    tcs.SetResult(true);
                    this.SelectionChanged -= handler;
                };
                this.SelectionChanged += handler;
                this.SelectedIndex = (this.SelectedIndex + 1) % list.Count;
                await tcs.Task;
                //await Task.Delay(500);
                var i = this.SelectedIndex;
                this.SelectedItem = null;

                var item = list[0];
                list.RemoveAt(0);
                list.Add(item);
                this.SelectedIndex = i - 1;
            }
            else if (this.Items != null)
            {
                if (this.Items.Count == 0)
                {
                    return;
                }

                SelectionChangedEventHandler handler = null;
                var tcs = new TaskCompletionSource<bool>();
                handler = (s, e) =>
                {
                    tcs.SetResult(true);
                    this.SelectionChanged -= handler;
                };
                this.SelectionChanged += handler;
                this.SelectedIndex = (this.SelectedIndex + 1) % this.Items.Count;
                await tcs.Task;
                //await Task.Delay(500);
                var i = this.SelectedIndex;
                this.SelectedItem = null;

                var item = this.Items[0];
                this.Items.RemoveAt(0);
                this.Items.Add(item);
                this.SelectedIndex = i - 1;
            }
        }

        public async Task AutoCycle()
        {
            while (true)
            {
                this.Cycle();
                //await Task.Delay(1000);
            }
        }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage3 : Page
    {
        

        public BlankPage3()
        {
            this.InitializeComponent();

            InitializeValues();
           
        }

        private async void InitializeValues()
        {
            List<string> obj = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                obj.Add("Item at " + i);
            }

            var fv = new CyclingFlipView();
            fv.ItemsSource = obj;
            fv.ItemTemplate = (DataTemplate)this.Resources["ItemTemplate"];
            LayoutRoot.Children.Add(fv);
            //await fv.AutoCycle();
        }

        

        

        //async void LayoutRoot_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        //{
        //    var velocity = e.Velocities;
        //    if (velocity.Linear.X < 0 && velocity.Linear.Y > 0)
        //    {
        //        MessageDialog messageDialog = new MessageDialog("Right");
        //        await messageDialog.ShowAsync();
        //    }
        //    else if (velocity.Linear.X > 0 && velocity.Linear.Y < 0)
        //    {
        //        MessageDialog messageDialog = new MessageDialog("Left");
        //        await messageDialog.ShowAsync();
        //    }
        //}

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
