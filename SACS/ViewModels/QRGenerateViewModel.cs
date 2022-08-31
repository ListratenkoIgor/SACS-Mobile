using Newtonsoft.Json;
using SACS.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SACS.ViewModels
{
    internal class QRGenerateViewModel : BaseViewModel
    {
        public Action DisplayError;
        public Action SuccessLogin;
        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }
        private string token;
        public string Token
        {
            get => token;
            set => SetProperty(ref token, value);
        }
        private string error;
        public string Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }
        public ICommand GenerateQR { protected set; get; }
        public QRGenerateViewModel()
        {
            //GenerateQR = new Command(OnGenerateQR);
        }

        private void OnGenerateQR(object obj)
        {
            var AppData = DependencyService.Get<AppData>();
            Token = AppData.EncodeToken();
        }
    }
}
