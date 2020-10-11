using HIIR.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HIIR
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntervalsPage : ContentPage
    {
        public IntervalsPageViewModel ViewModel
        {
            get { return BindingContext as IntervalsPageViewModel; }
            set { BindingContext = value; }
        }

        public IntervalsPage()
        {

            InitializeComponent();

            
            BindingContext  = new IntervalsPageViewModel();
            timePickersListView.ItemsSource = (BindingContext as IntervalsPageViewModel).TimerPickersViewModel;
       //    timePicker.BindingContext = ViewModel;


        }

        private void rightButton_Clicked(object sender, EventArgs e)
        {

           // ViewModel.IsVisible = false;
          //  progressTime.IsVisible = true;
            //progressbar.IsEnabled = true;
           // progressbar.IsVisible = true;
            //ViewModel.OkCommand.Execute(timePicker.Time);
            //timePicker.IsOpen = false;

        }

        private void leftButton_Clicked(object sender, EventArgs e)
        {
            //ViewModel.IsVisible = true;
            //progressTime.IsVisible = false;
            //progressbar.IsVisible = false;

        }

        private void TimerPickerWheel_Changed(object sender, EventArgs e)
        {
                ViewModel.SetTime((sender as TimerPickerWheel).TimeValue);
        }

        private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.CurrentTimerSelectorIndex = e.ItemIndex; 
            ViewModel.ShowOrHideListModels(e.Item as WheeltimePickerModel);
        }
    }
       
}