using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Collections;
using System.Threading;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using HIIR.Model;

namespace HIIR
{

    public class Tts
    {
        private IEnumerable<CrossLocale> locales;
        private CrossLocale locale;

        public Tts()
        {
            Task.Run(async () =>
            {
                locales = await CrossTextToSpeech.Current.GetInstalledLanguages();
                if (Device.RuntimePlatform == Device.Android)
                    locale = locales.FirstOrDefault(l => l.ToString() == "en-US");
                else
                    locale = new CrossLocale { Language = "en-US" };//fine for iOS
            });
        }


        public void SpeakNow(string text,float i_pitch = 1f, float i_speakRate = 0f, float i_volume = 0.8f )
        {
            Task.Run(async () =>
            {
                await CrossTextToSpeech.Current.Speak(text,
                pitch: i_pitch,
                speakRate: i_speakRate,
                volume: i_volume,
                crossLocale: locale);
            });
        }
    }
}
