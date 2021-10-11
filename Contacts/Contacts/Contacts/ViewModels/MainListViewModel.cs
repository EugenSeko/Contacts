using Prism.Navigation;
using System;
using System.Collections.Generic;
using Contacts.Views;
using Prism.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;
using Contacts.Services.Authentication;
using Contacts.Services.Settings;
using Contacts.Services.Profiles;

namespace Contacts.ViewModels
{
    class MainListViewModel : BaseViewModel
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

        public ICommand OnExitButtonTap => new Command(ExitAuthorisation);

        private void ExitAuthorisation(object obj)
        {
            _authenticationService.ExitAuthorisation();
            GoSignInPage();

           // _profileManager.GetAllProfiles(); //временно

        }


        #region  --- Navigation ---     
        public ICommand AddButtonTapCommand =>
           new Command(GoToAddEditProfilePage);
        #endregion
    }
}
