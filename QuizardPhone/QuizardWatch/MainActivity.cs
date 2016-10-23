using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.Wearable.Views;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Java.Interop;
using Android.Views.Animations;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Quizard
{
    [Activity(Label = "QuizardWatch", MainLauncher = false, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private ListView QuizList;
        //Temporary List of Dummy data
        private ObservableCollection<string> QuizListData = new ObservableCollection<string>();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Set our view from the "main" layout resource
            SetContentView(Resource.Layout.CardSets);

            QuizListData.Add("History");
            QuizListData.Add("Mathematics");
            QuizListData.Add("Literature");

            QuizList = FindViewById<ListView>(Resource.Id.QuizList);

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, QuizListData);

            QuizList.Adapter = adapter;

        }
    }
}


