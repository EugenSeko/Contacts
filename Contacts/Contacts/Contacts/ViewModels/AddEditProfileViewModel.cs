using Prism.Navigation;
using System;
using System.Collections.Generic;
using Contacts.Views;
using Prism.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;
using Contacts.Services.Profiles;
using Contacts.Models;
using System.Threading.Tasks;
using Contacts.Services.Settings;

namespace Contacts.ViewModels
{
    class AddEditProfileViewModel : BaseViewModel, INavigationAware
    {
        private IProfileManager _profileManager;
        private ISettingsManager _settingsManager;
        public AddEditProfileViewModel(INavigationService navigationService, 
                                       IProfileManager profileManager, 
                                       ISettingsManager settingsManager) : base(navigationService)
        {
            _profileManager = profileManager;
            _settingsManager = settingsManager;
        }

        private int _id;

        #region --- Public Properties ---

        private ProfileModel _profile;
        public ProfileModel Profile
        {
            get => _profile;
            set => SetProperty(ref _profile, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _nickname;
        public string NickName
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        #endregion
        #region --- Commands ---
        public ICommand OnSaveButton => new Command(Save);
        public ICommand OnPlusTapButton => new Command(GoToTestPage);
        public ICommand OnLArrowTapButton => new Command(GoToMainPage);
        #endregion
        #region --- Private Helpers ---
        private async Task GetProfile()
        {
            var v = await _profileManager.GetProfileById(_id);
            Profile = v;
        }
        private async void Save()
        {
            if(_id >= 0)// Update // текущее
            {
                await GetProfile();
            }
            else// Create
            {
                ProfileModel profile = new ProfileModel();

                profile.Author = _settingsManager.UserName; // внимание: свойство юзернейм лучше поменять
                profile.Name = Name;  
                profile.NickName = NickName;
                profile.CreationTime = DateTime.Now;
                profile.Description = Description;

                await _profileManager.CreateAsync(profile);
            }
        }
        #endregion
        #region --- Navigation ---
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _id = parameters.GetValue<int>("id");
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        #endregion
    }
}
