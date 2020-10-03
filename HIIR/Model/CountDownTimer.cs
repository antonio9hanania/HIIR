using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HIIR.Model
{
    public class CountDownTimer : BindableObject
    {
        private TimeSpan _timeLeft ;
        private bool isRunning = false ;
        private bool isPause = false;


        public event Action Completed;
        public event Action Ticked;
        public event Action Paused;


        public TimeSpan RequestedTime { get;private set; }
        public CountDownTimer(TimeSpan TotalTime)
        {
            RequestedTime = TotalTime;
            TimeLeft = RequestedTime;
        }

        public TimeSpan TimeLeft
        {
            get { return _timeLeft; }

            private set
            {
                _timeLeft = value;
                OnPropertyChanged();
            }
        }

        public void Start()
        {
            TimeSpan endTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second).Add(TimeLeft);

            if (!isRunning) //if it is already running ther's no meaning for starting again
            {
                Device.StartTimer(TimeSpan.FromMilliseconds(SpreedControl()), () =>
                {
                    isRunning = true;

                    TimeSpan nowTime = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                    TimeLeft = endTime - nowTime;

                    var ticked = TimeLeft.TotalSeconds > 0;

                    if (isPause)
                    {
                        Paused?.Invoke();
                        ticked = false;
                        isRunning = false;

                    }
                    else if (ticked)
                    {
                        Ticked?.Invoke();
                    }
                    else
                    {
                        Completed?.Invoke();
                    }

                    return ticked;
                });
            }
        }

        public void Pause()
        {
            isPause = true;
        }

        public void Resume()
        {
            isPause = false;
            Start();
        }

        private int SpreedControl()
        {
            int Milliseconds;

            if (RequestedTime.Hours >= 1 || RequestedTime.Minutes >= 1)
                Milliseconds = 500;
            else if (RequestedTime.Seconds >= 10)
                Milliseconds = 250;
            else
                Milliseconds = 125;
            return Milliseconds;

        }
    }
}
