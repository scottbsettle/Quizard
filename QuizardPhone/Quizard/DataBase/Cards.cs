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
    class Cards
    {
        private string UserName;
        private string SetName;
        private string Question;
        private string Answer;
        private string NumberBox;
        private string PreRun;

        public Cards()
        {

        }

        // Overloaded class constructor
        public Cards(string m_Username, string m_SetName, string m_Question, string m_Answer, string m_NumberBox, string m_PreRun)
        {
            UserName = m_Username;
            SetName = m_SetName;
            Question = m_Question;
            Answer = m_Answer;
            NumberBox = m_NumberBox;
            PreRun = m_PreRun;
        }

        #region Accessors & Mutators
        public string GetUserName()
        {
            return UserName;
        }
        public string GetSetName()
        {
            return SetName;
        }
        public string GetQuestion()
        {
            return Question;
        }
        public string GetAnswer()
        {
            return Answer;
        }
        public string GetNumberBox()
        {
            return NumberBox;
        }
        public string GetPreRun()
        {
            return PreRun;
        }
        public void SetUserName(string m_Username)
        {
            UserName = m_Username;
        }
        public void SetSetName(string m_SetName)
        {
            SetName = m_SetName;
        }
        public void SetQuestion(string m_Question)
        {
            Question = m_Question;
        }
        public void SetAnswer(string m_Answer)
        {
            Answer = m_Answer;
        }
        public void SetNumberBox(string m_NumberBox)
        {
            NumberBox = m_NumberBox;
        }
        public void SetPreRun(string m_PreRun)
        {
            PreRun = m_PreRun;
        }
        #endregion
    }
}