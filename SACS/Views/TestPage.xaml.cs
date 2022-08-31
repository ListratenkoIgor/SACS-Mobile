using SACS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SACS.Views
{
    public class PageModel {
        public string Title { get; set; }
        public string ShortName { get; set; } //will be used for jump lists
        public string Subtitle { get; set; }
        public PageModel(string title, string shortname, string subtitle)
        {
            Title = title;
            ShortName = shortname;
            Subtitle = subtitle;
        }
    }
    public class PageTypeGroup : List<PageModel>
    {
        public string Title { get; set; }
        public string ShortName { get; set; } //will be used for jump lists
        public string Subtitle { get; set; }
        private PageTypeGroup(string title, string shortName)
        {
            Title = title;
            ShortName = shortName;
        }
        static PageTypeGroup()
        {
            List<PageTypeGroup> Groups = new List<PageTypeGroup> {
            new PageTypeGroup ("Alpha", "A"){
                new PageModel("Amelia", "Cedar",""),
                new PageModel("Alfie", "Spruce", "grapefruit.jpg"),
                new PageModel("Ava", "Pine","ruit.jpg"),
                new PageModel("Archie", "Maple", "apefruit.jpg")
            },
            new PageTypeGroup ("Bravo", "B"){
                new PageModel("Brooke", "Lumia", ""),
                new PageModel("Bobby", "Xperia", "apefruit.jpg"),
                new PageModel("Bella", "Desire", "apefruit.jpg"),
                new PageModel("Ben", "Chocolate","rapefruit.jpg")
            } };
            All = Groups; //set the publicly accessible list
        }
        public static IList<PageTypeGroup> All { private set; get; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public TestPage()
        {
            InitializeComponent();
            BindingContext = new TestViewModel();
        }
    }
}