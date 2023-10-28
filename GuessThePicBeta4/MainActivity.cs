using Android;
using Android.Accessibilityservice.AccessibilityService;
using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.App;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GuessThePicBeta4
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, View.IOnClickListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            Button btn = FindViewById<Button>(Resource.Id.check);
            btn.Click += RequestPrem;
        }

        private async void RequestPrem(object sender, EventArgs e)
        {
            var premission = await Permissions.CheckStatusAsync<Permissions.Photos>();
            Toast.MakeText(this, premission.ToString(), ToastLength.Short).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);


        }
        
        public void OnClick(View v)
        {
            Intent intent;
            Button b = (Button)v;
            if (b.Text == "Create a game")
            {
                intent = new Intent(this, typeof(GameLobbyHost));
                base.StartActivity(intent);
            }
            else if (b.Text == "Join a game")
            {
                intent = new Intent(this, typeof(LoginActivity));
                base.StartActivity(intent);
            }
            
        }

    }
}