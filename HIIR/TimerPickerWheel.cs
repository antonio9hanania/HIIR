using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HIIR
{
    public class TimerPickerWheel : TimePicker
    {

        //public event Func<TimeSpan> TimeValueChanged ;




        #region Bindable Properties

        public static readonly BindableProperty TextSizeProperty =
            BindableProperty.Create(nameof(TextSize), typeof(float), typeof(TimerPickerWheel));

        public static readonly BindableProperty DividerColorProperty =
            BindableProperty.Create(nameof(DividerColor), typeof(Color), typeof(TimerPickerWheel));
        
        public static readonly BindableProperty ShowDividerProperty =
            BindableProperty.Create(nameof(ShowDivider), typeof(bool), typeof(TimerPickerWheel));


        //public static readonly BindableProperty FirstTimeProperty =
        //    BindableProperty.Create(nameof(FirstTime), typeof(TimeSpan), typeof(TimerPickerWheel), new TimeSpan(0, 1, 0), propertyChanged: (s, o, n) => { (s as TimerPickerWheel).OnTimeChanged(new EventArgs()); });

        //public static readonly BindableProperty SecondTimeProperty =
        //    BindableProperty.Create(nameof(SecondTime), typeof(TimeSpan), typeof(TimerPickerWheel),new TimeSpan(0,1,0), propertyChanged: (s, o, n) => { (s as TimerPickerWheel).OnTimeChanged(new EventArgs()); });

        public static readonly BindableProperty TimeValueProperty =
            BindableProperty.Create(nameof(TimeValue), typeof(TimeSpan), typeof(TimerPickerWheel), new TimeSpan(0, 1, 0), propertyChanged: (s, o, n) => { (s as TimerPickerWheel).OnTimeChanged(new EventArgs()); });

        public static readonly BindableProperty AllTextColorProperty =
            BindableProperty.Create(nameof(AllTextColor), typeof(Color), typeof(TimerPickerWheel));



        #endregion

        #region Properties





        public float TextSize
        {
            get
            {
                return GetValue<float>(TextSizeProperty);
            }
            set
            {
                SetValue(TextSizeProperty, value);
            }
        }     

        public Color AllTextColor
        {
            get
            {
                return GetValue<Color>(AllTextColorProperty);
            }
            set
            {
                SetValue(AllTextColorProperty, value);
            }
        }

        public Color DividerColor
        {
            get
            {
                return GetValue<Color>(DividerColorProperty);
            }
            set
            {
                SetValue(DividerColorProperty, value);
            }
        }

        public bool ShowDivider
        {
            get
            {
                return GetValue<bool>(ShowDividerProperty);
            }
            set
            {
                SetValue(ShowDividerProperty, value);
            }
        }

        //public TimeSpan FirstTime
        //{
        //    get
        //    {
        //        return GetValue<TimeSpan>(FirstTimeProperty);
        //    }
        //    set
        //    {
        //        SetValue(FirstTimeProperty, new TimeSpan( value.Ticks));
        //    }
        //}      

        //public TimeSpan SecondTime
        //{
        //    get
        //    {
        //        return GetValue<TimeSpan>(SecondTimeProperty);
        //    }
        //    set
        //    {
        //        SetValue(SecondTimeProperty, new TimeSpan( value.Ticks));
        //    }
        //}   

        public TimeSpan TimeValue {
            get
            {
                return GetValue<TimeSpan>(TimeValueProperty);
            }
            set
            {
                SetValue(TimeValueProperty, value);
            }
        }


        public event EventHandler Changed;



        #endregion

        #region Methods

        public T GetValue<T>(BindableProperty property)
        {
            return (T)GetValue(property);
        }

        public void TimeChanaged(TimeSpan value)
        {
            SetValue(TimeValueProperty, value);
            //SetValue(FirstTimeProperty, new TimeSpan(value.Ticks));
            //SetValue(SecondTimeProperty, new TimeSpan(value.Ticks));

        }

        protected virtual void OnTimeChanged(EventArgs e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        #endregion
    }
}
