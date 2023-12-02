using Android.App;
using Android.Content;
using Android.Hardware.Camera2;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GuessThePicBeta4
{
    public class PlayerProperties
    {
        public Player playerPointer;
        public static string name { get; set; }
        public static void Setname(string n) => name = n;
        public static void DeleteName() => name = "";
    }
}