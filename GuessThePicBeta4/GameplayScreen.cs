﻿using Android.App;
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

        //
        private Image image2;
        //

        private Gameplay gameplay;//add resposibilities later
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.gameplay_screen);

            this.roundcounter = FindViewById<TextView>(Resource.Id.roundcount);
            this.image = FindViewById<ImageView>(Resource.Id.img);
            this.grid = FindViewById<GridLayout>(Resource.Id.grid);

            image.SetImageResource(Resource.Drawable.dog);
            Button b1 = new Button(this);
            b1.Text = "ori";
            b1.TextSize = 20;
            Button b2 = new Button(this);
            b2.Text = "amos";
            b2.TextSize = 20;
            Button b3 = new Button(this);
            b3.Text = "mori";
            Button b4 = new Button(this);
            b4.Text = "shaked";
            b4.TextSize = 20;
            Button b5 = new Button(this);
            b5.Text = "noam";
            b5.TextSize = 20;
            Button b6 = new Button(this);
            b6.Text = "gili";
            b6.TextSize = 20;
            b1.Click += OnClick;
            grid.AddView(b1);
            grid.AddView(b2);
            grid.AddView(b3);
            grid.AddView(b4);
            grid.AddView(b5);
            grid.AddView(b6);


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