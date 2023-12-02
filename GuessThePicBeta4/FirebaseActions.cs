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
    public class FirebaseActions
    {
        private FirebaseClient firebase = new FirebaseClient(
       "https://guess-the-pic-a861a-default-rtdb.europe-west1.firebasedatabase.app/");
        private string gameid;
        private Context currentActivityContext;
        public FirebaseActions(string gameid)
        {
            this.gameid = gameid;
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
        public async Task<string> GetPlayerArray()
        {
            string playersarray = await firebase.Child("Games").Child(gameid).Child("playersarray").OnceSingleAsync<string>();
            return playersarray;
        }

        public void SubscribeToPlayersArray()//subscribes to the playersarray in fb
        {
            var disposable = firebase.Child("Games").Child(gameid).Child("playersarray")
                .AsObservable<string>()
                .Subscribe(databaseEvent =>
                {
                    PlayersArrayEvent(databaseEvent.Object);
                });
        }

        private void PlayersArrayEvent(string updatedPlayersArray) //calls whenever there is a playersaray change.
        {
            //need to call a function in the GameLobby activities to change the screens
        }

        public async void AddPLayerToGame(Player newPlayer)
        {

            string playersarray = await firebase.Child("Games").Child(gameid).Child("playersarray").OnceSingleAsync<string>();
            playersarray = $",{newPlayer.name}";
            await firebase.Child("Games").Child(gameid).Child("playersarray").PutAsync<string>(playersarray);

            await firebase.Child("Games").Child(gameid).Child("players").Child(newPlayer.name).PutAsync<Player>(newPlayer);
        }
    }

}