using Xamarin.Essentials;

namespace Contacts.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
        public string UserName
        {
            get => Preferences.Get(nameof(UserName), null);
            set => Preferences.Set(nameof(UserName), value);
        }
        public string SortBy
        { 
            get => Preferences.Get(nameof(SortBy), "CreationTime");
            set => Preferences.Set(nameof(SortBy), value);
        }
        public string Descending
        {
            get => Preferences.Get(nameof(Descending), "true");
            set => Preferences.Set(nameof(Descending), value);
        }
        public string ThemeStyle
        {
            get => Preferences.Get(nameof(ThemeStyle), "light");
            set => Preferences.Set(nameof(ThemeStyle), value);
        }
    }
}
