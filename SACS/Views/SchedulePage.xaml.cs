using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SACS.ViewModels;
using SACS.Models;

namespace SACS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : ContentPage
    {
        ScheduleViewModel _viewModel;
        public SchedulePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ScheduleViewModel();
            _viewModel.DisplayError += () => DisplayAlert("Error", _viewModel.Error, "OK");
            _viewModel.ScheduleNotFound += () => { labelNotFound.IsVisible = true; ScheduleListView.IsVisible = false;};
            _viewModel.Refresh += () => { labelNotFound.IsVisible = false; ScheduleListView.IsVisible = true;};
            ScheduleListView.ItemsSource = _viewModel.Items;
            ScheduleListView.ItemTemplate = GetDataTemplate();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
        private DataTemplate GetDataTemplate()
        {
            DataTemplate result;
            var AppData = DependencyService.Get<AppData>();
            if (AppData.User.isStudent)
            {
                result = new DataTemplate(() =>
                {
                    StackLayout stack = new StackLayout(); 
                    Label labelStart = new Label { FontAttributes = FontAttributes.Bold}; ;
                    Label labelEnd = new Label();
                    Label labelSubject = new Label { FontAttributes = FontAttributes.Bold }; ;
                    Label labelAuditories = new Label();
                    Label labelEmployee = new Label { HorizontalTextAlignment = TextAlignment.End };
                    
                    labelStart.SetBinding(Label.TextProperty, "Start");
                    labelEnd.SetBinding(Label.TextProperty, "End");
                    labelSubject.SetBinding(Label.TextProperty, "Subject");
                    labelAuditories.SetBinding(Label.TextProperty, "Auditory");
                    labelEmployee.SetBinding(Label.TextProperty, "Employee");
                    Grid grid = new Grid
                    {
                        RowDefinitions =
                        {
                            new RowDefinition(),
                            new RowDefinition(),
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition{ Width = new GridLength(2, GridUnitType.Star)},
                            new ColumnDefinition(),
                            new ColumnDefinition{Width  = new GridLength(100)}
                        }
                    };
                    grid.Children.Add(labelStart, 0, 0);
                    grid.Children.Add(labelEnd, 0, 1);
                    grid.Children.Add(labelSubject, 1, 0);
                    grid.Children.Add(labelAuditories, 1, 1);
                    grid.Children.Add(labelEmployee, 2, 1);
                    stack.Children.Add(grid);
                    stack.Padding = 10;
                    return stack;
                });
            }
            else
            {
                result = new DataTemplate(() =>
                {
                    StackLayout stack = new StackLayout();
                    Label labelStart = new Label { FontAttributes = FontAttributes.Bold }; ;
                    Label labelEnd = new Label();
                    Label labelSubject = new Label { FontAttributes = FontAttributes.Bold }; ;
                    Label labelAuditories = new Label();
                    Label labelGroups = new Label { HorizontalTextAlignment = TextAlignment.End };
                    
                    labelStart.SetBinding(Label.TextProperty, "Start");
                    labelEnd.SetBinding(Label.TextProperty, "End");
                    labelSubject.SetBinding(Label.TextProperty, "Subject");
                    labelAuditories.SetBinding(Label.TextProperty, "Auditory");
                    labelGroups.SetBinding(Label.TextProperty, "Groups");
                    Grid grid = new Grid
                    {
                        RowDefinitions =
                        {
                        new RowDefinition(),
                        new RowDefinition(),
                        },
                        ColumnDefinitions =
                        {
                        new ColumnDefinition{ Width = new GridLength(2, GridUnitType.Star)},
                        new ColumnDefinition(),
                        new ColumnDefinition{Width  = new GridLength(100)}
                        }
                    };
                    grid.SetBinding(Grid.ClassIdProperty, "Id");                    
                    grid.Children.Add(labelStart, 0, 0);
                    grid.Children.Add(labelEnd, 0, 1);
                    grid.Children.Add(labelSubject, 1, 0);
                    grid.Children.Add(labelAuditories, 1, 1);
                    grid.Children.Add(labelGroups, 2, 1);
                    var recognizer = new TapGestureRecognizer
                    {
                        NumberOfTapsRequired = 1
                    };
                    recognizer.Tapped += (s, e) => {
                        _viewModel.ItemTapped.Execute(_viewModel.Items.Where(x => x.Id == grid.ClassId).FirstOrDefault());
                    };
                    grid.GestureRecognizers.Add(recognizer);
                    stack.Children.Add(grid);
                    stack.Padding = 10;
                    return stack;
                });
            }
            return result;
        }
    }
}
