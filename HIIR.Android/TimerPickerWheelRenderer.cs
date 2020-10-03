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



[assembly: ExportRenderer(typeof(TimerPickerWheel), typeof(TimerPickerWheelRenderer))]
namespace HIIR.Droid
{
    public class TimerPickerWheelRenderer : ViewRenderer
    {
        private MinutesSecondsWheelPicker _minutesSecondsWheelPicker;
        private Context _context;
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


                _minutesSecondsWheelPicker = new MinutesSecondsWheelPicker(_context);
                if (e.NewElement != null && e.NewElement is TimerPickerWheel timerPickerWheel)
                {
                    _minutesSecondsWheelPicker.DividerColor = timerPickerWheel.DividerColor.ToAndroid();
                    _minutesSecondsWheelPicker.ShowDivider = timerPickerWheel.ShowDivider;
                    _minutesSecondsWheelPicker.TextSize = timerPickerWheel.TextSize;

                }
                SetNativeControl(_minutesSecondsWheelPicker);

            }
        }

        // **Fetching native android native from axml file --> succeed but couldn't replace the basic attributes to my own custom attributes**
        //
        //protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
        //{
        //    base.OnElementChanged(e);
        //    if (e.OldElement != null || this.Element == null)
        //        return;
        //    if (Control == null)
        //    {
        //        MainActivity activity = Android.App.Application.Context as MainActivity;
        //        LayoutInflater inflater = (LayoutInflater)Context.GetSystemService(Context.LayoutInflaterService);
        //        timePickerLayout = (Android.Widget.LinearLayout)inflater.Inflate(Resource.Layout.TimePickerLayout, null);
        //        seconds = new NumberPicker( timePickerLayout.FindViewById<Android.Widget.NumberPicker>(Resource.Id.numpicker_seconds));
        //        seconds.MaxValue = 59;
        //        seconds.MinValue = 1;
        //        minutes = new NumberPicker(timePickerLayout.FindViewById<Android.Widget.NumberPicker>(Resource.Id.numpicker_minutes));
        //        minutes.MaxValue = 23;
        //        minutes.MinValue = 0;
        //        SetNativeControl(timePickerLayout);
        //    }
        //}


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

        }

        private void OnStarted(object sender, EventArgs eventArgs)
        {
            ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedProperty, true);


        }

        private void OnEnded(object sender, EventArgs eventArgs)
        {
            ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
        }


    }


    public class MinutesSecondsWheelPicker : LinearLayout
    {
        //private Android.Widget.LinearLayout timePickerLayout;
        private CustomNumberPicker numPickerMinutes;
        private CustomNumberPicker numPickerSeconds;
        private const float TITELS_TEXT_SIZE = 22;

        public MinutesSecondsWheelPicker(Context context) :base(context)
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

        private void prepareView(Context _context)
        {
            this.SetGravity(GravityFlags.Center);
            this.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            



            numPickerMinutes = new CustomNumberPicker(_context);
            numPickerMinutes.DividerColor = Android.Graphics.Color.Transparent;
            numPickerMinutes.TextSize = TextSize;
            numPickerMinutes.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            
            numPickerMinutes.MaxValue = 59;
            numPickerMinutes.MinValue = 1;

            this.AddView(numPickerMinutes);

            TextView minTxt = new TextView(_context);
            minTxt.SetText("min", TextView.BufferType.Normal);
            minTxt.SetTextColor(new Android.Graphics.Color(Android.Graphics.Color.Black));
            minTxt.SetTextSize(Android.Util.ComplexUnitType.Sp, TITELS_TEXT_SIZE);
            minTxt.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);

            this.AddView(minTxt);

            numPickerSeconds = new CustomNumberPicker(_context);
            numPickerSeconds.DividerColor = Android.Graphics.Color.Transparent;
            numPickerSeconds.TextSize = TextSize;
            numPickerSeconds.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
            numPickerSeconds.MaxValue = 60;
            numPickerSeconds.MinValue = 0;

            this.AddView(numPickerSeconds);

            TextView secTxt = new TextView(_context);
            secTxt.SetText("sec", TextView.BufferType.Normal);
            secTxt.SetTextColor(new Android.Graphics.Color(Android.Graphics.Color.Black));
            secTxt.SetTextSize(Android.Util.ComplexUnitType.Sp, TITELS_TEXT_SIZE);
            secTxt.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);

            this.AddView(secTxt);
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
        
    }

}