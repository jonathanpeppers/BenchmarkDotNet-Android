using Android.App;
using Android.OS;
using System;
using System.Text.RegularExpressions;

namespace DotNetRunner
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _ = DateTimeOffset.Now;
            _ = new Regex(@"([^;=]+)=([^;]+)(?:;\s*)?", RegexOptions.Compiled);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }
    }
}