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

            AppDomain.CurrentDomain.UnhandledException += (sender, args) => {
                
            };

            

            
            global::Xamarin.Forms.Forms.Init(this, bundle);
            
            LoadApplication(new App());
           
        }
    }
    
    
}