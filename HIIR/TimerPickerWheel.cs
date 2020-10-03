using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace HIIR
{
    public class TimerPickerWheel : TimePicker
    {

        #region Bindable Properties

        public static readonly BindableProperty TextSizeProperty =
            BindableProperty.Create(nameof(TextSize), typeof(float), typeof(TimerPickerWheel));

        public static readonly BindableProperty DividerColorProperty =
            BindableProperty.Create(nameof(DividerColor), typeof(Color), typeof(TimerPickerWheel));
        
        public static readonly BindableProperty ShowDividerProperty =
            BindableProperty.Create(nameof(ShowDivider), typeof(bool), typeof(TimerPickerWheel));


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


        #endregion

        #region Methods

        public T GetValue<T>(BindableProperty property)
        {
            return (T)GetValue(property);
        }

        #endregion
    }
}
