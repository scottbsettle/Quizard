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

namespace Quizard.DataBase
{
    class Sets
    {
        private string UserName;
        private string SetName;
        private string Notify;
        private string Correct;
        private string Incorrect;

        public Sets()
        {

        }

        // Overloaded class constructor
        public Sets(string m_Username, string m_SetName, string m_Notify, string m_Correct, string m_Incorrect)
        {
            UserName = m_Username;
            SetName = m_SetName;
            Notify = m_Notify;
            Correct = m_Correct;
            Incorrect = m_Incorrect;
        }

        #region Accessors & Mutators
        public string GetUsername()
        {
            return UserName;
        }
        public string GetSetName()
        {
            return SetName;
        }
        public string GetNotify()
        {
            return Notify;
        }
        public string GetCorrect()
        {
            return Correct;
        }
        public string GetIncorrect()
        {
            return Incorrect;
        }

        public void SetUsername(string m_Username)
        {
            UserName = m_Username;
        }
        public void SetSetName(string m_SetName)
        {
            SetName = m_SetName;
        }
        public void SetNotify(string m_Notify)
        {
            Notify = m_Notify;
        }
        public void SetCorrect(string m_Correct)
        {
            Correct = m_Correct;
        }
        public void SetIncorrect(string m_Incorrect)
        {
            Incorrect = m_Incorrect;
        }
        #endregion
    }
}