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
    public class Player
    {
        public string name { get; set; }
        private string[] pictures;

        public Player(string name)
        {
            this.name = name;
            this.pictures = new string[0];

        }
        public void AddPicture(string picturebase64)
        {
            string[] newarr = new string[pictures.Length + 1];
            for (int i = 0; i < this.pictures.Length; i++) 
            {
                newarr[i] = pictures[i];
            }
            newarr[pictures.Length] = picturebase64;
            pictures = newarr;
        }
    }
}