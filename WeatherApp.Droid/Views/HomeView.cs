using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using AndroidX.AppCompat.Widget;
using DjK.WeatherApp.Core.ViewModels;
using Google.Android.Material.Snackbar;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Android.Views;
using MvvmCross.ViewModels;
using System.Globalization;

namespace DjK.WeatherApp.Droid.Views
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class HomeView : MvxActivity<HomeViewModel>
    {
        private IMvxInteraction<string> _interaction;
        MvxFluentBindingDescriptionSet<HomeView, HomeViewModel> _bindingSet;

        public bool RequestInProgress { get; set; }

        public IMvxInteraction<string> Interaction
        {
            get { return _interaction; }
            set
            {
                if (_interaction != null)
                    _interaction.Requested -= OnInteractionRequested;

                _interaction = value;
                _interaction.Requested += OnInteractionRequested;
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

            _bindingSet = this.CreateBindingSet<HomeView, HomeViewModel>();
            _bindingSet.Bind(this).For(view => view.Interaction).To(viewModel => viewModel.InteractionForCitySaved).OneWay();
            _bindingSet.Bind(this).For(view => view.RequestInProgress).To(viewModel => viewModel.RequestInProgress).OneWay();
            _bindingSet.Apply();
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

        public override void OnBackPressed()
        {
            if (RequestInProgress)
                ViewModel.CancelRequestCommand.Execute();
            else
               base.OnBackPressed();
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (_interaction != null)
                _interaction.Requested -= OnInteractionRequested;

            _bindingSet.Dispose();
        }

    }
}