using HIIR.Model;
using HIIR.ViewModel;
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
using Xamarin.Essentials;

//need to be biandable

namespace HIIR.ViewModel
{
    public class IntervalsPageViewModel : BaseViewModel
    {
        private CountDownTimer _timer;

        private WheelTimerPickerViewModel _oldViewModel;
        private bool isEditCklicked = false;
        private Tts _textToSpeach;
        private GeolocationService _locationService;
        private DateTime? lastNotifiedTime;
        private TimeSpan notificatioTimePeriod;
        private bool speedDecreasedDetected = false;
        private bool isInTicked = false;
        public ObservableCollection<WheelTimerPickerViewModel> TimerPickersViewModel { get; set; }
            = new ObservableCollection<WheelTimerPickerViewModel>();


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

        private double _progressAnimationLength = 10000; 
        public double ProgressAnimationLength
        {
            get { return _progressAnimationLength; }
            set
            {
                if (_progressAnimationLength == value)
                    return;
                _progressAnimationLength = value;
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

        private int _speed= 0;
        public int Speed
        {
            get { return _speed; }
            set
            {
                if (_speed == value)
                    return;
                _speed = value;
                OnPropertyChanged();
            }
        }

        public ICommand RightButtonClickedCommand { get; private set; }
        public ICommand EditButtonClickedCommand { get; private set; }
        public ICommand StartTimerCommand { get; private set; }
        public ICommand PauseTimerCommand { get; private set; }
        public ICommand CancelTimerCommand { get; private set; }
        public ICommand LeftButtonClickedCommand { get; private set; }



        public IntervalsPageViewModel(Tts TextToSpeech, GeolocationService geolocationService)
        {

            TimerPickersViewModel.Add(new WheelTimerPickerViewModel
            {
                Name = "Walking Duration",
                IsVisible = true,
                InEdit = true,
                Time = new TimeSpan(0, 1, 0) //tobind with default val
            });

            TimerPickersViewModel.Add(new WheelTimerPickerViewModel
            {
                Name = "Running Duration",
                IsVisible = false,
                InEdit = false,
                Time = new TimeSpan(0, 1, 0) //to bind with default val
            });

            _locationService = geolocationService;
            _locationService.SpeedErrorrAreaOFMonoticity = 3; // acceptble error area of 2 km/h
            _locationService.PartialSpeedGraphSize = 15;
            Task.Run(async () => { await _locationService.GetPermissions(); });
            _oldViewModel = TimerPickersViewModel[0];
            _textToSpeach = TextToSpeech;
            RightButtonClickedCommand = new Command(() => OnRightButtonClicked());
            EditButtonClickedCommand = new Command(() => OnEditButtonClicked());
            LeftButtonClickedCommand = new Command(() => OnLeftButtonClicked());
            StartTimerCommand = new Command(() => StartTimer());
            PauseTimerCommand = new Command(() => PauseTimer());
            CancelTimerCommand = new Command(() => CancelTimer());
            
        }

        private void OnEditButtonClicked()
        {
            _locationService.PauseTraking();

            _timer.Pause();
            isInTicked = false;
            RightButtonMode = RightButtomModes.Resume;
            isEditCklicked = true;

            IsActivated = false;
        }

        private void StartTimer()
        {

            _locationService.startGPSTraking();
            IsActivated = true;
            isInTicked = false;
            ProgressAnimationLength = 10;
            ProgressPracentage = 1;


            CountdownTimer = TimerPickersViewModel[(int)UserExercizeMode].Time;
            Time = CountdownTimer;
            _timer = new CountDownTimer(Time);
            _timer.Completed += OnCountdownTimerCompleted;
            _timer.Ticked += OnCountdownTimerTicked;
            _timer.Paused += OnCountdownTimerPaused;
            _textToSpeach.SpeakNow("Lets go!");
            _timer.Start();

        }


        private void PauseTimer()
        {
            //IsActivated = false;
            _textToSpeach.SpeakNow("pausing workout");

            _locationService.PauseTraking();
            isInTicked = false;
            _timer.Pause();
        }

        private void ResumeTimer()
        {
            _textToSpeach.SpeakNow("Resuming workout");
            isInTicked = false;
            _locationService.ResumeTraking();
            if (isEditCklicked)
            {

                isEditCklicked = false;
                if(Time != TimerPickersViewModel[(int)UserExercizeMode].Time)
                {
                    ProgressAnimationLength = 10;

                    ProgressPracentage = 1;

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
            _locationService.StopTraking();
            RightButtonMode = RightButtomModes.Start;
            UserExercizeMode = ExercizeMode.Walking; // resetting
            
            RoundsCounter = 0;
            IsActivated = false;
            _timer.Pause();
        }

        private void OnCountdownTimerCompleted()
        {

            ProgressAnimationLength = 10;
            isInTicked = false;

            ProgressPracentage = 0;

            CountdownTimer = TimeSpan.Zero;



            UserExercizeMode = Extensions.Next<ExercizeMode>(UserExercizeMode);
            ProgressPracentage = 1;

            Time = TimerPickersViewModel[(int)UserExercizeMode].Time;
            CountdownTimer = Time;

            //play some sound according the new ExercizeMode;

            _timer.InitWith(Time);
            if (UserExercizeMode == ExercizeMode.Walking)
            {
                RoundsCounter++;
                _textToSpeach.SpeakNow("time to walk");
            }
            else
            {
                _textToSpeach.SpeakNow("time to run");

            }

            _timer.Start();

            //start the timer with the current mode 


            //alert
        }

        private void OnCountdownTimerTicked()
        {

            string[] texts = { "harry up", "move your ass","le'ts go","go go go ", "a lil bit more please", "speed up mother fucker", "you can do it", "come on"};
            if (!isInTicked)
            {
                ProgressAnimationLength = 1000;
                isInTicked = true;
            }
            ProgressPracentage = (float)(_timer.TimeLeft.TotalSeconds / _timer.RequestedTime.TotalSeconds);
            Speed = (int)_locationService.Speed;

            CountdownTimer = _timer.TimeLeft;
            if(CountdownTimer.CompareTo(new TimeSpan(0,0,0,3,0)) == 0)
            {
                _textToSpeach.SpeakNow("3");
                Console.WriteLine("beep3");
            }
            else if (CountdownTimer.CompareTo(new TimeSpan(0,0,0,2,0)) == 0)
            {
                _textToSpeach.SpeakNow("2");

                Console.WriteLine("beep2");
            }
            else if (CountdownTimer.CompareTo(new TimeSpan(0,0,0,1,0)) == 0)
            {
                _textToSpeach.SpeakNow("1");

                Console.WriteLine("beep1");
            }

            if (UserExercizeMode == ExercizeMode.Running && CountdownTimer > TimeSpan.FromSeconds(5))
            {
                //SetNotificationTimePeriod();
                //TimerPickersViewModel[(int)UserExercizeMode].Time
                //lastNotifiedTime = DateTime.Now;
                //check speed 


                if (lastNotifiedTime == null)
                {
                    if (_locationService.IsSpeedMonotonicInc == false)
                    {
                        Console.WriteLine("notify runner to speed up");
                        
                        _textToSpeach.SpeakNow(texts[RandomeWithoitRepeatInRow(0, texts.Length)]);

                        lastNotifiedTime = DateTime.Now;
                    }

                }
                else
                {
                    //if (((DateTime)lastNotifiedTime).CompareTo(DateTime.Now.Add(TimeSpan.FromSeconds(_locationService.PartialSpeedGraphSize))) <= 0)
                    if(DateTime.Now.CompareTo(((DateTime)lastNotifiedTime).Add(new TimeSpan(0,0,_locationService.PartialSpeedGraphSize))) >= 0)
                    {

                        if (_locationService.IsSpeedMonotonicInc == false)
                        {
                            Console.WriteLine("notify runner to speed up");
                            _textToSpeach.SpeakNow(texts[RandomeWithoitRepeatInRow(0, texts.Length)]);
                            lastNotifiedTime = DateTime.Now;
                        }
                    }

                }

                // for user that run for an hour it posible to notify every 5 minutes but for user that run for 10 seconds 

            }




            //looking for speed and play some cheer sound if UserExercizeMode == Running;
        }
        private int lastVal;
        private int RandomeWithoitRepeatInRow(int min, int max)
        {
            Random rnd = new Random();

            var val = rnd.Next(min, max);
            while (lastVal == val)
            {
                val = rnd.Next(min, max);
            }
            lastVal = val;

            return val;
        }

        private void SetNotificationTimePeriod()
        {
            //notificatioTimePeriod
            var runningTime = CountdownTimer /*TimerPickersViewModel[(int)ExercizeMode.Running].Time*/;
            if(runningTime.TotalMinutes > 10)
            {
                notificatioTimePeriod = new TimeSpan(0, 1, 0);
            }
            else if(runningTime.TotalMinutes > 2)
            {
                notificatioTimePeriod = new TimeSpan(0, 0, 45);
            }
            else if(runningTime.TotalMinutes > 1)
            {
                notificatioTimePeriod = new TimeSpan(0, 0, 30);
            }
            else if(runningTime.TotalMinutes > 0 )
            {
                notificatioTimePeriod = new TimeSpan(0, 0, 20);
            }
            else if(runningTime.TotalSeconds > 30)
            {
                notificatioTimePeriod = new TimeSpan(0, 0, 10);
            }
            else if(runningTime.TotalSeconds > 10)
            {
                notificatioTimePeriod = new TimeSpan(0, 0, 5);
            }
            else
            {
                notificatioTimePeriod = new TimeSpan(0, 0, 2);
            }

        }
        private void OnCountdownTimerPaused()
        {

        }

        public void SetTime(TimeSpan Time)
        {
            TimerPickersViewModel[CurrentTimerSelectorIndex].Time = Time;
        }


        public void ShowOrHideListModels(WheelTimerPickerViewModel viewModel)
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

        private void UpdateViews(WheelTimerPickerViewModel view)
        {
            var index = TimerPickersViewModel.IndexOf(view);
            TimerPickersViewModel.Remove(view);
            TimerPickersViewModel.Insert(index, view);
        }

    }
}
