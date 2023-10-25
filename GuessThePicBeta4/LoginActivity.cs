using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessThePicBeta4
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity, View.IOnClickListener
    {
        private EditText gameid;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_screen);

            this.gameid = FindViewById<EditText>(Resource.Id.gameid);
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
                //joining a game
                //need to check if the gameid is ok and active
            }
        }
    }
}