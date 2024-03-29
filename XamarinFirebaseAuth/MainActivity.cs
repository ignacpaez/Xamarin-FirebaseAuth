﻿using Android.App;
using Android.Widget;
using Android.OS;
using Firebase;
using Firebase.Auth;
using System;
using static Android.Views.View;
using Android.Views;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;

namespace XamarinFirebaseAuth
{
    [Activity(Label = "XamarinFirebaseAuth", MainLauncher = true, Theme ="@style/AppTheme")]
    public class MainActivity : Activity, IOnClickListener, IOnCompleteListener
    {
        Button btnLogin;
        EditText input_email, input_password;
        TextView btnSignUp, btnForgetPassword;
        RelativeLayout activity_main;

        public static FirebaseApp app;
        FirebaseAuth auth;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Vista main
            SetContentView(Resource.Layout.Main);

            //Iniciar Auth
            InitFirebaseAuth();

            //Vistas
            btnLogin = FindViewById<Button>(Resource.Id.login_btn_login);
            input_email = FindViewById<EditText>(Resource.Id.login_email);
            input_password = FindViewById<EditText>(Resource.Id.login_password);
            btnSignUp = FindViewById<TextView>(Resource.Id.login_btn_sign_up);
            btnForgetPassword = FindViewById<TextView>(Resource.Id.login_btn_forget_password);
            activity_main = FindViewById<RelativeLayout>(Resource.Id.activity_main);
            btnSignUp.SetOnClickListener(this);
            btnLogin.SetOnClickListener(this);
            btnForgetPassword.SetOnClickListener(this);
        }

        private void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
               .SetApplicationId("AIzaSyDDkjTIE-LQMNCfPOwzR8kX0-IPENxl_xY")
               .SetApiKey("AIzaSyBmuAwrNEgENM40rnjUToHHMraFXQOyuPE") //Token
               .Build();

            if (app == null)
                app = FirebaseApp.InitializeApp(this, options);
            auth = FirebaseAuth.GetInstance(app);
        }

        public void OnClick(View v)
        {
            if(v.Id == Resource.Id.login_btn_forget_password)
            {
                StartActivity(new Android.Content.Intent(this, typeof(ForgetPassword)));
                Finish();
            }
            else
            if(v.Id == Resource.Id.login_btn_sign_up)
            {
                StartActivity(new Android.Content.Intent(this, typeof(SignUp)));
                Finish();
            }
            else
            if (v.Id == Resource.Id.login_btn_login)
            {
                LoginUser(input_email.Text, input_password.Text);
            }
        }

        private void LoginUser(string email ,string password)
        {
            auth.SignInWithEmailAndPassword(email, password).AddOnCompleteListener(this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                StartActivity(new Android.Content.Intent(this, typeof(DashBoard)));
                Finish();
            }
            else
            {
                Snackbar snackbar = Snackbar.Make(activity_main, "Login Fallido", Snackbar.LengthShort);
                snackbar.Show();
            }
        }
    }
  }


