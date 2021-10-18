using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
