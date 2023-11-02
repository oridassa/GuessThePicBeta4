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
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using System.Security.Cryptography.X509Certificates;
using Javax.Security.Auth;
using Java.Lang;
using System.Drawing;
using System.ComponentModel;
using System.Security.AccessControl;
using Android.Animation;
using System.IO;
using Android.Util;
using static Android.Graphics.ImageDecoder;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Diagnostics.Tracing;
using static AndroidX.RecyclerView.Widget.RecyclerView;
using System.Reactive.Linq;
using System.Reflection;

namespace GuessThePicBeta4
{
    [Activity(Label = "GameLobbyHost")]
    public class GameLobbyHost : Activity, View.IOnClickListener
    {
        private FirebaseClient firebase = new FirebaseClient(
       "https://guess-the-pic-a861a-default-rtdb.europe-west1.firebasedatabase.app/");
        ListView listView;
        TextView gameidview;
        private string gameid;
        Player player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game_lobby_host);

            player = new Player(PlayerProperties.name);

            listView = FindViewById<ListView>(Resource.Id.list123);
            gameidview = FindViewById<TextView>(Resource.Id.gameidview);


            string[] arr = "just,a,test".Split(',');
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
            listView.Adapter = adapter;

            CreateGame();
        }
        public void OnClick(View v)
        {
            Intent intent;
            Button b = (Button)v;
            if (b.Text == "Quit to main menu")
            {
                EndGame();
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
            byte[] bytes;
            string base64str;

            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Upload a picture for the game!"
            });

            var stream = await result.OpenReadAsync();
            var mstream = new MemoryStream();
            stream.CopyTo(mstream);
            bytes = mstream.ToArray(); //block of code that decodes picture of any type to base64
            base64str = Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks);

            await firebase.Child("Games").Child("-Nhq0zFgs8L2MAByvtWe").PutAsync<string>(base64str.ToString());

        }
        public async void EndGame() 
        {
            await firebase.Child("Games").Child(gameid).DeleteAsync();        
        }

        public static async Task<string> GetUnusedSixDigitNumber()
        {
            FirebaseClient firebase = new FirebaseClient(
               "https://guess-the-pic-a861a-default-rtdb.europe-west1.firebasedatabase.app/");
            Random random = new Random();
            string sixDigitNumber = "123666"; //random.Next(100000, 1000000).ToString();
            var data = await firebase
            .Child("games")
            .OnceAsync<Dictionary<string, object>>();
            var childNodes = data.Select(item => item.Key).ToArray();

            while (childNodes.Contains(sixDigitNumber))
            {
                sixDigitNumber = random.Next(100000, 1000000).ToString();
            }

            return sixDigitNumber;
        }

        private async void CreateGameId() //creates a random gameid and creates a json object in the Games section
        {
            Random rand = new Random();
            gameid = $"{rand.Next(100000, 1000000)}";
            //string gameid = await GetUnusedSixDigitNumber();
            gameidview.Text = gameid;
            await firebase.Child("Games").Child(gameid).Child("gamid").PutAsync<string>(gameid);

        }
        public async Task<bool> InitiatePlayers() //create a Player object for host and initiate playersarray with his name
        {
            FirebaseClient firebase = new FirebaseClient(
                "https://guess-the-pic-a861a-default-rtdb.europe-west1.firebasedatabase.app/");
            string playersarray = PlayerProperties.name + ",";
            await firebase.Child("Games").Child(gameid).Child("playersarray").PutAsync<string>(playersarray);
            await firebase.Child("Games").Child(gameid).Child("players").Child(PlayerProperties.name).PutAsync<Player>(player);
            return true;

        }
        public async void UpdatePlayerArray() //updates the Listview that show the players in the lobby
        {
            ArrayAdapter<string> adapter;
            //string playersarray = await firebase.Child("Games").Child(gameid).Child("playersarray")
            //    .OnceSingleAsync<string>();
            //string[] arr = playersarray.Split(',');
            //adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
            //this.listView.Adapter = adapter;

            var observable = firebase.Child("Games").Child(gameid).Child("playersarray")
                .AsObservable<string>().Subscribe(x =>
                {
                    Toast.MakeText(this, "entered asobservable", ToastLength.Short).Show();
                    string[] arr = x.Object.Split(',');
                    ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
                    this.listView.Adapter = adapter;
                    Try();
                }); // !!!!NEED TO ASK AMOS WHY THIS ISN'T WORKING!!!!
            //Try();
            string str = await firebase.Child("Games").Child(gameid).Child("playersarray").OnceSingleAsync<string>();
            try
            {
                string[] arr = str.Split(',');
                adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
                this.listView.Adapter = adapter;
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, "arr" + ex.Message, ToastLength.Short).Show();
            }
            

            
        }
        private async void Try()
        {
            try
            {
                var x = await firebase.Child("Games").Child(gameid).Child("playersarray")
                                    .OnceSingleAsync<string>();
                string[] arr = x.Split(",");
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
                this.listView.Adapter = adapter;
            } catch (System.Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
            
        }
        private async void subscribe()
        {
            var reference = firebase.Child("Games").Child(gameid);
            Toast.MakeText(this, "in func", ToastLength.Short).Show();
            string data = await reference.OnceSingleAsync<string>();
            Toast.MakeText(this, data, ToastLength.Short).Show();

            string[] arr = data.Split(",");
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
            this.listView.Adapter = adapter;

            var dispose = reference.AsObservable<string>().Subscribe(data =>
            {
                Toast.MakeText(this, data.Object, ToastLength.Short).Show();
                Toast.MakeText(this, "in the func", ToastLength.Short).Show();
                if (data.Object != null)
                {
                    string[] arr = data.Object.Split(",");
                    ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
                    this.listView.Adapter = adapter;
                }
            });
        }
        private async void CreateGame()
        {
            CreateGameId();
            await InitiatePlayers();
            //UpdatePlayerArray();
            UpdatePlayerArray();
        }
    }
}