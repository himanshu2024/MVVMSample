using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage2 : Page
    {

        PointerPoint firstPoint = null;
        ScrollViewer listScrollviewer = null;


        public BlankPage2()
        {
            this.InitializeComponent();

            List<string> objList = new List<string>();

            for (int i = 0; i < 2; i++)
            {
                objList.Add("Item at " + i);
            }

            myList.ItemsSource = objList;
            //myList.PointerEntered += myList_PointerEntered;
            //myList.PointerMoved += myList_PointerMoved;

        }


        public static ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer) return depObj as ScrollViewer;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = GetScrollViewer(child);
                if (result != null) return result;
            }
            return null;
        }

        private ScrollViewer DisableScrolling(DependencyObject depObj)
        {
            ScrollViewer foundOne = GetScrollViewer(depObj);
            if (foundOne != null) foundOne.VerticalScrollMode = ScrollMode.Disabled;
            return foundOne;
        }

        private void myList_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            firstPoint = e.GetCurrentPoint(myList);
            if (listScrollviewer == null) listScrollviewer = DisableScrolling(myList);
        }

        private void myList_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (listScrollviewer != null)
            {
                PointerPoint secondPoint = e.GetCurrentPoint(myList);
                double verticalDifference = secondPoint.Position.Y - firstPoint.Position.Y;
                listScrollviewer.ChangeView(null, listScrollviewer.VerticalOffset - verticalDifference, null);
            }
            // some other code you need
        }

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
