using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
using ZXing.QrCode;
using ZXing;
using SACS.ViewModels;
using ZXing.Aztec.Internal;
using ZXing.QrCode.Internal;

namespace SACS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRGeneratePage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        QRGenerateViewModel _viewModel;
        public QRGeneratePage()
        {
            InitializeComponent();
            _viewModel = new QRGenerateViewModel();
            //QRCodeView = GenerateQR();
            QRCodeView.BarcodeValue = GetToken();
            QRCodeView.IsVisible = true;
        }
        private string GetToken() {
            var AppData = DependencyService.Get<AppData>();
            var token = AppData.EncodeToken();
            token = "qwertyuiiiiiiopoiuyt";
            return token;
        }
        ZXingBarcodeImageView GenerateQR()
        {
            var AppData = DependencyService.Get<AppData>();
            var token = AppData.EncodeToken();
            token = "qwertyuiiiiiiopoiuyt";
            var qrCode = new ZXingBarcodeImageView
            {
                BarcodeFormat = BarcodeFormat.QR_CODE,
                BarcodeOptions = new QrCodeEncodingOptions
                {
                    Height = 350,
                    Width = 350
                },
                BarcodeValue = token,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };
            // Workaround for iOS
            qrCode.WidthRequest = 350;
            qrCode.HeightRequest = 350;
            return qrCode;
        }
    }
}
