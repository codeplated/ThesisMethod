
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
        }
        protected override void OnAppearing()
        {
            
            if (!string.IsNullOrEmpty(Title))
            {
                logger.InfoNavigational(InfoNavigational.pageName, Title);
                Debug.WriteLine(TAG + "About page  = " + Title);
            }
            base.OnAppearing();
        }
    }
}
