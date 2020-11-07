using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace HIIR.ViewModel
{
    public class RunningEventsViewModel : BaseViewModel
    {

        private PinViewModel _oldViewModel;


        private IEnumerable _currMapPins;
        public IEnumerable CurrMapPins
        {
            get => _currMapPins;
            set
            {
                if (value == _currMapPins)
                    return;
                else
                {
                    _currMapPins = value;
                    OnPropertyChanged();
                }
            }
        }


        private PinViewModel _newEventPin = null;
        public PinViewModel NewEventPin
        {
            get { return _newEventPin; }
            set
            {
                if (value == _newEventPin)
                    return;
                else
                {
                    _newEventPin = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SelectedPositionCoords));
                }
            }
        }

        public string SelectedPositionCoords 
        {
            get
            {
                if (NewEventPin != null)
                    return string.Format("Lat: {0}.. ,Long: {1}..", NewEventPin.Pin.Position.Latitude.ToString("F3"), NewEventPin.Pin.Position.Longitude.ToString("F3"));
                else
                    return "Select a starting point on the map";
            }
        }
        private string _newEventComment = String.Empty;
        public string NewEventComment 
        {
            get { return _newEventComment; }
            set
            {
                if (value == _newEventComment)
                    return;
                else
                {
                    _newEventComment = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<PinViewModel> AllPins { get; set; } = new ObservableCollection<PinViewModel>();

        private enum MapStates
        {
            AllPins,
            PinSelection,
        }

        private MapStates _mapState = MapStates.AllPins;
        private MapStates MapState
        {
            get { return _mapState; }
            set
            {
                if (value == _mapState)
                    return;
                else
                {
                    _mapState = value;
                    OnPropertyChanged();
                }

            }
        }

        private bool _isNewEvent = false;
        public bool IsNewEvent 
        {
            get { return _isNewEvent; }
            set
            {
                if (value == _isNewEvent)
                    return;
                else
                {
                    _isNewEvent = value;
                    OnPropertyChanged();
                }

            }
        }

        private bool _isNewEventFormFiiled = false;
        public bool IsNewEventFormFiiled 
        {
            get => _isNewEventFormFiiled;
            set
            {
                if (_isNewEventFormFiiled == value)
                    return;
                else
                {
                    _isNewEventFormFiiled = value;
                    OnPropertyChanged();
                }
            }
        }

        private Pin _focusedPin = null;
        public Pin FocusedPin
        {
            get => _focusedPin;
            set
            {
                if (_focusedPin == value)
                    return;
                else
                {
                    _focusedPin = value;
                    OnPropertyChanged();

                }
            }
        }



        public int MyProperty { get; set; }

        public ICommand NewEventButtonClickedCommand { get; private set; }
        public ICommand NewEventSavedButtonClickedCommand { get; private set; }


        public RunningEventsViewModel()
        {
            NewEventButtonClickedCommand = new Command(() => OnNewEventButtonClicked());
            NewEventSavedButtonClickedCommand = new Command(() => OnNewEventSavedButtonClicked(), ()=> false);
            AllPins.CollectionChanged += AllPins_CollectionChanged;
        }

        private void AllPins_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("All Pins CHanged");
        }

        public void OnMapClicked(Position pos)
        {
            if(MapState == MapStates.PinSelection)
            {
                NewEventPin = new PinViewModel
                {
                    Pin = new Pin
                    {
                        Label = "Starting point",
                        Position = pos,
                        IsDraggable = false
                    }

                };
                IsNewEventFormFiiled = true;
                (CurrMapPins as ObservableCollection<PinViewModel>).Clear();
                (CurrMapPins as ObservableCollection<PinViewModel>).Add(NewEventPin);
            }
        }

        public void OnNewEventButtonClicked()
        {
            MapState = MapStates.PinSelection;
            CurrMapPins = new ObservableCollection<PinViewModel>();
            IsNewEvent = true;
        }

        public void OnNewEventSavedButtonClicked()
        {
            IsNewEvent = false;

            if(NewEventComment != String.Empty)
                NewEventPin.Pin.Label += ": " + NewEventComment;
            NewEventPin.Note = NewEventComment;

            NewEventComment = String.Empty;
            NewEventPin.IsValid = true;
            
            if (!AllPins.Contains(NewEventPin))
                AllPins.Add(NewEventPin);
            CurrMapPins = AllPins;
            MapState = MapStates.AllPins;
            NewEventPin = null;
            IsNewEventFormFiiled = false;

        }

        public void OnPinClicked(Pin selectedPin)
        {
            FocusedPin = selectedPin;
        }




        ///////////////////////////////////////////
        public void ShowOrHideListModels(PinViewModel viewModel, ObservableCollection<Pin> mapPins)
        {
            if (_oldViewModel == viewModel)
            {
                // click twice on the same item will hide it
                viewModel.IsExpanded = !viewModel.IsExpanded;
                UpdateViews(viewModel);
                if(viewModel.IsExpanded == false)
                    FocusedPin = null;
                else
                    FocusedPin = mapPins.Where(p => (p.Position == viewModel.Pin.Position)).FirstOrDefault(); ;


            }
            else
            {
                if (_oldViewModel != null)
                {
                    // hide previous selected item
                    _oldViewModel.IsExpanded = false;
                    UpdateViews(_oldViewModel);
                }
                // show selected item
                viewModel.IsExpanded = true;

                UpdateViews(viewModel);
                FocusedPin = mapPins.Where(p => (p.Position == viewModel.Pin.Position)).FirstOrDefault(); ;

            }

            _oldViewModel = viewModel;
        }

        private void UpdateViews(PinViewModel view)
        {
            var index = (CurrMapPins as ObservableCollection<PinViewModel>).IndexOf(view);
            (CurrMapPins as ObservableCollection<PinViewModel>).Remove(view);
            (CurrMapPins as ObservableCollection<PinViewModel>).Insert(index, view);
            

        }
        //////////////////////////////////////////////////////
    }
    public class PinViewModel
    {
        public Pin Pin { get; set; }
        public string Publisher { get; set; } = "Name";
        public string Note { get; set; }
        public bool IsExpanded { get; set; } = false;

        public bool IsValid { get; set; } = false;


    }
}
