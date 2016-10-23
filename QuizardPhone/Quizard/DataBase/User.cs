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
    class User
    {
        private string Username;
        private string Password;

        public User()
        {

        }

        // Overloaded class constructor
        public User(string m_Username, string m_Password)
        {
            Username = m_Username;
            Password = m_Password;
        }

        #region Accessors & Mutators
        public string GetUsername()
        {
            return Username;
        }
        public string GetPassword()
        {
            return Password;
        }
        public void SetUsername(string m_Username)
        {
            Username = m_Username;
        }
        public void SetPassword(string m_Password)
        {
            Password = m_Password;
        }
        #endregion
    }
}