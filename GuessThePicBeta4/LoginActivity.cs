using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessThePicBeta4
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity, View.IOnClickListener
    {
        private EditText gameidinput;
        private FirebaseClient firebase = new FirebaseClient(
            "https://guess-the-pic-a861a-default-rtdb.europe-west1.firebasedatabase.app/");
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_screen);

            this.gameidinput = FindViewById<EditText>(Resource.Id.gameid);
        }
        public void OnClick(View v)
        {
            Intent intent;
            Button b = (Button)v;
            if (b.Text == "Quit to main menu")
            {
                intent = new Intent(this, typeof(MainActivity));
                base.StartActivity(intent);
            }
            else if (b.Text == "Join Game")
            {
                intent = new Intent(this, typeof(GameLobbyPlayer));
                base.StartActivity(intent);
            }
        }
    }
}