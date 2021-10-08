using Prism.Navigation;
using System;
using System.Collections.Generic;
using Contacts.Views;
using Prism.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    class MainListViewModel : BaseViewModel
    {
        public MainListViewModel(INavigationService navigationService) : base(navigationService)
        {
        }

        #region  --- Navigation ---     
        public ICommand AddButtonTapCommand =>
           new Command(GoToAddEditProfilePage);
        #endregion
    }
}
