using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.DeviceInfo;
using System;
using System.Threading.Tasks;

namespace ThesisMethod.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            //AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            //TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            System.Diagnostics.Debug.WriteLine("Device ID : " +CrossDevice.Hardware.DeviceId);
            System.Diagnostics.Debug.WriteLine("Screen Dimension : " + CrossDevice.Hardware.ScreenHeight +"*"+ CrossDevice.Hardware.ScreenWidth);
            System.Diagnostics.Debug.WriteLine("Manufactorer and model : " + CrossDevice.Hardware.Manufacturer + " "+ CrossDevice.Hardware.Model);
            System.Diagnostics.Debug.WriteLine("operating system and vesion : " + CrossDevice.Hardware.OperatingSystem + " " + CrossDevice.Hardware.OperatingSystemVersion);
            System.Diagnostics.Debug.WriteLine("App version number : " + CrossDevice.App.Version);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
        //private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        //{ 
        //    System.Diagnostics.Debug.WriteLine("Exeception catch of task scheduler type : "+ unobservedTaskExceptionEventArgs.Exception.ToString());
        //}
        //private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        //{
        //    System.Diagnostics.Debug.WriteLine("Exeception catch of Current domain type : " + unhandledExceptionEventArgs.ToString());
        //}
    }
}