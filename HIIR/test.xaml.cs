﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HIIR
{



    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class test : ContentPage
    {

        public test()
        {
            
            InitializeComponent();
            //BindingContext = new MainPageModel();

        }

    }

    public class MainPageModel
    {

    }
}
