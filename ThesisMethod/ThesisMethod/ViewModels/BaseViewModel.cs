using System.Diagnostics;
using ThesisMethod.Helpers;
using ThesisMethod.Models;
using ThesisMethod.Services;

using Xamarin.Forms;

namespace ThesisMethod.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public static ILogger logger = DependencyService.Get<ILogManager>().GetLog();
        private static string TAG = "------------BaseViewModel.cs ";
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }
        public Command<Point> CanvasTappedCommand { get { return new Command<Point>((p) => OnCanvasTapped(p)); } }
        public void OnCanvasTapped(Point p)
        {
            // your event handling logic
            if (!p.IsEmpty)
            {
                logger.InfoTouch(InfoTouch.cordinateX, p.X);
                logger.InfoTouch(InfoTouch.cordinateY, p.Y);
            }
            else
            {
                Debug.WriteLine(TAG + "p is empty");
            }

        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        
    }
}

