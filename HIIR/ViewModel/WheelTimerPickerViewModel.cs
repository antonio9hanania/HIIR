using System;
using System.Collections.Generic;
using System.Text;

namespace HIIR.ViewModel
{
    public class WheelTimerPickerViewModel
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
