using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace GuessThePicBeta4
{
    [Activity(Label = "GameplayScreen")]
    public class GameplayScreen : Activity
    {
        private TextView roundcounter;
        private ImageView image;
        private GridLayout grid;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.gameplay_screen);

            this.roundcounter = FindViewById<TextView>(Resource.Id.roundcount);
            this.image = FindViewById<ImageView>(Resource.Id.img);
            this.grid = FindViewById<GridLayout>(Resource.Id.grid);

            image.SetImageResource(Resource.Drawable.dog);
            Button b1 = new Button(this);
            b1.Text = "player 1";
            b1.TextSize = 20;
            b1.Click += OnClick;
            grid.AddView(b1);


        }

        private async void OnClick(object sender, EventArgs e)
        {
            FirebaseClient firebase = new FirebaseClient(
                "https://guess-the-pic-a861a-default-rtdb.europe-west1.firebasedatabase.app/");
            Toast.MakeText(this, "button worked", ToastLength.Long).Show();
            string imageData = await firebase.Child("Games").Child("-Nhq0zFgs8L2MAByvtWe").OnceSingleAsync<string>();
            byte[] imageBytes = Convert.FromBase64String(imageData);
            // Create a Bitmap from the byte array
            Bitmap bitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
            image.SetImageBitmap(bitmap);
        }
        //-Nhq0zFgs8L2MAByvtWe
    }
}