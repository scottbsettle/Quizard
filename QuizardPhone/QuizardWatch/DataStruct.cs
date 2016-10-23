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



namespace Quizard
{
    class DataStruct
    {
        public List<string> Answers { get; set; }
        public List<string> Questions { get; set; }
        public string NameOfSet { get; set; }
        public int Count { get; set; }
        public int Correct { get; set; }
    }
}