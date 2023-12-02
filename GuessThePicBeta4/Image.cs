using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuessThePicBeta4
{
    public class Image
    {
        public byte[] ImageBytesData { get; }
        public string sourcePlayer { get; }
        public Image(byte[] imageBytesDATA, string sourcePlayer)//gets name and byte data
        {
            this.ImageBytesData = imageBytesDATA;
            this.sourcePlayer = sourcePlayer;
        }

        
        public Image(byte[] imageBytesDATA)  //gets only byte data
        {
            this.ImageBytesData = imageBytesDATA;
            this.sourcePlayer = PlayerProperties.name;
        }

        public Image(string base64Image) 
        {
            this.sourcePlayer = PlayerProperties.name;
            this.ImageBytesData = this.ConvertBase64ToBytes(base64Image);
        }

        public string ConvertBytesToBase64(byte[] imageBytes)
        {
            string imageBase64 = Convert.ToBase64String(imageBytes, Base64FormattingOptions.InsertLineBreaks);// convert bytes to base64
            return imageBase64;
        }

        public byte[] ConvertBase64ToBytes(string base64Image)
        {
            byte[] imageBytes = Convert.FromBase64String(base64Image);
            return imageBytes;
        }
        
        public Bitmap GetBipmapData()
        {
            Bitmap bitmap = BitmapFactory.DecodeByteArray(this.ImageBytesData, 0, this.ImageBytesData.Length);
            return bitmap;
        }



    }
}