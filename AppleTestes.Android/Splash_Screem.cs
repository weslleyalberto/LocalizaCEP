using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleTestes.Droid
{
    [Activity(Theme ="@style/MyTheme.Splash",MainLauncher = true,NoHistory =true)]
    public class Splash_Screem : Activity
    {
        public override void OnCreate(Bundle savedInstanceState,PersistableBundle persistableBundle)
        {
            base.OnCreate(savedInstanceState,persistableBundle);

            // Create your application here
        }
        protected override void OnResume()
        {
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
            base.OnResume();
        }
        public override void OnBackPressed()
        {
            
        }
        async void SimulateStartup()
        {
            await Task.Delay(10);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}