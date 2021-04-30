using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Firebase.Auth;
using static Android.Views.View;

namespace XamarinFirebaseAuth
{
    [Activity(Label = "SignUp", Theme ="@style/AppTheme")]
    public class SignUp : Activity, IOnClickListener, IOnCompleteListener
    {
        Button btnSignup;
        TextView btnLogin, btnForgetPass;
        EditText input_email, input_password;
        RelativeLayout activity_sign_up;

        FirebaseAuth auth;

        public void OnClick(View v)
        {
            if(v.Id == Resource.Id.signup_btn_login)
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
                Finish();
            }
            else
            if (v.Id == Resource.Id.signup_btn_forget_password)
            {
                StartActivity(new Intent(this, typeof(ForgetPassword)));
                Finish();
            }
            else
            if (v.Id == Resource.Id.signup_btn_register)
            {
                SignUpUser(input_email.Text, input_password.Text); 
            }
                
        }

        private void SignUpUser(string email, string password)
        {
            auth.CreateUserWithEmailAndPassword(email, password).AddOnCompleteListener(this, this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Crear app
            SetContentView(Resource.Layout.SignUp);

            //Iniciar Firebase
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            //Vistas
            btnSignup = FindViewById<Button>(Resource.Id.signup_btn_register);
            btnLogin = FindViewById<TextView>(Resource.Id.signup_btn_login);
            btnForgetPass = FindViewById<TextView>(Resource.Id.signup_btn_forget_password);
            input_email = FindViewById<EditText>(Resource.Id.signup_email);
            input_password = FindViewById<EditText>(Resource.Id.signup_password);
            activity_sign_up = FindViewById<RelativeLayout>(Resource.Id.activity_sign_up);

            btnLogin.SetOnClickListener(this);
            btnSignup.SetOnClickListener(this);
            btnForgetPass.SetOnClickListener(this);

        }

        public void OnComplete(Task task)
        {
            if(task.IsSuccessful == true)
            {
                Snackbar snackbar = Snackbar.Make(activity_sign_up, "Registro Exitoso ", Snackbar.LengthShort);
                snackbar.Show();
            }
            else
            {
                Snackbar snackbar = Snackbar.Make(activity_sign_up, "Register Fallido ", Snackbar.LengthShort);
                snackbar.Show();
            }
        }
    }
}