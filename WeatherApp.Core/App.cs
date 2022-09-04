using DjK.WeatherApp.Core.ViewModels;
using MvvmCross.ViewModels;

namespace DjK.WeatherApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<HomeViewModel>();
        }
    }
}