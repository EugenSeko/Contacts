using Contacts.Services.Settings;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
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
        }

        public ICommand OnSaveButton => new Command(Save);
        public ICommand LeftArrowCommand => new Command(ExitPage);


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
        private void ExitPage()
        {
            Save();
            GoToMainPage();
        }
        private void Save()
        {
            _settingsManager.SortBy = SelectedSorting;
            _settingsManager.Descending = SelectedDescending;
        }
    }
}
