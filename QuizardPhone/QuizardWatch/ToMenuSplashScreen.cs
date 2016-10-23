using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Android.Gms.Common.Apis;
using Android.Gms.Wearable;
using Android.Gms.Common;
using Android.Support.V4.Content;

using Newtonsoft.Json;

namespace Quizard
{
    [Activity(Label = "Quizard"/*"ToMenuSplashScreen"*/, MainLauncher = true, Icon = "@drawable/splashscreen512")]
    public class ToMenuSplashScreen : Activity
    {
        private int countDown = 0;
        private Timer timer;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.SplashScreen);


            countDown = 3;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += OnTimedEvent;
            timer.Start();



        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            countDown--;

            if (countDown <= 0)
            {
                Intent intent = new Intent(this, typeof(SetPage));

                this.StartActivity(intent);

                timer.Stop();
            }
        }


    }
}