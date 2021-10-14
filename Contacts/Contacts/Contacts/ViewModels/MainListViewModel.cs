using Prism.Navigation;
using System;
using System.Collections.Generic;
using Contacts.Views;
using Prism.Mvvm;
using Contacts.Models;
using System.Windows.Input;
using Xamarin.Forms;
using Contacts.Services.Authentication;
using Contacts.Services.Settings;
using Contacts.Services.Profiles;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Acr.UserDialogs;

namespace Contacts.ViewModels
{
    class MainListViewModel : BaseViewModel, IInitializeAsync
    {
        private IAuthenticationService _authenticationService;
        private ISettingsManager _settingsManager;
        private IProfileManager _profileManager;
        public MainListViewModel(INavigationService navigationService,
                                 ISettingsManager settingsManager,
                                 IAuthenticationService authenticationService,
                                 IProfileManager profileManager) : base(navigationService)
        {
            _authenticationService = authenticationService;
            _settingsManager = settingsManager;
            _profileManager = profileManager;
        }

        public async Task InitializeAsync(INavigationParameters parameters)
        {
            await GetProfileList();
        }
        private async Task GetProfileList()
        {
            var profiles = await _profileManager.GetAllProfilesAsync();

            ProfileList = new ObservableCollection<ProfileViewModel>();

            ProfileViewModel pvm;

            foreach (ProfileModel m in profiles)
            {
                pvm = new ProfileViewModel();

                pvm.Id = m.Id;
                pvm.Name = m.Name;
                pvm.Author = m.Author;
                pvm.CreationTime = m.CreationTime;
                pvm.Description = m.Description;
                pvm.ImageUrl = m.ImageUrl;
                pvm.NickName = m.NickName;
                pvm.ListViewModel = this;
                ProfileList.Add(pvm);
            }
        }

        #region --- Public Properties ---

        private ProfileViewModel _selectedItem;
        //public ProfileViewModel SelectedItem
        //{
        //    get => _selectedItem;
        //    set => SetProperty(ref _selectedItem, value);
        //}

        private ObservableCollection<ProfileViewModel> _profileList;

        public ObservableCollection<ProfileViewModel> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }

        #endregion

        #region --- Commands ---
        public ICommand OnLogautButtonTap => new Command(ExitAuthorisation);
        public ICommand OnSettingsButtonTap => new Command(GoSettings);
        public ICommand OnAddButtonTap => new Command(AddNewProfile);
        public ICommand OnDeleteButtonTap => new Command(DeleteAsync);
        public ICommand OnEditButtonTap => new Command(GoEdit);
        #endregion

        #region --- Privat Helpers ---
        private void GoEdit(object profileObj)
        {
            ProfileViewModel profile = profileObj as ProfileViewModel;
            int id = profile.Id;
            
            GoToAddEditProfilePage(id);
        }

        private async void DeleteAsync(object profileObj) // внимание может не асинк
        {
            if (profileObj != null)
            {
                var confirmConfig = new ConfirmConfig()
                {
                    Message = "You really want to delete this profile?",
                    OkText = "Delete",
                    CancelText = "Cancel"
                };

                var confirm = await UserDialogs.Instance.ConfirmAsync(confirmConfig);

                if (confirm)
                {
                    ProfileViewModel profile = profileObj as ProfileViewModel;
                    await _profileManager.DeleteAsync(profile.ProfileModel);
                    ProfileList.Remove(profile);
                }
            }
        }

        private void ExitAuthorisation(object obj)
        {
            _authenticationService.ExitAuthorisation(); 

            GoSignInPage();
        }

        private void AddNewProfile(object obj)
        {
            GoToAddEditProfilePage(-1); // внимание параметр id отрицательный
        }

        private void GoSettings(object obj)
        {
            GoToSettingsPage();
        }

        #endregion
    }
}
