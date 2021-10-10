using Contacts.Services.Authentication;
using Contacts.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    class SignUpViewModel : BaseViewModel
    {
        private  IAuthenticationService _authenticationService;
        public SignUpViewModel(INavigationService navigationService, 
                               IAuthenticationService authenticationService) : base(navigationService)
        {
            _authenticationService = authenticationService;
        }

        public ICommand SignUpButtonTapCommand => new Command(OnSignUpButtonTap);

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

        private async void OnSignUpButtonTap(object obj)
        {
            var done = await _authenticationService.RegistrationAsync(UserName, Password);

            Console.WriteLine(done);

            if (done) 
            {
                _authenticationService.GetAllUsers(); //временно

                GoSignInPage();
            } 
        }

      

    }
}
