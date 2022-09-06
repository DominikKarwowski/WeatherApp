using AndroidX.Core.Widget;

namespace DjK.WeatherApp.Droid
{
    public class LinkerPleaseInclude
    {
        public static void Include(ContentLoadingProgressBar progressBar)
        {
            progressBar.Click += (s, e) => progressBar.Visibility = progressBar.Visibility - 1;
        }
    }
}