using System;
using System.Diagnostics;
using ThesisMethod.Models;
using ThesisMethod.ViewModels;

using Xamarin.Forms;

namespace ThesisMethod.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
        private static string TAG = "------------ItemsPage.xaml.cs ";
        private static ILogger logger = DependencyService.Get<ILogManager>().GetLog();
        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }
       
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }
        protected override bool OnBackButtonPressed()
        {
            logger.InfoNavigational(InfoNavigational.backButtonPressed);
            return true;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if (!string.IsNullOrEmpty(Title))
            {
                logger.InfoNavigational(InfoNavigational.pageName, Title);
                Debug.WriteLine(TAG + "Items page = " + Title);
            }
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
