using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using AndroidX.AppCompat.Widget;
using DjK.WeatherApp.Core.ViewModels;
using Google.Android.Material.Snackbar;
using Google.Android.Material.TextField;
using Java.Interop;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.ViewModels;
using System;
using System.Globalization;

namespace DjK.WeatherApp.Droid.Views
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class HomeView : MvxActivity<HomeViewModel>
    {
        private IMvxInteraction<string> _Interaction;

        public IMvxInteraction<string> Interaction
        {
            get { return _Interaction; }
            set
            {
                if (_Interaction != null)
                    _Interaction.Requested -= OnInteractionRequested;

                _Interaction = value;
                _Interaction.Requested += OnInteractionRequested;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_home);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            ViewModel.SetCurrentCultureCommand.Execute(CultureInfo.CurrentUICulture);

            var bindingSet = this.CreateBindingSet<HomeView, HomeViewModel>();
            bindingSet.Bind(this).For(view => view.Interaction).To(viewModel => viewModel.Interaction).OneWay();
            bindingSet.Apply();
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        private void OnInteractionRequested(object sender, MvxValueEventArgs<string> e)
        {
            if (CurrentFocus != null)
            {
                InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
                imm.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);
            }

            var view = FindViewById(Resource.Id.content_home_view);
            Snackbar.Make(
                view,
                $"{Resources.GetString(Resource.String.added_as_favourite)} {e.Value}",
                Snackbar.LengthLong)
            .SetAction("Action", (View.IOnClickListener)null)
            .Show();
        }


        protected override void OnPause()
        {
            base.OnPause();

            if (_Interaction != null)
                _Interaction.Requested -= OnInteractionRequested;
        }

    }
}