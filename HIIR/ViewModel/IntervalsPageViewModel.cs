using HIIR.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Collections.ObjectModel;

using System.Xml.Schema;
using Xamarin.Forms;

//need to be biandable

namespace HIIR.ViewModel
{
    public class IntervalsPageViewModel : BaseViewModel
    {
        private CountDownTimer _timer;

        private WheeltimePickerModel _oldViewModel;
        private bool isEditCklicked = false;

        public ObservableCollection<WheeltimePickerModel> TimerPickersViewModel { get; set; }
            = new ObservableCollection<WheeltimePickerModel>();


        private ExercizeMode _userExercizeMode = ExercizeMode.Walking;
        public ExercizeMode UserExercizeMode 
        {
            get { return _userExercizeMode; }
            set
            {
                if (_userExercizeMode == value)
                    return;
                _userExercizeMode = value;
                OnPropertyChanged();
            } 
        }

        private int _currentTimerSelectorIndex = 0;
        public int CurrentTimerSelectorIndex
        {
            get { return _currentTimerSelectorIndex; }
            set
            {
                if (_currentTimerSelectorIndex == value)
                    return;

                _currentTimerSelectorIndex = value;
                OnPropertyChanged();
            }
        }

        private int _roundCounter = 0;
        public int RoundsCounter {
            get { return _roundCounter; }
            set
            {
                if (value == _roundCounter)
                    return;
                _roundCounter = value;
                OnPropertyChanged();
            }
        }

        public enum RightButtomModes
        {
            Start ,
            Pause ,
            Resume,
        }

        private Color[] _rightButtomColorsModes =
        {
            Color.White,
            Color.DarkOrange,
            Color.DarkGreen
        };

        public enum ExercizeMode
        {
            Walking = 0,
            Running,
        }


        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                _time = value;
            }
        }

        private bool _isActivated = false;
        public bool IsActivated
        {
            get { return _isActivated; }
            set
            {
                if (_isActivated == value)

                    return;
                _isActivated = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _countdownTimer = TimeSpan.Zero;
        public TimeSpan CountdownTimer
        {
            get { return _countdownTimer; }
            set
            {
                if (_countdownTimer == value)
                    return;
                _countdownTimer = value;
                OnPropertyChanged();
            }
        }

        private float _progressPracentage = 1;
        public float ProgressPracentage
        {
            get { return _progressPracentage; }
            set
            {
                if (_progressPracentage == value)
                    return;
                _progressPracentage = value;
                OnPropertyChanged();
            }
        }

        private RightButtomModes _rightButtonMode = RightButtomModes.Start;
        public RightButtomModes RightButtonMode
        {
            get { return _rightButtonMode; }
            set
            {
                if (_rightButtonMode == value)
                    return;
                _rightButtonMode = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ButtomModeText));
                OnPropertyChanged(nameof(ButtomModeColor));
            }
        }


        public string ButtomModeText
        {
            get
            {
                return Enum.GetName(typeof(RightButtomModes), RightButtonMode);
            }
        }

        public Color ButtomModeColor
        {
            get
            {
                return _rightButtomColorsModes[(int) RightButtonMode];
            }
        }


        public ICommand RightButtonClickedCommand { get; private set; }
        public ICommand EditButtonClickedCommand { get; private set; }
        public ICommand StartTimerCommand { get; private set; }
        public ICommand PauseTimerCommand { get; private set; }
        public ICommand CancelTimerCommand { get; private set; }
        public ICommand LeftButtonClickedCommand { get; private set; }



        public IntervalsPageViewModel()
        {


            TimerPickersViewModel.Add(new WheeltimePickerModel
            {
                Name = "Walking Duration",
                IsVisible = true,
                InEdit = true,
                Time = new TimeSpan(0, 1, 0)
            });

            TimerPickersViewModel.Add(new WheeltimePickerModel
            {
                Name = "Running Duration",
                IsVisible = false,
                InEdit = false,
                Time = new TimeSpan(0, 1, 0)
            });


            _oldViewModel = TimerPickersViewModel[0];
            RightButtonClickedCommand = new Command(() => OnRightButtonClicked());
            EditButtonClickedCommand = new Command(() => OnEditButtonClicked());
            LeftButtonClickedCommand = new Command(() => OnLeftButtonClicked());
            StartTimerCommand = new Command(() => StartTimer());
            PauseTimerCommand = new Command(() => PauseTimer());
            CancelTimerCommand = new Command(() => CancelTimer());
            
        }

        private void OnEditButtonClicked()
        {
            _timer.Pause();
            RightButtonMode = RightButtomModes.Resume;
            isEditCklicked = true;

            IsActivated = false;
        }

        private void StartTimer()
        {
            IsActivated = true;
            
            ProgressPracentage = 1;
            CountdownTimer = TimerPickersViewModel[(int)UserExercizeMode].Time;
            Time = CountdownTimer;
            _timer = new CountDownTimer(Time);
            _timer.Completed += OnCountdownTimerCompleted;
            _timer.Ticked += OnCountdownTimerTicked;
            _timer.Paused += OnCountdownTimerPaused;
            _timer.Start();

        }


        private void PauseTimer()
        {
            //IsActivated = false;


            _timer.Pause();
        }

        private void ResumeTimer()
        {

            if (isEditCklicked)
            {
                ProgressPracentage = 1;

                isEditCklicked = false;
                Time = TimerPickersViewModel[(int)UserExercizeMode].Time;
                CountdownTimer = Time;

                _timer.InitWith(Time);
                IsActivated = true;

                _timer.Start();
            }
            else
            {
                IsActivated = true;

                _timer.Resume();
            }

        }

        private void CancelTimer()
        {

        }
        private void OnRightButtonClicked()
        {
            switch (RightButtonMode)
            {
                case RightButtomModes.Start:
                    RightButtonMode++;
                    StartTimer();
                    break;
                case RightButtomModes.Pause:
                    RightButtonMode++;
                    PauseTimer();
                    break;
                case RightButtomModes.Resume:
                    ResumeTimer();
                    RightButtonMode--;
                    break;
                default: //not suposed to happend
                    break;
            }
        }

        private void OnLeftButtonClicked()
        {
            RightButtonMode = RightButtomModes.Start;
            UserExercizeMode = ExercizeMode.Walking; // resetting
            RoundsCounter = 0;
            IsActivated = false;
        }

        private void OnCountdownTimerCompleted()
        {
            ProgressPracentage = 0;

            CountdownTimer = TimeSpan.Zero;



            UserExercizeMode = Extensions.Next<ExercizeMode>(UserExercizeMode);

            ProgressPracentage = 1;

            Time = TimerPickersViewModel[(int)UserExercizeMode].Time;
            CountdownTimer = Time;

            //playsome sounde according the new ExercizeMode;

            _timer.InitWith(Time);
            if (UserExercizeMode == ExercizeMode.Walking)
                RoundsCounter++;

            _timer.Start();

            //start the timer with the current mode 


            //alert
        }
        private void OnCountdownTimerTicked()
        {
            ProgressPracentage = (float)(_timer.TimeLeft.TotalSeconds / _timer.RequestedTime.TotalSeconds);

            CountdownTimer = _timer.TimeLeft;
            if(CountdownTimer  == TimeSpan.FromSeconds(3))
            {
                // play 3, 2, 1 beep;
                Console.WriteLine("3, 2, 1 occured");
            }

            //looking for speed and play some cheer sound if UserExercizeMode == Running;
        }

        private void OnCountdownTimerPaused()
        {

        }

        public void SetTime(TimeSpan Time)
        {
            TimerPickersViewModel[(int)UserExercizeMode].Time = Time;
        }


        public void ShowOrHideListModels(WheeltimePickerModel viewModel)
        {
            if (_oldViewModel == viewModel)
            {
                // click twice on the same item will hide it
                viewModel.IsVisible = !viewModel.IsVisible;
                viewModel.InEdit = !viewModel.InEdit;
                UpdateViews(viewModel);
            }
            else
            {
                if (_oldViewModel != null)
                {
                    // hide previous selected item
                    _oldViewModel.IsVisible = false;
                    _oldViewModel.InEdit = false;
                    UpdateViews(_oldViewModel);
                }
                // show selected item
                viewModel.IsVisible = true;
                viewModel.InEdit = true;
                UpdateViews(viewModel);
            }

            _oldViewModel = viewModel;
        }

        private void UpdateViews(WheeltimePickerModel view)
        {
            var index = TimerPickersViewModel.IndexOf(view);
            TimerPickersViewModel.Remove(view);
            TimerPickersViewModel.Insert(index, view);
        }

    }
}
