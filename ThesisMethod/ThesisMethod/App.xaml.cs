
using System.Diagnostics;
using ThesisMethod.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ThesisMethod
{
    public partial class App : Application
    {
        private static ILogger logger = DependencyService.Get<ILogManager>().GetLog();
        private static string TAG = "------------App.xaml.cs ";

        public App()
        {
            InitializeComponent();

    
            logger.InfoApp(InfoApp.appIntializing);

            //logger.InfoNavigational(InfoNavigational.pageName, "Test page name");


            SetMainPage();
        }
        protected override void OnStart()
        {
           
            logger.InfoApp(InfoApp.appForeground);
            DependencyService.Get<ILogManager>().checkFileSizeAndUpload();
            base.OnStart();
        }
        protected override void OnResume()
        {
           
            logger.InfoApp(InfoApp.appForeground);
            DependencyService.Get<ILogManager>().checkFileSizeAndUpload();
            base.OnResume();
        }
        
        protected override void OnSleep()
        {
            logger.InfoApp(InfoApp.appBackground);
            base.OnSleep();
        }
        public Xamarin.Forms.Command<Point> CanvasTappedCommand { get { return new Xamarin.Forms.Command<Point>((p) => OnCanvasTapped(p)); } }
        public void OnCanvasTapped(Point p)
        {
            // your event handling logic
            
            

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
