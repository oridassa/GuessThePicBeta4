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
using Firebase.Database;
using Firebase.Database.Query;
using System.Security.Cryptography.X509Certificates;
using Javax.Security.Auth;
using Java.Lang;
using System.Drawing;
using System.ComponentModel;
using System.Security.AccessControl;
using Android.Animation;

namespace GuessThePicBeta4
{
    [Activity(Label = "GameLobbyHost")]
    public class GameLobbyHost : Activity, View.IOnClickListener
    {
        ListView listView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game_lobby_host);

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
                uploadpictures();

            }
            else if (b.Text == "Start Game")
            {
                //host wants to start the game
                intent = new Intent(this, typeof(GameplayScreen));
                base.StartActivity(intent);
            }
        }
        private async void uploadpictures()
        {
            var image = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
            Toast.MakeText(this, "entered function", ToastLength.Long).Show();
            FirebaseClient firebase = new FirebaseClient(
                    "https://guess-the-pic-a861a-default-rtdb.europe-west1.firebasedatabase.app/");
            //await firebase
            //    .Child("message")
            //    .PutAsync<string>("hello");//changes the value of the child "message"

            var result = await firebase
                .Child("Games").PostAsync<string>("hello"); // code that posts the string "hello" in Games json with a random key **remind myself to check how to give it a custom key!!!!
                
            Toast.MakeText(this, "passed upload", ToastLength.Long).Show();
            //string message = await firebase.Child("message").OnceSingleAsync<string>();
            //Toast.MakeText(this, message, ToastLength.Long).Show();
        }
    }
}