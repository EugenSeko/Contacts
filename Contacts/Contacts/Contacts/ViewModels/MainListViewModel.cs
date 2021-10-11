using Prism.Navigation;
using System;
using System.Collections.Generic;
using Contacts.Views;
using Prism.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;
using Contacts.Services.Authentication;
using Contacts.Services.Settings;

namespace Contacts.ViewModels
{
    class MainListViewModel : BaseViewModel
    {
        private IAuthenticationService _authenticationService;
        private ISettingsManager _settingsManager;


        public MainListViewModel(INavigationService navigationService,
                                 ISettingsManager settingsManager,
                                 IAuthenticationService authenticationService) : base(navigationService)
        {
            _authenticationService = authenticationService;
            _settingsManager = settingsManager;

        }

        public ICommand OnExitButtonTap => new Command(ExitAuthorisation);

        private void ExitAuthorisation(object obj)
        {
            _authenticationService.ExitAuthorisation();
            GoSignInPage();
        }


        #region  --- Navigation ---     
        public ICommand AddButtonTapCommand =>
           new Command(GoToAddEditProfilePage);
        #endregion
    }
}
