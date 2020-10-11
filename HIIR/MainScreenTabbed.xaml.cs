using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace HIIR
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainScreenTabbed : Xamarin.Forms.TabbedPage
    {
        public MainScreenTabbed()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Android>().SetIsSwipePagingEnabled(true);

        }
    }
}