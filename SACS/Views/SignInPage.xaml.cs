using SACS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SACS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            var vm = new SignInViewModel();
            this.BindingContext = vm;
            vm.DisplayError += () => DisplayAlert("Error", vm.Error, "OK");
            vm.SuccessLogin += async () => await Shell.Current.GoToAsync($"//{nameof(SchedulePage)}");
            InitializeComponent();
        }
        //buttonclic = vm.SubmitCommand.Execute(null);
    }
}