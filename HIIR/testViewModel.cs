using HIIR.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace HIIR
{
    public class testViewModel : BaseViewModel
    {
        private bool isNewEvent = false;


        private IEnumerable _pins;
        public IEnumerable Pins
        {
            get => _pins;
            set
            {
                if (value == _pins)
                    return;
                else
                {
                    _pins = value;
                    OnPropertyChanged();
                }
            }
        }


        public ObservableCollection<Pin> AllPins { get; set; } = new ObservableCollection<Pin>();
        public Pin NewEventPin { get; set; }
        

        public ICommand EventButtonClickedCommand { get; private set; }
        public ICommand ReturnButtonclickedCommand { get; private set; }

        public testViewModel()
        {
            EventButtonClickedCommand = new Command(() => OnEventButtonClicked());
            ReturnButtonclickedCommand = new Command(() => OnRetunButtonClicked());
            Pins = AllPins;
        }

        private void OnEventButtonClicked()
        {
            isNewEvent = true;
            Pins = new ObservableCollection<Pin>();
        }

        private void OnRetunButtonClicked()
        {
            isNewEvent = false;
            if (!AllPins.Contains(NewEventPin))
            {
                AllPins.Add(NewEventPin);
            };
            Pins = AllPins;
        }

        public void MapSelection (Position pos)
        {
            if (isNewEvent)
            {
                NewEventPin = new Pin
                {
                    Label = "Starting point",
                    Position = pos
                };
                
                (Pins as ObservableCollection<Pin>).Clear();
                (Pins as ObservableCollection<Pin>).Add(NewEventPin) ;


            }
        }



    }
}
