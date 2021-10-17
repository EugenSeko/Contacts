using Acr.UserDialogs;
using Contacts.Services.Authentication;
using Contacts.Services.Settings;
using Contacts.Views;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    class SignInViewModel : BaseViewModel
    {

        private IAuthenticationService _authenticationService;

        private ISettingsManager _settingsManager;


        public SignInViewModel(INavigationService navigationService,
            IAuthenticationService authenticationService,
            ISettingsManager settingsManager) : base(navigationService)
        {
            _authenticationService = authenticationService;
            _settingsManager = settingsManager;
            _userName = Converters.Global.Login;
        }

        public ICommand SignInButtonTapCommand => new Command(TryAuthorisation);

        private async void TryAuthorisation(object obj)
        {
            if (UserName == null || UserName == "" || Password == null || Password == "") return;

            string done = await  _authenticationService.AuthorisatonAsync(_userName, _password);
            var confirmConfig = new ConfirmConfig()
            {
                OkText = "Ok",
                CancelText = ""
            };
            if (done == "done") {
                Converters.Global.Login = null;
                GoToMainPage();
            }else
            if(done == "login_missing")
            {
                confirmConfig.Message = "No such login";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                UserName = "";
                return;
            }else
            if(done== "pass_missing")
            {
                confirmConfig.Message = "Wrong password";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                Password = "";
                return;
            }
        }


        #region --- Public Properties ---

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        #endregion



        #region --- Navigation ---
        public ICommand SignUpButtonTapCommand =>
            new Command(GoToSignUpPage);
        #endregion
    }
}
