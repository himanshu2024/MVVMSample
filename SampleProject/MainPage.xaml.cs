using SampleProject.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace SampleProject
{
    [DataContract]
    internal class Person
    {
        [DataMember]
        internal string name;

        [DataMember(Name = "Age", EmitDefaultValue = false)]
        internal int? age;


    }


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //private NavigationHelper navigationHelper;
        //private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public MainPage()
        {
            this.InitializeComponent();

            Person p = new Person();
            p.name = "John";
            //p.age = 42;



            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
            ser.WriteObject(stream1, p);

            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            Debug.WriteLine("JSON form of Person object: ");
            Debug.WriteLine(sr.ReadToEnd());

            //this.NavigationCacheMode = NavigationCacheMode.Required;

            //GenerateControls();

            DateTime? a = null;
            if (!a.HasValue)
            {
                a = DateTime.Now;
                if (a.HasValue)
                {
                    Debug.WriteLine(a.Value);
                }
            }

            int? ab = null;
            if (!ab.HasValue)
            {
                ab = 1;
                Debug.WriteLine(ab.Value);
            }

        }

        public void GenerateControls()
        {
            //Button btnClickMe = new Button();
            //btnClickMe.Content = "Click Me";
            //btnClickMe.Name = "btnClickMe";
            //btnClickMe.Click += new RoutedEventHandler(this.CallMeClick);
            //ContentGrid.Children.Add(btnClickMe);
            //TextBox txtNumber = new TextBox();
            //txtNumber.Name = "txtNumber";
            //txtNumber.Text = "1776";
            //ContentGrid.Children.Add(txtNumber);
            //ContentGrid.RegisterName(txtNumber.Name, txtNumber);
        }
        protected async void CallMeClick(object sender, RoutedEventArgs e)
        {
            TextBox txtNumber = (TextBox)this.ContentGrid.FindName("txtNumber");
            string message = string.Format("The number is {0}", txtNumber.Text);
            MessageDialog dialog = new MessageDialog(message);
            await dialog.ShowAsync();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(FirstPage));
        }
    }
}
