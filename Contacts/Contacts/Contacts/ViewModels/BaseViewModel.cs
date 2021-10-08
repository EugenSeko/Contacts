using Contacts.Views;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    class BaseViewModel : BindableBase
    {


        #region --- Navigation ---
        protected INavigationService _navigationService;

        public async void NavigateGoBack()
        {
            await _navigationService.GoBackAsync();
        }

        public async void GoToMainPage()
        {
            await _navigationService.NavigateAsync(nameof(MainListView));
        }
        public async void GoToSignUpPage()
        {
            await _navigationService.NavigateAsync(nameof(SignUpView));
        }
        public async void GoSignInPage()
        {
            await _navigationService.NavigateAsync(nameof(SignInView));
        }
        public async void GoToSettingsPage()
        {
            await _navigationService.NavigateAsync(nameof(SettingsView));
        }
        public async void GoToAddEditProfilePage()
        {
            await _navigationService.NavigateAsync(nameof(AddEditProfileView));
        }
        #endregion
    }
}
