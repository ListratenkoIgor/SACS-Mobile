using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SACS.ViewModels;

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
                    Label labelStart = new Label { FontAttributes = FontAttributes.Bold }; ;
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
                    var recognizer = new TapGestureRecognizer
                    {
                        BindingContext = _viewModel,
                        Command = _viewModel.ItemTapped,
                        NumberOfTapsRequired = 1,
                        CommandParameter = "."
                    };
                    stack.GestureRecognizers.Add(recognizer);
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
                    grid.Children.Add(labelStart, 0, 0);
                    grid.Children.Add(labelEnd, 0, 1);
                    grid.Children.Add(labelSubject, 1, 0);
                    grid.Children.Add(labelAuditories, 1, 1);
                    grid.Children.Add(labelGroups, 2, 1);
                    stack.Children.Add(grid);
                    var recognizer = new TapGestureRecognizer
                    {
                        BindingContext =_viewModel,
                        Command = _viewModel.ItemTapped,
                        NumberOfTapsRequired=1,
                        CommandParameter = "."                        
                    };
                    stack.GestureRecognizers.Add(recognizer);
                    return stack;
                });
            }
            return result;
        }
    }
}
