using Contacts.Resx;
using Contacts.Services.Settings;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    class SettingsViewModel:BaseViewModel
    {
        private readonly ISettingsManager _settingsManager;
        public SettingsViewModel(INavigationService navigationService,
                                 ISettingsManager settingsManager) :base(navigationService)
        {
            _settingsManager = settingsManager;
            RadioButtonsInit();
            CurrentLanguage = new(GetCurrentLanguageName);
            ChangeLanguageCommand = new AsyncCommand(ChangeLanguage);
        }
        public ICommand OnSaveButton => new Command(Save);
        public ICommand LeftArrowCommand => new Command(ExitPage);
        #region ---Overrides ---
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(IsDarkToggled) && !(IsDarkToggled == true && Converters.Global.ThemeStyle == "dark" || IsDarkToggled == false && Converters.Global.ThemeStyle == "light"))
            {
                Save();
                GoToSettingsPage();
            }
        }
        #endregion
        #region --- Public Properties ---
        private string _selectedsorting;
        public string SelectedSorting
        {
            get => _selectedsorting;
            set => SetProperty(ref _selectedsorting, value);
        }

        private string _selecteddescending;
        public string SelectedDescending
        {
            get => _selecteddescending;
            set => SetProperty(ref _selecteddescending, value);
        }

        private bool _isnamechecked;
        public bool IsNameChecked
        {
            get => _isnamechecked;
            set => SetProperty(ref _isnamechecked, value);
        }

        private bool _isnicknamechecked;
        public bool IsNickNameChecked
        {
            get => _isnicknamechecked;
            set => SetProperty(ref _isnicknamechecked, value);
        }

        private bool _isCreationTimechecked;
        public bool IsCreationTimechecked
        {
            get => _isCreationTimechecked;
            set => SetProperty(ref _isCreationTimechecked, value);
        }

        private bool _isdescendchecked;
        public bool IsDescendChecked
        {
            get => _isdescendchecked;
            set => SetProperty(ref _isdescendchecked, value);
        }

        private bool _isascendchecked;
        public bool IsAscendChecked
        {
            get => _isascendchecked;
            set => SetProperty(ref _isascendchecked, value);
        }
        private bool _isdarktoggled;
        public bool IsDarkToggled
        {
            get => _isdarktoggled;
            set => SetProperty(ref _isdarktoggled, value);
        }
        #endregion
        #region --- Privat Helpers ---
        private void RadioButtonsInit()
        {
            switch (_settingsManager.Descending)
            {
                case "true":
                    IsDescendChecked = true;
                    break;
                case "false":
                    IsAscendChecked = true;
                    break;
            }
            switch (_settingsManager.SortBy)
            {
                case "Name":
                    IsNameChecked = true;
                    break;
                case "NickName":
                    IsNickNameChecked = true;
                    break;
                case "CreationTime":
                    IsCreationTimechecked = true;
                    break;
            }
            IsDarkToggled = Converters.Global.ThemeStyle == "dark";
        }
        private void ExitPage()
        {
            Save();
            GoToMainPage();
        }
        private void Save()
        {
            _settingsManager.SortBy = SelectedSorting ?? "CreationTime";
            _settingsManager.Descending = SelectedDescending ??  "true";
            _settingsManager.ThemeStyle = IsDarkToggled ? "dark" : "light";
            Converters.Global.ThemeStyle = IsDarkToggled ? "dark" : "light";
        }
        #endregion
        #region --- Localization ---
        List<(Func<string> name, string value)> languageMapping { get; } = new()
        {
            (() => AppResources.System, null),
            (() => AppResources.English, "en"),
            (() => AppResources.Ukrainian, "uk"),
            (() => AppResources.Russian, "ru"),
        };
        public LocalizedString CurrentLanguage { get; }
        public LocalizedString Version { get; } = new(() => string.Format(AppResources.Version, AppInfo.VersionString));
        public ICommand ChangeLanguageCommand { get; }
        private string GetCurrentLanguageName()
        {
            var (knownName, _) = languageMapping.SingleOrDefault(m => m.value == LocalizationResourceManager.Current.CurrentCulture.TwoLetterISOLanguageName);
            return knownName != null ? knownName() : LocalizationResourceManager.Current.CurrentCulture.DisplayName;
        }
        private async Task ChangeLanguage()
        {
            string selectedName = await Application.Current.MainPage.DisplayActionSheet(
                AppResources.ChangeLanguage,
                null, null,
                languageMapping.Select(m => m.name()).ToArray());
            if (selectedName == null)
            {
                return;
            }
            string selectedValue = languageMapping.Single(m => m.name() == selectedName).value;
            LocalizationResourceManager.Current.CurrentCulture = selectedValue == null ? CultureInfo.CurrentCulture : new CultureInfo(selectedValue);
        }
        #endregion
    }
}
