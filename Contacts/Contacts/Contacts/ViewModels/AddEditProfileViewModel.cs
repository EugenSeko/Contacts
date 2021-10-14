using Prism.Navigation;
using System;
using System.Collections.Generic;
using Contacts.Views;
using Prism.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;
using Contacts.Services.Profiles;
using Contacts.Models;
using System.Threading.Tasks;

namespace Contacts.ViewModels
{
    class AddEditProfileViewModel : BaseViewModel, INavigationAware
    {
        private IProfileManager _profileManager;

        public AddEditProfileViewModel(INavigationService navigationService, 
                                       IProfileManager profileManager) : base(navigationService)
        {
            _profileManager = profileManager;
        }

        private int _id;

        private ProfileModel _profile;
        public ProfileModel Profile
        {
            get => _profile;
            set => SetProperty(ref _profile, value);
        }
        private string _firstname;
        public string FirstName
        {
            get => _firstname;
            set => SetProperty(ref _firstname, value);
        }


        private async Task GetProfile()
        {
            var v = await _profileManager.GetProfileById(_id);
            Profile = v;
        }

        #region --- Navigation ---
        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            _id = parameters.GetValue<int>("id");
             await  GetProfile(); // внимание: лежит в навигации
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        public ICommand OnPlusTapButton => new Command(GoToTestPage);
        #endregion
    }
}
