using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Database.Sqlite;
using Android.Database;
using Quizard.DeckInterface;

namespace Quizard
{
    [Activity(Label = "DeckActivity", 
    ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class DeckActivity : Activity
    {
        List<string> mQuestions, mAnswers, mQuizAnswers;
        DataBase.UserInfo UserInformation = new DataBase.UserInfo();
        //public DeckActivity(List<string> _questions, List<string> _answers)
        //{
        //    questions = new List<string>();
        //    questions = _questions;
        //    answers = new List<string>();
        //    answers = _answers;
        //}

        // Initializes list, and creates action bar
        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);

            mQuestions = new List<string>();
            mAnswers = new List<string>();
            mQuizAnswers = new List<string>();

            // Set our view from the "DeckLayout" layout resource
            SetContentView(Resource.Layout.DeckLayout);
            

            string[] UserSetname_Buffer = Intent.GetStringArrayExtra("Username/SetName");
            UserInformation.GetUser().SetUsername(UserSetname_Buffer[0]);
            string mSetName = UserSetname_Buffer[1];

            // creates action bar, and Card and Quiz tabs to Deck homepage
            this.ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            AddTab("CARDS", Resource.Drawable.cardIconSmall, new DeckCardTabFragment(mQuestions, mAnswers, UserSetname_Buffer, this));
            AddTab("QUIZ", Resource.Drawable.quizIcon, new DeckQuizTabFragment(mQuestions, mQuizAnswers, this, UserSetname_Buffer));

            ActionBar.SetDisplayShowTitleEnabled(false);
            ActionBar.SetDisplayShowHomeEnabled(false);

            if (bundle != null)
                this.ActionBar.SelectTab(this.ActionBar.GetTabAt(bundle.GetInt("tab")));
        }

        // Adds tab to action bar
        void AddTab(string tabText, int iconResourceId, Fragment view)
        {
            var tab = this.ActionBar.NewTab();

            tab.SetText(tabText);
            tab.SetIcon(iconResourceId);

            // must set event handler before adding tab
            tab.TabSelected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);

                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);

                e.FragmentTransaction.Add(Resource.Id.fragmentContainer, view);
            };

            tab.TabUnselected += delegate (object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(view);
            };

            this.ActionBar.AddTab(tab);
        }
    }


    
}