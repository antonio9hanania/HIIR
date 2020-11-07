using HIIR.ViewModel;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace HIIR
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RunnningEvents : ContentPage 
    {

        public RunningEventsViewModel ViewModel
        {
            get { return BindingContext as RunningEventsViewModel; }
            set { BindingContext = value; }
        }

        private enum MapStates
        {
            AllPins,
            PinSelection,
        }
        private MapStates? MapState 
        { 
            get;
            set; 
        }


        public bool IsNewEvent { get; set; } = false;

        public IList<Pin> AllPins 
        { 
            get;
            set; 
        }
        public Position SelectedPosition { get; set; }

        public RunnningEvents()
        {
            InitializeComponent();
            BindingContext = new RunningEventsViewModel();

            Task.Run(async () =>
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();
                map.MoveToRegion(Xamarin.Forms.GoogleMaps.MapSpan.FromCenterAndRadius(new Position(position.Latitude,position.Longitude),
                                                                                      Distance.FromKilometers(3)));

            });

            


        }

        public void map_MapClicked(object sender, MapClickedEventArgs e)
        {
            
            ViewModel.OnMapClicked(e.Point); 
        }

        private void map_PinClicked(object sender, PinClickedEventArgs e)
        {
            e.Handled = true;
            ViewModel.OnPinClicked(e.Pin);
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.ShowOrHideListModels(e.Item as PinViewModel, (map.Pins as ObservableCollection<Pin>));
            //map.SelectedPin = ViewModel.FocusedPin;
            if(ViewModel.FocusedPin != null)
                await map.AnimateCamera(CameraUpdateFactory.NewPosition(ViewModel.FocusedPin.Position));

        }




        //private async void startingPointSelection(object sender, MapClickedEventArgs e)
        //{
        //    var pin = new Pin();
        //    pin.Position = e.Point;
        //    pin.Label = "Starting point";

        //    var add = await Plugin.Geolocator.CrossGeolocator.Current.GetAddressesForPositionAsync(new Plugin.Geolocator.Abstractions.Position(e.Point.Latitude, e.Point.Longitude));

        //    map.Pins.Add(pin);
        //    SelectedPosition = e.Point;
        //    newPositionEntry.Text = "lat: "+e.Point.Latitude.ToString() + ", long: " +e.Point.Longitude.ToString();
        //}

        //private void newEventButton_Clicked(object sender, EventArgs e)
        //{
        //    IsNewEvent = true;
        //    (sender as Button).IsEnabled = false;
        //    AllPins = map.Pins;
        //    map.Pins.Clear();
        //    //map.MapClicked += startingPointSelection;
        //}

    }
}