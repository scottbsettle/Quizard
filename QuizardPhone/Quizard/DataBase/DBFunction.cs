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
using Android.Database.Sqlite;
using Android.Database;

namespace Quizard.DataBase
{
    class DBFunction
    {
        /*
       * This will add a User to the User database Table 
       * Pass the Username, Password of the new user
       * for Context pass this 
       */
        public bool SaveUser(String Username, String Password, Context c)
        {

            DBAdapter db = new DBAdapter(c);
            db.openDB();

            if (db.AddUser(Username, Password))
            {
                db.CloseDB();
                return true;
            }

            db.CloseDB();
            return false;
        }
        public bool SaveSet(string Username, string SetName, Context c)
        {
            DBAdapter db = new DBAdapter(c);
            DataBase.Sets Set_buffer = new DataBase.Sets(Username, SetName, "", "", "");
            db.openDB();

            if (db.SaveSet(Set_buffer))
            {
                db.CloseDB();
                return true;
            }

            db.CloseDB();
            return false;
        }
        /*
         * The Retrieve User Function retrieves a user with 
         * the specific username and password of the user 
         * it returns a ICursor that holds the column info
         * Pass in The username and password and this for the Context
         */
        public ICursor Retrieve_User(String Username, String Password, Context c)
        {
            DBAdapter db = new DBAdapter(c);
            db.openDB();
            ICursor cur = db.GetUser(Username, Password);
            db.CloseDB();
            return cur;
        }
    }
}