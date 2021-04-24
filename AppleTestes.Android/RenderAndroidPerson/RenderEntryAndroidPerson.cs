using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppleTestes.Droid.RenderAndroidPerson;
using AppleTestes.RenderPerson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(EntryPerson), typeof(RenderEntryAndroidPerson))]
namespace AppleTestes.Droid.RenderAndroidPerson
{
    public class RenderEntryAndroidPerson : EntryRenderer
    {
        public RenderEntryAndroidPerson(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.White);
            }
        }

    }
}