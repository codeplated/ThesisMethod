using System;
using System.Diagnostics;
using ThesisMethod.Models;

using Xamarin.Forms;

namespace ThesisMethod.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }
        private static ILogger logger = DependencyService.Get<ILogManager>().GetLog();
        private static string TAG = "------------NewItemPage.xaml.cs ";
        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is a nice description"
            };

            BindingContext = this;
        }
        protected override void OnAppearing()
        {
            
            if (!string.IsNullOrEmpty(Title))
            {
                logger.InfoNavigational(InfoNavigational.pageName, Title);
                Debug.WriteLine(TAG + "NewItem page = " + Title);
            }
            
            base.OnAppearing();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}