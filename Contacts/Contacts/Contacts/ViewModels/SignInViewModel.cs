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

        }

        public ICommand SignInButtonTapCommand => new Command(TryAuthorisation);

        private async void TryAuthorisation(object obj)
        {
            string done = await  _authenticationService.AuthorisatonAsync(_userName, _password);

            if (done == "done") {
                Console.WriteLine("Done");
                GoToMainPage();
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
