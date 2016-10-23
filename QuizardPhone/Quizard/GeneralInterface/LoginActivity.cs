using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace Quizard
{
    public class OnCreateAnAccountEventArgs : EventArgs
    {
        private string mEventNewUsername, mEventNewPassword, mEventNewConfirmPassword;

        public string newUsername
        {
            get { return mEventNewUsername; }
            set { mEventNewUsername = value; }
        }

        public string newPassword
        {
            get { return mEventNewPassword; }
            set { mEventNewPassword = value; }
        }

        public string newConfirmPassword
        {
            get { return mEventNewConfirmPassword; }
            set { mEventNewConfirmPassword = value; }
        }

        public OnCreateAnAccountEventArgs(string eventNewUsername, string eventNewPassword, string eventNewConfirmPassword)
        {
            newUsername = eventNewUsername;
            newPassword = eventNewPassword;
            newConfirmPassword = eventNewConfirmPassword;
        }
    }

    public class CreateAnAccountDialogFragment : DialogFragment
    {
        private EditText mNewUserUsername, mNewUserPassword, mNewUserConfirmPassword;
        private Button mCreateNewAccount;
        public event EventHandler<OnCreateAnAccountEventArgs> mOnCreateAnAccountIsClicked;

        #region Database Accessors & Mutators
        public String GetNewUserName()
        {
            return mNewUserUsername.Text;
        }

        public String GetNewPassword()
        {
            return mNewUserPassword.Text;
        }

        public void SetNewUserName(string Username)
        {
            mNewUserUsername.Text = Username;
        }

        public void SetNewPassword(string Password)
        {
            mNewUserPassword.Text = Password;
        }

        public void SetNewConfermPassword(string ConPassword)
        {
            mNewUserConfirmPassword.Text = ConPassword;
        }
        public string GetConfrimPassword()
        {
            return mNewUserConfirmPassword.Text;
        }
        #endregion

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.CreateAnAccountDialogLayout, container, false);

            #region Class Variable FindViewById<> Assignments
            mNewUserUsername = view.FindViewById<EditText>(Resource.Id.newUserUsernameEditTextID);
            mNewUserPassword = view.FindViewById<EditText>(Resource.Id.newUserPasswordEditTextID);
            mNewUserConfirmPassword = view.FindViewById<EditText>(Resource.Id.newUserConfirmPasswordEditTextID);
            mCreateNewAccount = view.FindViewById<Button>(Resource.Id.createNewAccountButtonID);
            #endregion

            mCreateNewAccount.Click += delegate (object sender, EventArgs e)
            {
                mOnCreateAnAccountIsClicked.Invoke(this, new OnCreateAnAccountEventArgs(mNewUserUsername.Text, mNewUserPassword.Text, mNewUserConfirmPassword.Text));
                this.Dismiss();
            };

            return view;
        }
    }

    [Activity(MainLauncher = true /* MainLauncher does NOT need to be changed unless another layout or diaglog fragment needs to be tested*/)]
    public class LoginActivity : Activity
    {
        private LinearLayout mLoginView;
        private EditText mUserLoginUsername, mUserLoginPassword;
        private Button mLogin, mCreateAnAccount;
        private CheckBox mRememberMe;
        private ProgressBar mLoginProgressBar;
        private CreateAnAccountDialogFragment mFragment = new CreateAnAccountDialogFragment();
        private string[] mRemembermeInfo;

        #region Database Variables
        private string NewUsername, NewPassword, NewConfirmPassword;
        private DataBase.UserInfo UserInformation = new DataBase.UserInfo();
        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Window.RequestFeature(WindowFeatures.NoTitle);

            base.OnCreate(savedInstanceState);

            // Set our view from the "LoginLayout" layout resource
            SetContentView(Resource.Layout.LoginLayout);

            #region Class Variable FindViewById<> Assignments
            mLoginView = FindViewById<LinearLayout>(Resource.Id.loginViewLinearLayoutID);
            mUserLoginUsername = FindViewById<EditText>(Resource.Id.userLoginUsernameEditTextID);
            mUserLoginPassword = FindViewById<EditText>(Resource.Id.userLoginPasswordEditTextID);
            mLogin = FindViewById<Button>(Resource.Id.loginButtonID);
            mCreateAnAccount = FindViewById<Button>(Resource.Id.createAnAccountButtonID);
            mRememberMe = FindViewById<CheckBox>(Resource.Id.rememberMeCheckBoxID);
            mLoginProgressBar = FindViewById<ProgressBar>(Resource.Id.loginProgressBarID);
            #endregion

            mRemembermeInfo = GetRememberMe();

            if (!mRemembermeInfo[0].Contains("{..Failed..}"))
            {
                mUserLoginUsername.Text = mRemembermeInfo[0];
                mUserLoginPassword.Text = mRemembermeInfo[1];
                mRememberMe.Checked = true;
            }

            // If the "CreateAnAccount" dialog fragment is brought up by accident, the user may click the
            // layout around the dialog fragment to close it and bring them back to the main login layout
            mLoginView.Click += delegate (object sender, EventArgs e)
            {
                InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Activity.InputMethodService);
                inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.None);
            };

            mLogin.Click += delegate (object sender, EventArgs e)
            {
                mLoginProgressBar.Visibility = ViewStates.Visible;

                Thread progressBarThread = new Thread(ServerRequest);
                progressBarThread.Start();
                UserLogin();
            };

            mCreateAnAccount.Click += delegate (object sender, EventArgs e)
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();

                // When the "New to Quizard?" button is clicked, bring up the assigned dialog fragment
                mFragment.Show(transaction, "dialog fragment");

                mFragment.mOnCreateAnAccountIsClicked += CreateAnAccount_mOnCreateAnAccountIsClicked;
            };
        }

        private void CreateAnAccount_mOnCreateAnAccountIsClicked(object sender, OnCreateAnAccountEventArgs e)
        {
            mLoginProgressBar.Visibility = ViewStates.Visible;

            Thread progressBarThread = new Thread(ServerRequest);
            progressBarThread.Start();

            #region Database Create An Account Error Checking
            NewUsername = mFragment.GetNewUserName();
            NewPassword = mFragment.GetNewPassword();
            NewConfirmPassword = mFragment.GetConfrimPassword();
            CreateUser();
            mFragment.SetNewUserName("");
            mFragment.SetNewPassword("");
            mFragment.SetNewConfermPassword("");
            #endregion
        }

        private void ServerRequest()
        {
            // This code is temporary until a server can be implemented
            Thread.Sleep(3000);

            RunOnUiThread(() =>
            {
                mLoginProgressBar.Visibility = ViewStates.Invisible;
            });
        }

        #region DataBase Funtions
        // Once the user clicks create account this will check to make sure
        // Username and password are grater than 0
        // Username is does not already exist
        // attempt to add the user to the database
        private void CreateUser()
        {
            try
            {
                if (NewUsername.Length >= 4 && NewPassword.Length >= 6 && NewConfirmPassword.Length >= 6 && NewPassword == NewConfirmPassword
                   && NewPassword.Contains("0") || NewPassword.Contains("1") || NewPassword.Contains("2")
                   || NewPassword.Contains("3") || NewPassword.Contains("4") || NewPassword.Contains("5")
                   || NewPassword.Contains("6") || NewPassword.Contains("7") || NewPassword.Contains("8")
                   || NewPassword.Contains("9") && !NewUsername.Contains(" "))
                {
                    if (NewPassword == NewConfirmPassword)
                    {
                        DataBase.DBAdapter db = new DataBase.DBAdapter(this);
                        db.openDB();

                        ICursor UserInfo = db.GetUser(NewUsername, "");

                        if (UserInfo.Count == 0)
                        {
                            DataBase.DBFunction DataFunc = new DataBase.DBFunction();

                            if (DataFunc.SaveUser(NewUsername, NewPassword, this))
                            {
                                DataBase.User NewUser = new DataBase.User(NewUsername, NewPassword);

                                UserInformation.SetUser(NewUser);

                                Toast.MakeText(this, "Welcome to Quizard!", ToastLength.Short).Show();

                                // Once the user has clicked the "New to Quizard?" button, take them to the home screen
                                Intent intent = new Intent(this, typeof(HomeActivity));
                                intent.PutExtra("UserName", NewUsername);
                                this.StartActivity(intent);
                            }
                            else
                                throw new System.ArgumentException("Failed to save new username", "SaveUser");
                        }
                        else
                            throw new System.ArgumentException("UserInfo is Size 0", "UserInfo");
                    }
                }
                else
                    throw new System.ArgumentException("Username or Password is blank", "Username/Password");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Toast.MakeText(this, "Unable to create a new user", ToastLength.Short).Show();
            }
        }

        private void UserLogin()
        {
            // This try catch will check to make sure
            // Username and password are greater than 0
            // & that the user actually exists in the database
            try
            {
                if (mUserLoginUsername.Length() > 0 && mUserLoginPassword.Length() > 0)
                {
                    DataBase.DBAdapter db = new DataBase.DBAdapter(this);
                    db.openDB();

                    ICursor LoginInfo = db.GetUser(mUserLoginUsername.Text, mUserLoginPassword.Text);
                    LoginInfo.MoveToNext();

                    if (mUserLoginUsername.Text == LoginInfo.GetString(0) && mUserLoginPassword.Text == LoginInfo.GetString(1))
                    {
                        DataBase.User NewUser = new DataBase.User(mUserLoginUsername.Text, mUserLoginPassword.Text);
                        UserInformation.SetUser(NewUser);

                        Toast.MakeText(this, "Welcome to Quizard!", ToastLength.Short).Show();

                        // Once the user has clicked the "Login" button, take them to the home screen
                        Intent intent = new Intent(this, typeof(HomeActivity));
                        intent.PutExtra("UserName", mUserLoginUsername.Text);

                        if (mRememberMe.Checked)
                        {
                            if (!RememberMeSaveUser(mUserLoginUsername.Text, mUserLoginPassword.Text))
                                Toast.MakeText(this, "Failed to Save RemeberMe", ToastLength.Short).Show();
                        }
                        else
                        {
                            db.DeleteRememberMe();
                        }

                        mUserLoginUsername.Text = "";
                        mUserLoginPassword.Text = "";

                        this.StartActivity(intent);
                    }
                    else
                        throw new System.ArgumentException("Username or Password is incorrect", "Username/Password");

                    db.CloseDB();
                }
                else
                    throw new System.ArgumentException("Username or Password is blank", "Username/Password");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Toast.MakeText(this, "Username or Password is incorrect", ToastLength.Short).Show();
            }
        }

        /*
         * if remeber me is Checked 
         * Saves users info to RemeberMe table 
         */
        private bool RememberMeSaveUser(string _Username, string _Password)
        {
            DataBase.DBAdapter db = new DataBase.DBAdapter(this);
            db.openDB();

            if (db.AddRemeberMe_tb(_Username, _Password))
            {
                db.CloseDB();
                return true;
            }
            else
            {
                db.CloseDB();
                return false;
            }
        }

        /*
         * If user Checked Remeber me
         * Will retrieve last Username and Password 
         * from database
         */
        private string[] GetRememberMe()
        {
            DataBase.DBAdapter db = new DataBase.DBAdapter(this);
            db.openDB();

            ICursor RemeberInfo;
            RemeberInfo = db.GetRemeberMe();

            if (RemeberInfo.MoveToNext())
            {
                string[] infoBuffer = { RemeberInfo.GetString(0), RemeberInfo.GetString(1) };
                return infoBuffer;
            }
            else
            {
                string[] noto = { "{..Failed..}" };
                return noto;
            }
        }
        #endregion
    }
}