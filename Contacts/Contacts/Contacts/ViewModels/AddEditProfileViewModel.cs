using Prism.Navigation;
using System;
using System.Collections.Generic;
using Contacts.Views;
using Prism.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    class AddEditProfileViewModel : BaseViewModel
    {
        public AddEditProfileViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        #region --- Navigation ---
        public ICommand OnPlusTapButton => new Command(GoToTestPage);
        #endregion
    }
}
