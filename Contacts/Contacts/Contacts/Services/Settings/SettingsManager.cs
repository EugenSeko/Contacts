using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Contacts.Services.Settings
{
    public class SettingsManager : ISettingsManager
    {
        public int Count {
            get => Preferences.Get(nameof(Count), 7);
            set => Preferences.Set(nameof(Count), value);
        }
        public string Name {
            get => Preferences.Get(nameof(Name), "Hello from SettingsManager");
            set => Preferences.Set(nameof(Name), value);
        }
    }
}
