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

namespace Quizard.DataBase
{
    class DBHelper : SQLiteOpenHelper
    {
        public DBHelper(Context context) : base(context, Constants.DB_Name, null, Constants.DB_Version)
        {

        }
        public override void OnCreate(SQLiteDatabase db)
        {
            try
            {
                db.ExecSQL(Constants.Create_Users_tb);
                db.ExecSQL(Constants.Create_Sets_tb);
                db.ExecSQL(Constants.Create_Cards_tb);
                db.ExecSQL(Constants.Create_RemeberMe_tb);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL(Constants.Drop_Users_tb);
            db.ExecSQL(Constants.Drop_Sets_tb);
            db.ExecSQL(Constants.Drop_Cards_tb);
            db.ExecSQL(Constants.Drop_RemeberMe_tb);
            OnCreate(db);
        }
       

    }
}