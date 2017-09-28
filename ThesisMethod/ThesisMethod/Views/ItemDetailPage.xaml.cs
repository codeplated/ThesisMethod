
using System.Diagnostics;
using ThesisMethod.ViewModels;

using Xamarin.Forms;

namespace ThesisMethod.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        private static string TAG = "------------ItemDetailPage.xaml.cs ";
        private static ILogger logger = DependencyService.Get<ILogManager>().GetLog();
        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            
            if (!string.IsNullOrEmpty(Title))
            {
                logger.InfoNavigational(InfoNavigational.pageName, Title);
                Debug.WriteLine(TAG + "Item detail page = " + Title);
            }
            base.OnAppearing();
        }
        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}
