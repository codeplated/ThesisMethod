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
        //public delegate void UnhandledExceptionEventHandler(Object sender, UnhandledExceptionEventArgs e);
        //static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        //{
        //    Exception e = (Exception)args.ExceptionObject;
        //    Android.Util.Log.Debug("MyHandler caught : " , e.Message);
        //    Android.Util.Log.Debug("Runtime terminating: {0}", args.IsTerminating.ToString());
        //}
        protected override void OnCreate(Bundle bundle)
        {
            Console.WriteLine("------------Main Activity android");
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
            //try
            //{
            //    throw new Exception("1");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Catch clause caught : {0} \n", e.Message);
            //}

            
            base.OnCreate(bundle);

            AppDomain.CurrentDomain.UnhandledException += (sender, args) => {
                Console.WriteLine("------------ handled exceptions" + args.ExceptionObject.GetType());
            };

            

            
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