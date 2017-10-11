using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace SampleProject
{

    public class MyItem
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }

        public double Width
        {
            get
            {
                return (double)Application.Current.Resources["GridViewItemPortrait"];
                //return 250;
            }
        }

        public MyItem(string title, string subtitle, string description)
        {
            Title = title;
            Subtitle = subtitle;
            Description = description;
        }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        List<MyItem> myItems = new List<MyItem>();

        public BlankPage1()
        {
            this.InitializeComponent();

            CreateTestItems();

            myGridView.ContainerContentChanging += MyGridView_ContainerContentChanging;

        }

        // Create a simulated list of 150,000 items.
        void CreateTestItems()
        {
            for (int i = 1; i < 150000; i++)
            {
                myItems.Add(new MyItem(
                    "Title:" + i.ToString(), // Title.
                    "Sub:" + i.ToString(), // Subtitle.
                    "Desc:" + i.ToString())); // Description.                            
            }
        }

        // Connect the grid view to the list of items.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            myGridView.ItemsSource = myItems;
        }


        // Display each item incrementally to improve performance.
        private void MyGridView_ContainerContentChanging(
                ListViewBase sender,
                ContainerContentChangingEventArgs args)
        {
            args.Handled = true;

            if (args.Phase != 0)
            {
                throw new Exception("Not in phase 0.");
            }

            // First, show the items' placeholders.
            StackPanel templateRoot =
                (StackPanel)args.ItemContainer.ContentTemplateRoot;
            Rectangle placeholderRectangle =
                (Rectangle)templateRoot.FindName("placeholderRectangle");
            TextBlock titleTextBlock =
                (TextBlock)templateRoot.FindName("titleTextBlock");
            TextBlock subtitleTextBlock =
                (TextBlock)templateRoot.FindName("subtitleTextBlock");
            TextBlock descriptionTextBlock =
                (TextBlock)templateRoot.FindName("descriptionTextBlock");

            // Make the placeholder rectangle opaque.
            placeholderRectangle.Opacity = 1;

            // Make everything else invisible.
            titleTextBlock.Opacity = 0;
            subtitleTextBlock.Opacity = 0;
            descriptionTextBlock.Opacity = 0;

            // Show the items' titles in the next phase.
            args.RegisterUpdateCallback(ShowTitle);
        }

        // Show the items' titles.
        private void ShowTitle(
                ListViewBase sender,
                ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 1)
            {
                throw new Exception("Not in phase 1.");
            }

            // Next, show the items' titles. Keep everything else invisible.
            MyItem myItem = (MyItem)args.Item;
            SelectorItem itemContainer =
                (SelectorItem)args.ItemContainer;
            StackPanel templateRoot =
                (StackPanel)itemContainer.ContentTemplateRoot;
            TextBlock titleTextBlock =
                (TextBlock)templateRoot.FindName("titleTextBlock");

            titleTextBlock.Text = myItem.Title;
            titleTextBlock.Opacity = 1;

            // Show the items' subtitles in the next phase.
            args.RegisterUpdateCallback(ShowSubtitle);
        }

        // Show the items' subtitles.
        private void ShowSubtitle(
                ListViewBase sender,
                ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 2)
            {
                throw new Exception("Not in phase 2.");
            }

            // Next, show the items' subtitles. Keep everything else invisible.
            MyItem myItem = (MyItem)args.Item;
            SelectorItem itemContainer = (SelectorItem)args.ItemContainer;

            StackPanel templateRoot =
                (StackPanel)itemContainer.ContentTemplateRoot;
            TextBlock subtitleTextBlock =
                (TextBlock)templateRoot.FindName("subtitleTextBlock");

            subtitleTextBlock.Text = myItem.Subtitle;
            subtitleTextBlock.Opacity = 1;

            // Show the items' descriptions in the next phase.
            args.RegisterUpdateCallback(ShowDescription);
        }

        // Show the items' descriptions.
        private void ShowDescription(
                ListViewBase sender,
                ContainerContentChangingEventArgs args)
        {
            if (args.Phase != 3)
            {
                throw new Exception("Not in phase 3.");
            }

            // Finally, show the items' descriptions. 
            MyItem myItem = (MyItem)args.Item;
            SelectorItem itemContainer = (SelectorItem)args.ItemContainer;

            StackPanel templateRoot =
                (StackPanel)itemContainer.ContentTemplateRoot;
            Rectangle placeholderRectangle =
                (Rectangle)templateRoot.FindName("placeholderRectangle");
            TextBlock descriptionTextBlock =
                (TextBlock)templateRoot.FindName("descriptionTextBlock");

            descriptionTextBlock.Text = myItem.Description;
            descriptionTextBlock.Opacity = 1;

            // Make the placeholder rectangle invisible.
            placeholderRectangle.Opacity = 0;
        }




    }
}
