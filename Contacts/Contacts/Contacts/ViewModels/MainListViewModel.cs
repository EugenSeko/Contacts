﻿using Prism.Navigation;
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
            var profiles = await  _profileManager.GetAllProfilesAsync();

            ProfileList = profiles;
        }


        #region --- Public Properties


        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string _nickName;
        public string NickName
        {
            get => _lastName;
            set => SetProperty(ref _nickName, value);
        }

        private ProfileModel _selectedItem;
        public ProfileModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private ObservableCollection<ProfileModel> _profileList;

        public ObservableCollection<ProfileModel> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }

        #endregion


        #region --- Commands ---
        public ICommand OnLogautButtonTap => new Command(ExitAuthorisation);
        public ICommand OnSettingsButtonTap => new Command(Settings);
        public ICommand OnAddButtonTap => new Command(AddNewProfile);
        public ICommand MoveToTopCommand => new Command(Settings);



        #endregion


        #region --- Privat Helpers ---
        private void ExitAuthorisation(object obj)
        {
            _authenticationService.ExitAuthorisation(); 

            GoSignInPage();
        }

        private void AddNewProfile(object obj)
        {
            GoToAddEditProfilePage();
        }

        private void Settings(object obj)
        {
            GoToSettingsPage();
        }

        #endregion
    }
}
