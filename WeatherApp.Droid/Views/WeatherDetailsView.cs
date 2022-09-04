﻿using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using DjK.WeatherApp.Core.ViewModels;
using MvvmCross.Platforms.Android.Views;

namespace DjK.WeatherApp.Droid.Views
{
    [Activity(Label = "@string/weather_details_activity_title")]
    public class WeatherDetailsView : MvxActivity<WeatherDetailsViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_weather_details);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_weather_details, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.add_to_favourities:
                    // TODO: add to favourities command
                    return true;
                case Android.Resource.Id.Home:
                    ViewModel.CloseCommand.Execute();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        public override void OnBackPressed()
        {
            ViewModel.CloseCommand.Execute();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

    }
}