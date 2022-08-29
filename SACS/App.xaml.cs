using SACS.Services;
using SACS.Views;
using SACS.Resources;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Stick-Regular.ttf", Alias = "Stick")]
[assembly: ExportFont("OpenSans-Regular.ttf", Alias = "Open Sans")]
namespace SACS
{    
    public partial class App : Application
    {

        public App()
        {
            LocalizationResourceManager.Current.Init(Text.ResourceManager);
            InitializeComponent();
            /*
            var resources = new ResourceDictionary
            {
                ["Color"] = "#FF2196F3",
                ["FontFamily"] = "Droid Sans",
                ["FontSize"] = 14,
                ["Theme"] = "Light",
                ["Language"] = "en"
            };
            
            DependencyService.RegisterSingleton(resources);*/
            DependencyService.Register<MockDataStore>();
            DependencyService.Register<AuthService>();
            DependencyService.Register<ScheduleService>();
            DependencyService.Register<DataService>();
            var AppData = new AppData();
            DependencyService.RegisterSingleton(AppData);
            var x = DependencyService.Get<AppData>();
            MainPage = new AppShell();
            //MainPage = new SignInPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
