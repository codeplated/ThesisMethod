using System;
using System.Diagnostics;
using System.Windows.Input;
using ThesisMethod.Gestures;
using Xamarin.Forms;

namespace ThesisMethod.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private static string TAG = "------------AboutViewModels.cs ";
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform"));
            

        }
        
        

        /// <summary>
        /// Command to open browser to xamarin.com
        /// </summary>
        public ICommand OpenWebCommand { get; }
    }
}
