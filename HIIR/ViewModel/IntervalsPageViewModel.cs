using HIIR.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Xml.Schema;
using Xamarin.Forms;

//need to be biandable

namespace HIIR.ViewModel
{
    public class IntervalsPageViewModel : BaseViewModel
    {
        private CountDownTimer _timer;

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


        private TimeSpan _time;
        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                _time = value;
            }
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible == value)

                    return;
                _isVisible = value;
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
        public ICommand StartTimerCommand { get; private set; }
        public ICommand PauseTimerCommand { get; private set; }
        public ICommand CancelTimerCommand { get; private set; }



        public IntervalsPageViewModel()
        {
            RightButtonClickedCommand = new Command(() => OnRightButtonClicked());
            StartTimerCommand = new Command(() => StartTimer());
            PauseTimerCommand = new Command(() => PauseTimer());
            CancelTimerCommand = new Command(() => CancelTimer());
        }

        private void StartTimer()
        {
            ProgressPracentage = 1;
            CountdownTimer = Time;
            _timer = new CountDownTimer(Time);
            _timer.Completed += OnCountdownTimerCompleted;
            _timer.Ticked += OnCountdownTimerTicked;
            _timer.Paused += OnCountdownTimerPaused;
            _timer.Start();

        }


        private void PauseTimer()
        {
            _timer.Pause();
        }
        private void ResumeTimer()
        {
            _timer.Resume();
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

        private void OnCountdownTimerCompleted()
        {
            ProgressPracentage = 0;

            CountdownTimer = TimeSpan.Zero;


            //alert
        }

        private void OnCountdownTimerTicked()
        {
            ProgressPracentage = (float)(_timer.TimeLeft.TotalSeconds / _timer.RequestedTime.TotalSeconds);

            CountdownTimer = _timer.TimeLeft;

        }

        private void OnCountdownTimerPaused()
        {

        }

    }
}
