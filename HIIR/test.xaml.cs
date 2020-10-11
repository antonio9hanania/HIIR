using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HIIR
{



    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class test : ContentPage
    {

        private WheeltimePickerModel _oldView;
        private int currnetTimerIndex = 0;

        public ObservableCollection<WheeltimePickerModel> ViewsModel { get; set; }
        = new ObservableCollection<WheeltimePickerModel>();





        public test()
        {
            InitializeComponent();
            //Views = new List<WheeltimePickerModel>();
            ViewsModel.Add(new WheeltimePickerModel
            {
                Name = "Running Duration",
                IsVisible = true,
                InEdit = true,
                Time = new TimeSpan(0, 1, 0)
            });
            ViewsModel.Add(new WheeltimePickerModel
            {
                Name = "Walking Duration",
                IsVisible = false,
                InEdit = false,
                Time = new TimeSpan(0, 1, 0)
            });
            //timePickersListView.ItemsSource = ViewsModel;
            _oldView = ViewsModel[0];
        }

        //public void ShowOrHidePoducts(WheeltimePickerModel view)
        //{
        //    if (_oldView == view)
        //    {
        //        // click twice on the same item will hide it
        //        view.IsVisible = !view.IsVisible;
        //        view.InEdit = !view.InEdit;
        //        UpdateViews(view);
        //    }
        //    else
        //    {
        //        if (_oldView != null)
        //        {
        //            // hide previous selected item
        //            _oldView.IsVisible = false;
        //            _oldView.InEdit = false;
        //            UpdateViews(_oldView);
        //        }
        //        // show selected item
        //        view.IsVisible = true;
        //        view.InEdit = true;
        //        UpdateViews(view);
        //    }

        //    _oldView = view;
        //}

        //private void UpdateViews(WheeltimePickerModel view)
        //{
        //    var index = ViewsModel.IndexOf(view);
        //    ViewsModel.Remove(view);
        //    ViewsModel.Insert(index, view);
        //}

        //private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    currnetTimerIndex = e.ItemIndex;
            
        //    var view = e.Item as WheeltimePickerModel;
        //    //var vm = BindingContext as MainViewModel;
        //    ShowOrHidePoducts(view);

        //}

        //private void TimerPickerWheel_Changed(object sender, EventArgs e)
        //{

        //    ViewsModel[currnetTimerIndex].Time = (sender as TimerPickerWheel).TimeValue;
        //    //if (currnetTimerIndex == 0)
        //    //    ViewsModel[currnetTimerIndex].Time = (sender as TimerPickerWheel).FirstTime;
        //    //if(currnetTimerIndex == 1)
        //    //    ViewsModel[currnetTimerIndex].Time = (sender as TimerPickerWheel).SecondTime;

        //}


    }

    public class WheeltimePickerModel
    {
        public string Name { get; set; }
        public bool IsVisible { get; set; }

        public TimeSpan Time 
        {
            get;
            set;
        }

        public bool InEdit { get; set; }
    }
}
