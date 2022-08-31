using SACS.Services;
using SACS.ViewModels;
using SACS.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace SACS
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            try
            {

                InitializeComponent();
                Routing.RegisterRoute(nameof(LessonPage), typeof(LessonPage));
                Debug();
                CurrentItem = new QRGeneratePage();
            }
            catch (Exception e) { 
                
            }
        }
        private void Debug() {
            var _service = DependencyService.Get<IAuth>();
            var result = _service.Login("","").Result;
            var AppData = DependencyService.Get<AppData>();
            //AppData.User.UrlId = "a-ivaniuk";
            AppData.User.UrlId = "s-kulikov";
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            var auth = DependencyService.Get<IAuth>();
            auth.Logout();
            await Shell.Current.GoToAsync("//SignInPage");
        }
    }
}
