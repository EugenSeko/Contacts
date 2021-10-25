using Acr.UserDialogs;
using Contacts.Resx;
using Contacts.Services.Authentication;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    class SignInViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        public SignInViewModel(INavigationService navigationService,
            IAuthenticationService authenticationService) : base(navigationService)
        {
            _authenticationService = authenticationService;
            _userName = Converters.Global.Login;
        }
        public ICommand SignInButtonTapCommand => new Command(TryAuthorisation, () => false);
        private async void TryAuthorisation()
        {
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
                confirmConfig.Message = AppResources.nologin;
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                UserName = "";
                return;
            }else
            if(done== "pass_missing")
            {
                confirmConfig.Message = AppResources.wrongpass;
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
