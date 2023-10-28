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

            byte[] bytes;
            string base64str;
            FirebaseClient firebase = new FirebaseClient(
                   "https://guess-the-pic-a861a-default-rtdb.europe-west1.firebasedatabase.app/");
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Upload a picture for the game!"
            });

            var stream = await result.OpenReadAsync();
            var mstream = new MemoryStream();
            stream.CopyTo(mstream);
            bytes = mstream.ToArray();
            base64str = Convert.ToBase64String(bytes, Base64FormattingOptions.InsertLineBreaks);

            await firebase.Child("Games").Child("-Nhq0zFgs8L2MAByvtWe").PutAsync<string>(base64str.ToString());



            /*var data = new
            {
                imageName = base64string,
            };
            await firebase
                .Child("Games").Child("photo")
                .PutAsync(data);//changes the value of the child "message"

            var image = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
            await firebase
                .Child("Games").Child("photo")
                .PutAsync(ConvertImageToBase64(image.ToString())) ;//changes the value of the child "message"
            /*
            var image = await Xamarin.Essentials.MediaPicker.PickPhotoAsync-();
            Toast.MakeText(this, "entered function", ToastLength.Long).Show();
            FirebaseClient firebase = new FirebaseClient(
                    "https://guess-the-pic-a861a-default-rtdb.europe-west1.firebasedatabase.app/");
            await firebase
                .Child("Games").Child("photo")
                .PutAsync(image);//changes the value of the child "message"
            
            //d var result = await firebase
            //    .Child("Games").PostAsync<object>(image); // code that posts the string "hello" in Games json with a random key **remind myself to check how to give it a custom key!!!!
            Toast.MakeText(this, "passed upload", ToastLength.Long).Show();
            //string message = await firebase.Child("message").OnceSingleAsync<string>();
            //Toast.MakeText(this, message, ToastLength.Long).Show();
            */
        }
        static string ConvertImageToBase64(string imagePath)
        {
            try
            {
                // Check if the file exists
                if (!File.Exists(imagePath))
                {
                    throw new FileNotFoundException("The specified file does not exist.", imagePath);
                }

                // Read the image file into a byte array
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                // Convert the byte array to a Base64 string
                string base64String = Convert.ToBase64String(imageBytes);

                return base64String;
            }
            catch (System.Exception ex)
            {
                // Handle exceptions, such as file not found or other IO errors
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
}