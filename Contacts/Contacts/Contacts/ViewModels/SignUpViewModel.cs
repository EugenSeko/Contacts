﻿using Acr.UserDialogs;
using Contacts.Services.Authentication;
using Prism.Navigation;
using System.Linq;
using System.Text.RegularExpressions;
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
        public ICommand OnLeftArrowTap => new Command(GoSignInPage);
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
            confirmConfig.Message = null;

            if(_password==null || _userName == null)
                confirmConfig.Message = "Password and login fields should not be empty";
            if (_confirmpass != _password)
                confirmConfig.Message = "Password is not equal to password confirmation";

            var m = new Regex(@"^\d{1}").Matches(_userName);
            if (m.Count > 0)
                confirmConfig.Message = "login must not start with a number";
            else
            if (_userName.Length<4 || _userName.Length > 16)
                confirmConfig.Message = "login must be at least 4 characters and no more than 16 characters";
            
            if (_password.Length < 8 || _password.Length > 16)
                confirmConfig.Message = "Password must be at least 8 characters and no more than 16 characters";
            else
            if (!_password.Any(char.IsLetter))
                confirmConfig.Message = "Password must contain letters";
            else 
            if (!_password.Any(char.IsDigit))
                confirmConfig.Message = "Password must contain numbers";
            else 
            if (!_password.Any(char.IsLower))
                confirmConfig.Message = "Password must contain lowercase";
            else 
            if (!_password.Any(char.IsUpper))
                confirmConfig.Message = "Password must contain uppercase";

            if (confirmConfig.Message==null & !await _authenticationService.RegistrationAsync(UserName, Password))
                confirmConfig.Message = "This login is already taken";

            if (confirmConfig.Message == null)
            {
                Converters.Global.Login = UserName;
                GoSignInPage();
            }else
            await UserDialogs.Instance.ConfirmAsync(confirmConfig);
            return;
        }
    }
}
