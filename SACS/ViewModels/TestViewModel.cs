using Interfaces.DTOs;
using SACS.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel.Extensions;
using Xamarin.Forms;

namespace SACS.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
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
            }
        }
        private Lesson _selectedItem;
        public ObservableCollection<PlanItem> Plans { get; }
        public Command LoadItemsCommand { get; }
        public Command<Item> ItemTapped { get; }
    }

    public class PlanItem : List<PlanWorkItem>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        string Name;
        public PlanItem(RestPlanItem name, List<PlanWorkItem> planWorks):base(planWorks)
        {
            Name = name.Name;
        }   
    }

    public class RestPlanItem
    {
        public string Name { get; set; }
    }

    public class PlanWorkItem
    {
        public string SurName { get; set; }

    }
}
