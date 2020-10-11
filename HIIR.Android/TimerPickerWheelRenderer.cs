using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using HIIR;
using HIIR.Model;
using HIIR.Droid;
using System.ComponentModel;
using System.Windows.Markup;
using Android.Graphics.Drawables;
using Android.Graphics;
using Plugin.MaterialDesignControls.Implementations;
using Android.Views;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using static Android.Widget.NumberPicker;

[assembly: ExportRenderer(typeof(TimerPickerWheel), typeof(TimerPickerWheelRenderer))]
namespace HIIR.Droid
{
    public class TimerPickerWheelRenderer : ViewRenderer , IOnTimeEventListener
    {
        private MinutesSecondsWheelPicker _minutesSecondsWheelPicker;
        private Context _context;
        TimerPickerWheel SharedWheelPicker;
        // public event PropertyChangedEventHandler PropertyChanged;

        //private CustomNumberPicker numPickerSeconds;


        public TimerPickerWheelRenderer(Context context) : base(context)
        {
            _context = context;
        }
        


        
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
                return;

            if (Control == null)
            {
                
              // _minutesSecondsWheelPicker = new MinutesSecondsWheelPicker(_context);

                if (e.NewElement != null && e.NewElement is TimerPickerWheel timerPickerWheel)
                {


                    _minutesSecondsWheelPicker = new MinutesSecondsWheelPicker(_context);
                  
                    SharedWheelPicker = timerPickerWheel;
                    _minutesSecondsWheelPicker.setTimeChangeEentListener(this);

                    _minutesSecondsWheelPicker.DividerColor = timerPickerWheel.DividerColor.ToAndroid();
                    _minutesSecondsWheelPicker.ShowDivider = timerPickerWheel.ShowDivider;
                    _minutesSecondsWheelPicker.TextSize = timerPickerWheel.TextSize;
                    _minutesSecondsWheelPicker.TextColor = timerPickerWheel.AllTextColor.ToAndroid();
                    //_minutesSecondsWheelPicker.listener
                    //timerPickerWheel.SelectedTimeValue = _minutesSecondsWheelPicker.TimeValue;
                    // timerPickerWheel.Time = _minutesSecondsWheelPicker.TimeValue;
                    //timerPickerWheel.SetBinding(TimerPickerWheel.TimeProperty, new Binding(_minutesSecondsWheelPicker.TimeValue));
                    //timerPickerWheel.SetValue(TimerPickerWheel.TimeProperty, _minutesSecondsWheelPicker.TimeValue);
                    
                }
                SetNativeControl(_minutesSecondsWheelPicker);

            }

        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);


            //var stack = (TimerPickerWheel)sender;

            //if (e.PropertyName == TimerPickerWheel.FirstTimeProperty.PropertyName)
            //{
            //    stack.FirstTime = new TimeSpan(_minutesSecondsWheelPicker.Time.Ticks);
            //}

            //else if (e.PropertyName == TimerPickerWheel.SecondTimeProperty.PropertyName)
            //{
            //    stack.SecondTime = new TimeSpan( _minutesSecondsWheelPicker.Time.Ticks);
            //}   

            //else if (e.PropertyName == TimerPickerWheel.TimeProperty.PropertyName)
            //{
            //    stack.Time = new TimeSpan( _minutesSecondsWheelPicker.Time.Ticks);
            //}
            


        }

        private void OnStarted(object sender, EventArgs eventArgs)
        {
            ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedProperty, true);


        }

        private void OnEnded(object sender, EventArgs eventArgs)
        {
            ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
        }

        public void onTimeChanged()
        {
            SharedWheelPicker.TimeChanaged(_minutesSecondsWheelPicker.Time);
        }      
    }


    public interface IOnTimeEventListener
    {
        void onTimeChanged();
    }

    public class MinutesSecondsWheelPicker : LinearLayout, IOnValueChangeListener
    {
        //private Android.Widget.LinearLayout timePickerLayout;
        private CustomNumberPicker numPickerMinutes;
        private CustomNumberPicker numPickerSeconds;
        private TextView minTxt;
        private TextView secTxt;
        private TimeSpan TimeValue = new TimeSpan();
        private const float TITELS_TEXT_SIZE = 16;
        private IOnTimeEventListener mListener = null;


        public MinutesSecondsWheelPicker(Context context ) :base(context)
        {
            
            prepareView(context);


        }



        private float _wheelTextsize = 50;
        public float TextSize 
        {
            get { return _wheelTextsize; }
            set
            {
                _wheelTextsize = value;
                numPickerMinutes.TextSize = _wheelTextsize;
                numPickerSeconds.TextSize = _wheelTextsize;
            }
        }

        private Android.Graphics.Color _dividerColor = Android.Graphics.Color.Black;
        public Android.Graphics.Color DividerColor 
        {
            get { return _dividerColor; }
            set
            {
                numPickerMinutes.DividerColor = value;
                numPickerSeconds.DividerColor = value;
                _dividerColor = value;
            }
        }


        private bool _showDivider = false;
        public bool ShowDivider {
            get { return _showDivider; }
            set 
            {
                if (value)
                {
                    numPickerMinutes.DividerColor = Android.Graphics.Color.Transparent;
                    numPickerSeconds.DividerColor = Android.Graphics.Color.Transparent;
                    _showDivider = value;
                }
                else
                {
                    DividerColor = _dividerColor; // original color
                }
            }
        }

        private Android.Graphics.Color _textColor = Android.Graphics.Color.Black;
        public Android.Graphics.Color TextColor
        {
            get { return _textColor; }
            set 
            {
                numPickerMinutes.TextColor = value;
                numPickerSeconds.TextColor = value;
                minTxt.SetTextColor(value);
                secTxt.SetTextColor(value);
                _textColor = value;

            }
        }

        //private TimeSpan _selectedTimeValue = new TimeSpan(0,0,1);
        public TimeSpan SelectedTimeValue()
        {

            return new TimeSpan(0, numPickerMinutes.Value, numPickerSeconds.Value);
                       
        }

        public TimeSpan Time {
            get 
            {

                return TimeValue;
            }
            set
            {
                if(TimeValue != value)
                {
                    TimeValue = value;
                    if(mListener != null)
                        onTimeChanged();
                }
            }
        }

        public void setTimeChangeEentListener(IOnTimeEventListener eventListener)
        {
            mListener = eventListener;
        }

        private void prepareView(Context _context)
        {
            this.SetGravity(GravityFlags.Center);
            this.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent);
            



            numPickerMinutes = new CustomNumberPicker(_context);
            numPickerMinutes.SetOnValueChangedListener(this);
            numPickerMinutes.DividerColor = Android.Graphics.Color.Transparent;
            numPickerMinutes.TextSize = TextSize;
            numPickerMinutes.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            
            numPickerMinutes.MaxValue = 60;
            numPickerMinutes.MinValue = 1;

            this.AddView(numPickerMinutes);

            minTxt = new TextView(_context);
            minTxt.SetText("min", TextView.BufferType.Normal);
            minTxt.SetTextColor(new Android.Graphics.Color(Android.Graphics.Color.Black));
            minTxt.SetTextSize(Android.Util.ComplexUnitType.Sp, TITELS_TEXT_SIZE);
            minTxt.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);

            this.AddView(minTxt);

            numPickerSeconds = new CustomNumberPicker(_context);
            numPickerSeconds.SetOnValueChangedListener(this);

            numPickerSeconds.DividerColor = Android.Graphics.Color.Transparent;
            numPickerSeconds.TextSize = TextSize;
            numPickerSeconds.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            numPickerSeconds.MaxValue = 59;
            numPickerSeconds.MinValue = 0;

            this.AddView(numPickerSeconds);

            secTxt = new TextView(_context);
            secTxt.SetText("sec", TextView.BufferType.Normal);
            secTxt.SetTextColor(new Android.Graphics.Color(Android.Graphics.Color.Black));
            secTxt.SetTextSize(Android.Util.ComplexUnitType.Sp, TITELS_TEXT_SIZE);
            secTxt.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);

            this.AddView(secTxt);
        }

        public void OnValueChange(NumberPicker picker, int oldVal, int newVal)
        {

            Handler handler = new Handler();
            Action action = () =>
            {
                Time  = new TimeSpan(0, numPickerMinutes.Value, numPickerSeconds.Value);
            };
            handler.PostDelayed(action, 500);
        }

        public void onTimeChanged()
        {
            mListener.onTimeChanged();
        }
    }

    public class CustomNumberPicker : NumberPicker 
    {
        private const float GESTURE_THRESHOLD_DIP = 16.0f;
        
        
        
        public CustomNumberPicker(Context context) : base(context)
        {
            
        }

        private Android.Graphics.Color _dividerColor = Android.Graphics.Color.Black;

        public Android.Graphics.Color DividerColor 
        {
            get { return _dividerColor; }
            set
            {
                SetDividerColor(value);
                
            }
        }

        private float _textSize = 49; // numberpicker defaultive text size
        public float TextSize 
        {
            get { return _textSize; }
            set
            {
                SetTextSize(value);
            }
        }

        private Android.Graphics.Color _textColor = Android.Graphics.Color.Black; // numberpicker defaultive text color
        public Android.Graphics.Color TextColor
        {
            get { return _textColor; }
            set
            {
                SetTextColor(value);
            }
        }

        private void SetDividerColor(Android.Graphics.Color color)
        {
            try
            {
                var numberPickerType = Java.Lang.Class.FromType(typeof(NumberPicker));
                var divider = numberPickerType.GetDeclaredField("mSelectionDivider");
                divider.Accessible = true;

                var dividerDraw = new ColorDrawable(color);
                divider.Set(this, dividerDraw);
                
                _dividerColor = color;
            }
            catch
            {
                // ignored
            }
        }

        private void SetTextSize(float Size)
        {
            try
            {
                //changing the first user option text size
                float scale = Context.Resources.DisplayMetrics.ScaledDensity;
                float pxSize = (GESTURE_THRESHOLD_DIP * scale + Size); // converting dip to px

                int count = this.ChildCount;

                for (int i = 0; i < count; i++)
                {
                    Android.Views.View child = this.GetChildAt(i);
                    if (this.GetChildAt(i).GetType() == typeof(EditText))
                        ((EditText)child).SetTextSize(Android.Util.ComplexUnitType.Px, pxSize); //size in px
                }

                // changing the rest of the options text
                var numPickerType = Java.Lang.Class.FromType(typeof(NumberPicker));
                var textSize = numPickerType.GetDeclaredField("mSelectorWheelPaint");

                textSize.Accessible = true;

                ((Paint)textSize.Get(this)).TextSize = pxSize; //size in dip
                _textSize = Size;



            }
            catch
            {
                // ignored
            }            
        }


        private void SetTextColor(Android.Graphics.Color TextColor)
        {
            try
            {                
                int count = this.ChildCount;

                for (int i = 0; i < count; i++)
                {
                    Android.Views.View child = this.GetChildAt(i);
                    if (this.GetChildAt(i).GetType() == typeof(EditText))
                        ((EditText)child).SetTextColor(TextColor);
                }

                // changing the rest of the options text
                var numPickerType = Java.Lang.Class.FromType(typeof(NumberPicker));
                var textColorField = numPickerType.GetDeclaredField("mSelectorWheelPaint");

                textColorField.Accessible = true;

                ((Paint)textColorField.Get(this)).Color = TextColor; 
                _textColor = TextColor;



            }
            catch
            {
                // ignored
            }
        }

    }

}