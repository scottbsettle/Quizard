using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace Quizard
{
    [Activity(Label = "ResultPage")]
    public class ResultPage : Activity
    {
        private TextView Average;
        private ImageView Emoji;
        private ImageButton AcceptButton;
        private ImageButton CancelButton;
        private DataStruct data;
        private int avg;

        protected override void OnCreate(Bundle savedInstanceState)
        {  
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.ResultsFlashCard);

            data = JsonConvert.DeserializeObject<DataStruct>(Intent.GetStringExtra("data"));

            Average = FindViewById<TextView>(Resource.Id.ResultPercentage);
            Emoji = FindViewById<ImageView>(Resource.Id.ResultEmoji);
            AcceptButton = FindViewById<ImageButton>(Resource.Id.ResultAcceptButton);
            CancelButton = FindViewById<ImageButton>(Resource.Id.ResultCancelButton);

            avg = (data.Correct * 100) / data.Count /*data.Questions.Count*/;
            Average.Text = avg.ToString();
            Average.Text += "%";

            SetEmoji(avg);
           
            AcceptButton.Click += AcceptButton_Click;
            CancelButton.Click += CancelButton_Click;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            data.Count = 1;

            //Sets Up an Intent For the Next Activity
            Intent intent = new Intent(this, typeof(ToMenuSplashScreen));
            intent.SetFlags(ActivityFlags.ClearTop);

            //Option A Serializing Individual basic type data sets
            intent.PutExtra("Name Of Set", data.NameOfSet);
            intent.PutExtra("Question Number", data.Count);
            //Option B Serializing an Object using Json
            intent.PutExtra("data", JsonConvert.SerializeObject(data));

            //Starts the Next Activity
            this.StartActivity(intent);
            //Closes this Activity
            this.Finish();
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            data.Count = 1;
            data.Correct = 0;

            //Sets Up an Intent For the Next Activity
            Intent intent = new Intent(this, typeof(QuestionPage));
            intent.SetFlags(ActivityFlags.ClearTop);

            //Option A Serializing Individual basic type data sets
            intent.PutExtra("Name Of Set", data.NameOfSet);
            intent.PutExtra("Question Number", data.Count);
            //Option B Serializing an Object using Json
            intent.PutExtra("data", JsonConvert.SerializeObject(data));

            //Starts the Next Activity
            this.StartActivity(intent);
            //Closes this Activity
            this.Finish();
        }

        private void RandomEmoji()
        {
            Random m_random = new Random();
            int select = m_random.Next(1, 7);

            switch (select)
            {
                case 1:
                    Emoji.SetImageResource(Resource.Drawable.WN1);
                    break;
                case 2:
                    Emoji.SetImageResource(Resource.Drawable.WN2);
                    break;
                case 3:
                    Emoji.SetImageResource(Resource.Drawable.WN3);
                    break;
                case 4:
                    Emoji.SetImageResource(Resource.Drawable.WN4);
                    break;
                case 5:
                    Emoji.SetImageResource(Resource.Drawable.WN5);
                    break;
                case 6:
                    Emoji.SetImageResource(Resource.Drawable.WN6);
                    break;
                case 7:
                    Emoji.SetImageResource(Resource.Drawable.WN7);
                    break;
                default:
                    break;
            }

        }

        private void SetEmoji(int avg)
        {
            if (avg == 0)
                Emoji.SetImageResource(Resource.Drawable.ZEROP);
            else if (avg < 40)
                Emoji.SetImageResource(Resource.Drawable.Low40P);
            else if (avg < 50)
                Emoji.SetImageResource(Resource.Drawable.Low50P);
            else if (avg == 50)
                Emoji.SetImageResource(Resource.Drawable.FIFTYP);
            else if (avg < 80)
                Emoji.SetImageResource(Resource.Drawable.Low80P);
            else if (avg < 100)
                Emoji.SetImageResource(Resource.Drawable.High80P);
            else
                RandomEmoji();
        }
    }
}