using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections;
using System.Threading;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using HIIR.Model;

//using Xamarin.Forms.Maps;
//using Xamarin.Essentials;

using Xamarin.Forms.GoogleMaps;
namespace HIIR
{



    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class test : ContentPage
    {
        //private ObservableCollection<Pin> _pinCollection = new ObservableCollection<Pin>();
        //public ObservableCollection<Pin> PinCollection 
        //{
        //    get => _pinCollection;
        //    set { 
        //        _pinCollection = value; 
        //        OnPropertyChanged(nameof(PinCollection));
        //    } 
        //}


        ////readonly ObservableCollection<Pin> _pins;

        ////public IEnumerable Pins => _pins;

        //private ObservableCollection<Pin> _pins = new ObservableCollection<Pin>();
        //public ObservableCollection<Pin> Pins
        //{
        //    get => _pinCollection;
        //    set
        //    {
        //        _pinCollection = value;
        //        OnPropertyChanged(nameof(Pins));
        //    }
        //}

        public testViewModel ViewModel
        {
            get { return BindingContext as testViewModel; }
            set { BindingContext = value; }
        }
        //public ObservableCollection<Pin> SecondaryPins { get; set; } = new ObservableCollection<Pin>();
        public test()
        {
            //_pins = new ObservableCollection<Pin>();

            InitializeComponent();
            BindingContext = new testViewModel();
            //bindableMap.MapPins= PinCollection;
            //map.ItemsSource = Pins;
            



        }

        private void map_MapClicked(object sender, MapClickedEventArgs e)
        {
            ViewModel.MapSelection(e.Point);
            //Pins.Add(new Pin
            //{
            //    Label = (string)"Starting point",
            //    Position = e.Point
            //});
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    map.ItemsSource = SecondaryPins;
        //}

        //private void Button_Clicked_1(object sender, EventArgs e)
        //{
        //    map.ItemsSource = Pins;
        //}



        //private void BindableMap_MapClicked(object sender, MapClickedEventArgs e)
        //{

        //    Pins.Add(new Pin
        //    {
        //        Label = (string)"Starting point",
        //        Position = e.Position
        //    }) ;

        //    //    Console.WriteLine(map.Behaviors);
        //}

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    map.ItemsSource = SecondaryPins;
        //}

        //private void Button_Clicked_1(object sender, EventArgs e)
        //{
        //    map.ItemsSource = Pins;
        //}

    }
}
