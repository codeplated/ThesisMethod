
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
            if (!string.IsNullOrEmpty(Title))
            {
                logger.InfoNavigational(InfoNavigational.pageTextColor, "#fffff" + "|" + Title);
                logger.InfoNavigational(InfoNavigational.pageBackgroundColor, "#fffff" + "|" + Title);
                logger.InfoNavigational(InfoNavigational.pageMainColor, "#2893e6" + "|" + Title);
            }
            else {
                Debug.WriteLine(TAG + "Item detail not avaialble =");
            }

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
        protected override bool OnBackButtonPressed()
        {
            logger.InfoNavigational(InfoNavigational.backButtonPressed);
            return true;
        }
        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }
    }
}
