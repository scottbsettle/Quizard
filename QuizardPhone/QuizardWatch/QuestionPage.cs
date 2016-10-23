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
    [Activity(Label = "QuestionPage")]
    public class QuestionPage : Activity
    {
        private new TextView Title;
        private TextView Question;
        private ImageButton nextPageButton;
        DataStruct data;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your application here
            SetContentView(Resource.Layout.QuestionFlashCard);

            //Aquireing Objects from the QuestionFlashCard Layout
            Title = FindViewById<TextView>(Resource.Id.QuestionTitle);
            Question = FindViewById<TextView>(Resource.Id.QuestionTextBox);           
            nextPageButton = FindViewById<ImageButton>(Resource.Id.QuestionNext);

            //Option A getting data individually
            Title.Text = "Question: " + Intent.GetIntExtra("Question Number", 0).ToString();
            Question.Text = Intent.GetStringExtra("Name Of Set") + " Question?";
            //Option B getting an entire serialized Object
            data = JsonConvert.DeserializeObject<DataStruct>(Intent.GetStringExtra("data"));
            //Setting up a Click event for the next Button Page
            nextPageButton.Click += nextPageButton_Click;        

        }
        //Transition to Question Page
        private void nextPageButton_Click(object sender, EventArgs e)
        {
            //Sets Up an Intent For the Next Activity
            Intent intent = new Intent(this, typeof(AnswerPage));

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
    }
}