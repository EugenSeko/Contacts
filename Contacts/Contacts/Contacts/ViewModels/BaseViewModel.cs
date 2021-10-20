using Contacts.Views;
using Prism.Mvvm;
using Prism.Navigation;


namespace Contacts.ViewModels
{
    class BaseViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;
        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
        #region --- Navigation ---
        public async void NavigateGoBack()
        {
            await _navigationService.GoBackAsync();
        }

        public async void GoToMainPage()
        {
            await _navigationService.NavigateAsync("/"+nameof(MainListView));
        }

        public async void GoBackToRootAsync()
        {
            await _navigationService.GoBackToRootAsync();
        }
        public async void GoToSignUpPage()
        {
            await _navigationService.NavigateAsync(nameof(SignUpView));
        }
        public async void GoSignInPage()
        {
            await _navigationService.NavigateAsync("/"+nameof(SignInView));
        }
        public async void GoToSettingsPage()
        {
            await _navigationService.NavigateAsync(nameof(SettingsView));
        }
        public  async void GoToAddEditProfilePage(int id)
        {
            var p = new NavigationParameters();
            p.Add("id", id);
            await _navigationService.NavigateAsync(nameof(AddEditProfileView),p);
        }
        public async void GoToTestPage()
        {
            await _navigationService.NavigateAsync(nameof(TestPage));
        }


        #endregion
    }
}
