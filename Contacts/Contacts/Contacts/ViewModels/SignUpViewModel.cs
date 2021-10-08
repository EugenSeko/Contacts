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
        public SignUpViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        
    }
}
