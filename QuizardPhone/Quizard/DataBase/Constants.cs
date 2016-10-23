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
    class Constants
    {
        //Columns
        public static String ROW_ID = "id";
        public static String Name = "name";

        //Users Columns
        public static String Users_Email = "Email";
        public static String Users_UserName = "UserName";
        public static String Users_Password = "Password";
        public static String Users_FirstName = "FirstName";
        public static String Users_LastName = "LastName";

        // Sets Columns
        public static String Sets_UserName = "UserName";
        public static String Sets_SetName = "SetName";
        public static String Sets_Notify = "Notify";
        public static String Sets_Correct = "Correct";
        public static String Sets_Incorrect = "Incorrect";

        //Card Columns
        public static String Cards_UserName = "UserName";
        public static String Cards_SetName = "SetName";
        public static String Cards_Question = "Question";
        public static String Cards_Answer = "Answer";
        public static String Cards_NumberBox = "Number_Box";
        public static String Cards_PreRun = "PreRun";

        // Database and table Names version
        public static String DB_Name = "Quizard_db";
        public static String Users_TB_Name = "Users_tb";
        public static String Sets_TB_Name = "Sets_tb";
        public static String Cards_TB_Name = "Cards_tb";
        public static String RememberMe_TB_Name = "RememberMe_tb";
        public static int DB_Version = 1;

        //Remeber Me
        public static String RememberMe_Username = "UserName";
        public static String RememberMe_Password = "Password";

        // Creating Quizard Tables & User Tables
        public static String Create_Users_tb = "create table Users_tb(" +
                                      " UserName Text NOT NULL UNIQUE," +
                                               "Password Text NOT NULL" + ")";
        // Create Sets Tables 
        public static String Create_Sets_tb = "create table Sets_tb (" +
                                             "UserName Text NOT NULL," +
                                             " SetName Text NOT NULL," +
                                           " Notify INTEGER NOT NULL," +
                                          " Correct INTEGER NOT NULL," +
                                          " Incorrect INTEGER NOT NULL)";
        // Create Cards table             
        public static String Create_Cards_tb = "create table Cards_tb (" +
                                               "UserName Text NOT NULL," +
                                                "SetName Text NOT NULL," +
                                               "Question Text NOT NULL," +
                                                 "Answer Text NOT NULL," +
                                          "Number_Box integer NOT NULL," +
                                           "PreRun integer NOT NULL)";

        public static String Create_RemeberMe_tb = "create table RememberMe_tb(" +
                                              "UserName Text NOT NULL UNIQUE," +
                                               "Password Text NOT NULL" + ")";
        // Drop Quizard Table
        public static String Drop_Users_tb = "drop table if exists " + Users_TB_Name;
        public static String Drop_Sets_tb = "drop table if exists " + Sets_TB_Name;
        public static String Drop_Cards_tb = "drop table if exists " + Cards_TB_Name;
        public static String Drop_RemeberMe_tb = "drop table if exists " + RememberMe_TB_Name;
    }
}