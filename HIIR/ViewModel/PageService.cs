using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HIIR.ViewModel
{
    public class PageService : IPageService
    {
        private Page mainPage = Application.Current.MainPage;
        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel = "")
        {
            return await Application.Current.MainPage.DisplayAlert(title, message, ok, cancel);
        }

        public async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task PushAsync(Page page)
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(page);
            }
            catch(Exception e)
            {
                await this.DisplayAlert("Error",e.Message, "Ok", "Cancel");
            }
        }
    }
}
