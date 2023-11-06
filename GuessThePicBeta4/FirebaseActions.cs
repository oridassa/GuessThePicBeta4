using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Graphics.ColorSpace;

namespace GuessThePicBeta4
{
    class FirebaseActions 
    {
        private FirebaseClient firebase;
        
        public FirebaseActions(string firebaseURL)
        {
            this.firebase = new FirebaseClient(firebaseURL);
        }

        public async void GameLobbyInit(string gameid) //creates a random gameid and creates a json object in the Games section
        {
            await firebase.Child("Games").Child(gameid).Child("gamid").PutAsync<string>(gameid);
        }
        public async Task<bool> GameLobbyPlayersInit(string gameid, Player player)
        {
            string host_name = player.name;
            string playersarray = host_name;
            await firebase.Child("Games").Child(gameid).Child("playersarray").PutAsync<string>(playersarray);
            await firebase.Child("Games").Child(gameid).Child("players").Child(host_name).PutAsync<Player>(player);
            return true;
        }
        public async Task<string> GetPlayerArray(string gameid)
        {
            string playersarray = await firebase.Child("Games").Child(gameid).Child("playersarray").OnceSingleAsync<string>();
            return playersarray;
        }
    }
}