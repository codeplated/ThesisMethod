
using System.Diagnostics;
using Xamarin.Forms;

namespace ThesisMethod.Views
{
    public partial class AboutPage : ContentPage
    {
        private static string TAG = "------------About page.xaml.cs ";
        private static ILogger logger = DependencyService.Get<ILogManager>().GetLog();
        public AboutPage()
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(Title))
            {
                logger.InfoNavigational(InfoNavigational.pageTextColor, "#fffff" + "|" + Title);
                logger.InfoNavigational(InfoNavigational.pageBackgroundColor, "#fffff" + "|" + Title);
                logger.InfoNavigational(InfoNavigational.pageMainColor, "#2893e6" + "|" + Title);
            }

        }
        protected override void OnAppearing()
        {
            
            if (!string.IsNullOrEmpty(Title))
            {
                logger.InfoNavigational(InfoNavigational.pageName, Title);
               
            }
            base.OnAppearing();
        }
        protected override bool OnBackButtonPressed()
        {
            logger.InfoNavigational(InfoNavigational.backButtonPressed);
            return true;
        }
    }
}
