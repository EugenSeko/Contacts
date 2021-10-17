using Acr.UserDialogs;
using Contacts.Services.Authentication;
using Contacts.Views;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        public ICommand SignUpButtonTapCommand => new Command(ValidateAsync,()=>false);

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
        private string _confirmpass;
        public string ConfirmPass
        {
            get => _confirmpass;
            set => SetProperty(ref _confirmpass, value);
        }
        #endregion
        private async void ValidateAsync()
        {
            var confirmConfig = new ConfirmConfig()
            {
                OkText="Ok",
                CancelText=""
            };
            if(_password==null || _userName == null)
            {
                confirmConfig.Message = "Password and login fields should not be empty";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }
            if (_confirmpass != _password)
            {
                confirmConfig.Message = "Password is not equal to password confirmation";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }

            var m = new Regex(@"^\d{1}").Matches(_userName);
            if (m.Count > 0)
            {
                confirmConfig.Message = "login must not start with a number";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }else
            if (_userName.Length<4 || _userName.Length > 16)
            {
                confirmConfig.Message = "login must be at least 4 characters and no more than 16 characters";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }

            if (_password.Length < 8 || _password.Length > 16)
            {
                confirmConfig.Message = "Password must be at least 8 characters and no more than 16 characters";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }else
            if (!_password.Any(char.IsLetter))
            {
                confirmConfig.Message = "Password must contain letters";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }
            else 
            if (!_password.Any(char.IsDigit))
            {
                confirmConfig.Message = "Password must contain numbers";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }else 
            if (!_password.Any(char.IsLower))
            {
                confirmConfig.Message = "Password must contain lowercase";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }else 
            if (!_password.Any(char.IsUpper))
            {
                confirmConfig.Message = "Password must contain uppercase";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }
            if (!await _authenticationService.RegistrationAsync(UserName, Password))
            {
                confirmConfig.Message = "This login is already taken";
                await UserDialogs.Instance.ConfirmAsync(confirmConfig);
                return;
            }
            Converters.Global.Login = UserName;
            GoSignInPage();

        }



    }
}
