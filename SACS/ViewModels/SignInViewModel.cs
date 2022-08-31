using System;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using Interfaces.Models;
using System.Linq;
using System.Security.Claims;
using SACS.Services;

namespace SACS.ViewModels
{
    class SignInViewModel : BaseViewModel
    {
        public Action DisplayError;
        public Action SuccessLogin;
        private string username;
        public string Username
        {
            get => username; 
            set=> SetProperty(ref username, value);
        }
        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }
        private string error;
        public string Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }
        public ICommand SubmitCommand { protected set; get; }
        public SignInViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }
        public void OnSubmit()
        {
            IsBusy = true;
            var _service = DependencyService.Get<IAuth>();
            var result = _service.Login(Username, Password).Result;
            IsBusy = false;
            if (!(result == string.Empty))
            {
                Error = result;
                DisplayError();
            }
            else {
                SuccessLogin();
            }
        }
    }
}
