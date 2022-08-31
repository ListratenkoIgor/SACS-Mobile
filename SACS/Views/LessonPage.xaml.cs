using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SACS.ViewModels;
using SACS.Models;

namespace SACS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LessonPage : ContentPage
    {
        LessonViewModel _viewModel;
        public LessonPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new LessonViewModel();
            _viewModel.DisplayError += () => DisplayAlert("Error", _viewModel.Error, "OK");
            //_viewModel.ScheduleNotFound += () => { labelNotFound.IsVisible = true; ScheduleListView.IsVisible = false; };
            //_viewModel.Refresh += () => { labelNotFound.IsVisible = false; ScheduleListView.IsVisible = true; };
            //ScheduleListView.ItemsSource = _viewModel.Items;
            //ScheduleListView.ItemTemplate = GetDataTemplate();
            LessonListView.BindingContext = _viewModel;
            LessonListView.ItemsSource = _viewModel.Items2;
            //LessonListView.IsGroupingEnabled = true;
            //LessonListView.GroupDisplayBinding = new Binding("Key");
            //LessonListView.ItemTemplate = GetDataTemplate();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            //_viewModel.OnAppearing();
        }
        private DataTemplate GetDataTemplate2()
        {
            DataTemplate result;
            result = new DataTemplate(() =>
            {
                StackLayout stack = new StackLayout();
                Grid grid = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition(),
                        new RowDefinition(),
                        new RowDefinition()
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition{ Width = new GridLength(2, GridUnitType.Star)},
                        new ColumnDefinition(),
                        new ColumnDefinition(),
                        new ColumnDefinition{Width  = new GridLength(100)}
                    }
                };
                grid.SetBinding(Grid.ClassIdProperty, "RecordbookNumber");
                Image image = new Image {HeightRequest=120,WidthRequest=100};
                Label labelFirstname = new Label();
                Label labelLastname = new Label();
                Label LabelMiddlename = new Label();
                Label labelAttendanceTime = new Label();
                Switch switchPresent = new Switch();
                image.SetBinding(Image.SourceProperty, "ImageUrl");
                labelAttendanceTime.SetBinding(Label.TextProperty, "Auditory");
                labelFirstname.SetBinding(Label.TextProperty, "Start");
                labelLastname.SetBinding(Label.TextProperty, "End");
                LabelMiddlename.SetBinding(Label.TextProperty, "Subject");
                switchPresent.SetBinding(Switch.IsToggledProperty, "IsPresent",BindingMode.TwoWay);
                               
                grid.Children.Add(image, 0, 0);
                grid.Children.Add(labelAttendanceTime, 1, 1);
                grid.Children.Add(labelLastname, 2, 0);
                grid.Children.Add(labelFirstname, 2, 1);
                grid.Children.Add(LabelMiddlename, 2, 2);
                grid.Children.Add(switchPresent, 3, 1);
                stack.Children.Add(grid);
                stack.Padding = 10;
                return stack;
            });
            return result;
        }
        private DataTemplate GetDataTemplate()
        {
            DataTemplate result;
            result = new DataTemplate(() =>
            {
                StackLayout stack = new StackLayout();
                Grid grid = new Grid
                {
                    RowDefinitions =
                    {
                        new RowDefinition(),
                        new RowDefinition(),
                        new RowDefinition()
                    },
                    ColumnDefinitions =
                    {
                        new ColumnDefinition{ Width = new GridLength(2, GridUnitType.Star)},
                        new ColumnDefinition(),
                        new ColumnDefinition(),
                        new ColumnDefinition{Width  = new GridLength(100)}
                    }
                };
                grid.SetBinding(Grid.ClassIdProperty, "RecordbookNumber");
                Image image = new Image { HeightRequest = 120, WidthRequest = 100 };
                Label labelFirstname = new Label();
                Label labelLastname = new Label();
                Label LabelMiddlename = new Label();
                Label labelAttendanceTime = new Label();
                Switch switchPresent = new Switch();
                image.SetBinding(Image.SourceProperty, "ImageUrl");
                labelAttendanceTime.SetBinding(Label.TextProperty, "Auditory");
                labelFirstname.SetBinding(Label.TextProperty, "Start");
                labelLastname.SetBinding(Label.TextProperty, "End");
                LabelMiddlename.SetBinding(Label.TextProperty, "Subject");
                switchPresent.SetBinding(Switch.IsToggledProperty, "IsPresent", BindingMode.TwoWay);

                grid.Children.Add(image, 0, 0);
                grid.Children.Add(labelAttendanceTime, 1, 1);
                grid.Children.Add(labelLastname, 2, 0);
                grid.Children.Add(labelFirstname, 2, 1);
                grid.Children.Add(LabelMiddlename, 2, 2);
                grid.Children.Add(switchPresent, 3, 1);
                stack.Children.Add(grid);
                stack.Padding = 10;
                return stack;
            });
            return result;
        }
    }
}
