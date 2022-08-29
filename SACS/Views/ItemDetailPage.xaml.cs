using SACS.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SACS.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}