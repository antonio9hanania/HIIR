using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using HIIR;
using HIIR.iOS;

[assembly: ExportRenderer(typeof(TimerPickerWheel), typeof(TimerPickerWheelRenderer))]

namespace HIIR.iOS
{
    public class TimerPickerWheelRenderer : TimePickerRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }

    }
}