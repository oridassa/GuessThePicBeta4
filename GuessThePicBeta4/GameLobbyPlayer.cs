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
    [Activity(Label = "GameLobbyPlayer")]
    public class GameLobbyPlayer : Activity, View.IOnClickListener
    {
        ListView listView;
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