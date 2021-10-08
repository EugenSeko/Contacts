using Contacts.Views;
using Prism.Mvvm;
using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    class SignInViewModel : BaseViewModel
    {

        public SignInViewModel(INavigationService navigationService) : base(navigationService)
        {
            
        }

        #region --- Navigation ---
        public ICommand SignInButtonTapCommand =>
            new Command(GoToMainPage);

        public ICommand SignUpButtonTapCommand =>
            new Command(GoToSignUpPage);
        #endregion
    }
}
