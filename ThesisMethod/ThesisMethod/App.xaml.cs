using ThesisMethod.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ThesisMethod
{
    public partial class App : Application
    {
        private static ILogger logger = DependencyService.Get<ILogManager>().GetLog();

        public App()
        {
            InitializeComponent();
            logger.Info("App Start", "navigational");
            SetMainPage();
        }
        protected override void OnStart()
        {
            // Handle when your app starts
            logger.Fatal("app started" , "informations", "not happeing");
            DependencyService.Get<ILogManager>().HttpUploadFile();
        }

        public static void SetMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    },
                }
            };
        }
    }
}
