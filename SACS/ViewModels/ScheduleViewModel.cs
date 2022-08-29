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
using Interfaces.Shedule.IisApi;
using System.Collections.ObjectModel;
using SACS.Models;
using SACS.Views;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SACS.ViewModels
{
    public class ScheduleViewModel : BaseViewModel
    {
        private Lesson _selectedItem;
        public ObservableCollection<Lesson> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command<Lesson> ItemTapped { get; }
        public ScheduleViewModel()
        {
            Title = "Schedule";
            Items = new ObservableCollection<Lesson>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ItemTapped = new Command<Lesson>(OnItemSelected);
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            var AppData = DependencyService.Get<AppData>();
            try
            {
                Items.Clear();
                var _service = DependencyService.Get<ISchedule>();
                var items = await _service.LoadSchedule(AppData.User);
                foreach (var item in items)
                {
                    Items.Add(new Lesson(Guid.NewGuid().ToString(), item));
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                DisplayError();
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Lesson SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Lesson item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            //await Shell.Current.GoToAsync($"{nameof(LessonPage)}?{nameof(LessonDetailViewModel.ItemId)}={item.Id}");
            await Shell.Current.GoToAsync($"{nameof(AboutPage)}");
        }


        public Action DisplayError;
        public Action ItemClicked;
       
        private string error;
        public string Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }
    }
}
