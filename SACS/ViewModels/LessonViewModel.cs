using System;
using System.Collections.Generic;
using System.Text;
using SACS.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Interfaces.DTOs;
using SACS.Services;
using System.Linq;

namespace SACS.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class LessonViewModel : BaseViewModel
    {
        private string itemId;
        public string Id { get; set; }
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        private Lesson _selectedItem;
        public ObservableCollection<Grouping<string,AttendanceStudent>> Items { get; }
        public ObservableCollection<AttendanceStudent> Items2 { get; }
        public Command LoadItemsCommand { get; }
        public Command<Item> ItemTapped { get; }

        public LessonViewModel()
        {
            Title = GetTitle();
            LoadItemId(ItemId);
            Items = new ObservableCollection<Grouping<string, AttendanceStudent>>();
            Items2 = new ObservableCollection<AttendanceStudent>();
            LoadStudents();
            LoadItemsCommand = new Command(async () => await LoadStudents());
            //ItemTapped = new Command<Item>(OnItemSelected);
        }
        private string GetTitle()
        {
            return $"{_selectedItem.Subject} ({_selectedItem.LessonType})";
        }
        public async Task LoadStudents() {

            IsBusy = true;
            var AppData = DependencyService.Get<AppData>();
            try
            {
                Items.Clear();
                Items2.Clear();
                var _service = DependencyService.Get<IData>();
                var list = new List<string>();
                list.Add("951001");
                list.Add("951002");
                list.Add("951003");
                list.Add("951004");
                list.Add("951005");
                list.Add("951006");
                list.Add("951007");
                //var itemsResult = await _service.GetStudentsByGroups(list);/////////
                var itemsResult = await _service.GetStudentsByGroups(_selectedItem.GetGroupsList());
                /////////
                //var items = itemsResult;
                //var week = AppData.CurrentWeek;
                //var items = itemsResult.GetTodayLessons(week);
                //Refresh();
                //if (items.Count == 0) ScheduleNotFound();
                foreach (var item in itemsResult.GetStudents())
                {
                    Items2.Add(item);
                }
                    
                foreach (var item in itemsResult)
                {
                    Items.Add(new Grouping<string, AttendanceStudent>(item.Key,item.Value.GetAttendanceStudents()));
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
        public void LoadItemId(string itemId)
        {
            try
            {
                var AppData = DependencyService.Get<AppData>();
                _selectedItem = AppData.CurrentLessons.CurrentItem;
                //_selectedItem= AppData.CurrentLessons.Items.Where(x => x.Id == itemId).FirstOrDefault();
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        public Action DisplayError;
        public Action Refresh;
        //public Action ItemClicked;
        public Action ScheduleNotFound;

        private string error;
        public string Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }
    }
}
