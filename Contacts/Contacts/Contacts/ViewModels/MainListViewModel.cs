﻿using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using Contacts.Services.Authentication;
using Contacts.Services.Profiles;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using System.Linq;
using Contacts.Converters;

namespace Contacts.ViewModels
{
    class MainListViewModel : BaseViewModel, IInitializeAsync
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IProfileManager _profileManager;
        public MainListViewModel(INavigationService navigationService,
                                 IAuthenticationService authenticationService,
                                 IProfileManager profileManager) : base(navigationService)
        {
            _authenticationService = authenticationService;
            _profileManager = profileManager;
        }
        public async Task InitializeAsync(INavigationParameters parameters)
        {
            await Init();
        }
        private async Task Init()
        {
            var profiles = await _profileManager.GetAllProfilesAsync();
            var list = new ObservableCollection<ProfileViewModel>();
            var v = profiles.Select(x => x.ToProfileViewModel());
            foreach (var p in v) 
            {
                if (p.ImageUrl == null)  p.ImageUrl = "user.png";
                p.DeleteCommand = new Command(DeleteAsync);
                p.EditCommand = new Command(GoEdit);
                list.Add(p); 
            }
            ProfileList = list;
        }

        #region --- Public Properties ---
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
        #endregion

        #region --- Privat Helpers ---
        private void GoEdit(object profileObj)
        {
            GoToAddEditProfilePage((profileObj as ProfileViewModel).Id);
        }
        private async void DeleteAsync(object profileObj) 
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
                    ProfileList.Remove(profileObj as ProfileViewModel);
                    await _profileManager.DeleteAsync((profileObj as ProfileViewModel).ToProfile());
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
