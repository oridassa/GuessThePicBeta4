using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessThePicBeta4
{
    [Activity(Label = "GameLobbyPlayer")]
    public class GameLobbyPlayer : Activity, View.IOnClickListener
    {
        ListView listView;

        private PictureTransport pt;


        // for the class diagram
        private FirebaseActions fbactions;
        private FirebaseClient firebase = new FirebaseClient(
       "https://guess-the-pic-a861a-default-rtdb.europe-west1.firebasedatabase.app/");
        //ListView listView;
        TextView gameidview;
        private string gameid;
        Player player;
        IDisposable subscription;
        //
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game_lobby_player);

            string[] array = { "ori", "amos", "mori", "noam" };

            listView = FindViewById<ListView>(Resource.Id.list123);

            ArrayAdapter<string> adapter = new ArrayAdapter<string>
                (this, Android.Resource.Layout.SimpleListItem1, array);
            listView.Adapter = adapter;
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
            else if (b.Text == "Insert photos")
            {
                //player pressed "Insert photos"
            }

        }
    }
}