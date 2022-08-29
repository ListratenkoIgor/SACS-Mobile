using SACS.Services;
using SACS.ViewModels;
using SACS.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SACS
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(LessonPage), typeof(LessonPage));
            CurrentItem = new SignInPage();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            var auth = DependencyService.Get<IAuth>();
            auth.Logout();
            await Shell.Current.GoToAsync("//SignInPage");
        }
    }
}
