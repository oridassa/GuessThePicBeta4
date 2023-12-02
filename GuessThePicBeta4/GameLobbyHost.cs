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
using System.Reflection;
using Android.App.Backup;
using System.Reactive.Linq;
using System.Collections.ObjectModel;

namespace GuessThePicBeta4
{
    [Activity(Label = "GameLobbyHost")]
    public class GameLobbyHost : Activity, View.IOnClickListener, IDisposable
    {
        private FirebaseActions fbactions;
        private FirebaseClient firebase = new FirebaseClient(
       "https://guess-the-pic-a861a-default-rtdb.europe-west1.firebasedatabase.app/");
        ListView listView;
        TextView gameidview;
        private string gameid;
        Player player;
        IDisposable subscription;

        private PictureTransport pt;

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game_lobby_host);

            FirebaseApp.InitializeApp(this);

            fbactions = new FirebaseActions("https://guess-the-pic-a861a-default-rtdb.europe-west1.firebasedatabase.app/");

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
            //subscription.Dispose();
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

        private void CreateGameId() //creates a random gameid and creates a json object in the Games section
        {
            Random rand = new Random();
            gameid = $"{rand.Next(100000, 1000000)}";
            //string gameid = await GetUnusedSixDigitNumber();

            fbactions.GameLobbyInit(gameid); //need to add a check if the gameid already exists
            gameidview.Text = gameid;
        }
        public async Task<bool> InitiatePlayers() //create a Player object for host and initiate playersarray with his name
        {
            bool result = await fbactions.GameLobbyPlayersInit(gameid, player);
            return result;

        }
        public void UpdatePlayerArray() //updates the Listview that show the players in the lobby
        {
            SetPlayerListview();
            var subscription1 = firebase.Child("Games").Child(gameid).Child("playersarray")
            .AsObservable<string>()
            .Subscribe((x) =>
            {
                SetPlayerListview(x.Object);
                //Toast.MakeText(this, "entered asobservable", ToastLength.Short).Show();
                //gameidview.Text = x.ToString();
                //string[] arr = x.Object.Split(',');
                //ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
                //this.listView.Adapter = adapter;
            } ); // !!!!NEED TO ASK AMOS WHY THIS ISN'T WORKING!!!!
            //trying
        }   
        public async void SetPlayerListview()
        {
            ArrayAdapter<string> adapter;
            string str = await fbactions.GetPlayerArray(gameid);
            try
            {
                string[] arr = str.Split(',');
                adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
                this.listView.Adapter = adapter;
            }
            catch (System.Exception ex)
            {
                Toast.MakeText(this, "arr " + ex.Message, ToastLength.Short).Show();
            }
        }
        public void SetPlayerListview(string str)
        {
            Toast.MakeText(this, "entered asobservable", ToastLength.Short).Show();
            ArrayAdapter<string> adapter;
            string[] arr = str.Split(',');
            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, arr);
            this.listView.Adapter = adapter;
            
        }
        private async void CreateGame()
        {
            CreateGameId();
            await InitiatePlayers();
            UpdatePlayerArray();
        }
    }
}