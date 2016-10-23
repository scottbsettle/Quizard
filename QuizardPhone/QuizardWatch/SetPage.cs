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
using System.Collections.ObjectModel;
using Android.Support.V4.Content;

namespace Quizard
{
    [Activity(Label = "SetPage"/*, MainLauncher = false, Icon = "@drawable/icon"*/)]
    public class SetPage : Activity
    {
        private ListView QuizList;
        //Temporary List of Dummy data
        private ObservableCollection<string> QuizListData;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.CardSets);
            QuizListData = new ObservableCollection<string>();

            QuizList = FindViewById<ListView>(Resource.Id.QuizList);

            QuizList.ItemClick += QuizList_ItemClick;

            IntentFilter filter = new IntentFilter(Intent.ActionSend);
            MessageReciever receiver = new MessageReciever(this);
            LocalBroadcastManager.GetInstance(this).RegisterReceiver(receiver, filter);
        }

        private void QuizList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //Test for getting data of list
            string SelectedSet = QuizListData[e.Position];
            //Sets Up an Intent For the Next Activity
            Intent intent = new Intent(this, typeof(QuestionPage));

            //Setting up data to be transfered between pages, this will be based on the SelectedSet
            DataStruct data = new DataStruct()
            {
                Answers = new List<string>(),//List Of Answers Obtained from the phone
                Questions = new List<string>(),//List Of Questios Obtained from the phone
                NameOfSet = SelectedSet,
                Count = 1,
                Correct = 0,
            };
            //Option A Serializing Individual basic type data sets
            intent.PutExtra("Name Of Set", SelectedSet);
            intent.PutExtra("Question Number", 1);
            //Option B Serializing an Object using Json
            intent.PutExtra("data", JsonConvert.SerializeObject(data));
            //Starts the Next Activity
            StartActivity(intent);
        }

        public void ProcessMessage(Intent intent)
        {
            JavaList<string> data = JsonConvert.DeserializeObject<JavaList<string>>(intent.GetStringExtra("WearMessage"));
            QuizListData.Clear();

            for (int i = 0; i < data.Count; i++)
            {
                QuizListData.Add(data.ElementAt(i));
            }
            

            QuizList.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleExpandableListItem1, QuizListData);
        }

        internal class MessageReciever : BroadcastReceiver
        {
            SetPage _main;
            public MessageReciever(SetPage owner) { this._main = owner; }
            public override void OnReceive(Context context, Intent intent)
            {
                _main.ProcessMessage(intent);
            }
        }
    }
}