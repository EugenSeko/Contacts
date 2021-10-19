using Prism.Navigation;
using System.Windows.Input;
using Xamarin.Forms;
using Contacts.Services.Authentication;
using Contacts.Services.Profiles;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Acr.UserDialogs;
using System.Linq;
using Contacts.Converters;
using System;
using Prism.Services.Dialogs;
using System.ComponentModel;

namespace Contacts.ViewModels
{
    class MainListViewModel : BaseViewModel
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IProfileManager _profileManager;
        private IDialogService _dialogService { get; }
        public MainListViewModel(INavigationService navigationService,
                                 IAuthenticationService authenticationService,
                                 IProfileManager profileManager, 
                                 IDialogService dialogservice) : base(navigationService)
        {
            _authenticationService = authenticationService;
            _profileManager = profileManager;
            _dialogService = dialogservice;

            Init();
        }
        private async void Init()
        {
            var profiles = await _profileManager.GetAllProfilesAsync();
            var list = new ObservableCollection<ProfileViewModel>();
            var v = profiles.Select(x => x.ToProfileViewModel());
            foreach (var p in v) 
            {
                if (p.ImageUrl == null)  p.ImageUrl = "user.png";
                p.DeleteCommand = new Command(DeleteAsync);
                p.EditCommand = new Command(GoEdit);
                list.Add(p); 
            }
            ProfileList = list;
        }
        #region --- Public Properties ---
        private ObservableCollection<ProfileViewModel> _profileList;
        public ObservableCollection<ProfileViewModel> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }
        private ProfileViewModel _selecteditem;
        public ProfileViewModel SelectedItem
        {
            get => _selecteditem;
            set => SetProperty(ref _selecteditem, value);
        }
        

        #endregion
        #region --- Commands ---
        public ICommand OnLogautButtonTap => new Command(ExitAuthorisation);
        public ICommand OnSettingsButtonTap => new Command(GoSettings);
        public ICommand OnAddButtonTap => new Command(AddNewProfile);
        #endregion
        #region ---Overrides ---
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(SelectedItem))
            {
                _dialogService.ShowDialog("ImageDialog", new DialogParameters
            {
                {"name",SelectedItem.Name},
                { "imageurl",SelectedItem.ImageUrl},
                {"description",SelectedItem.Description }
            });
            }
            }
        #endregion
        #region --- Privat Helpers ---
        private void GoEdit(object profileObj)
        {
            Global.Id = (profileObj as ProfileViewModel).Id;
            GoToAddEditProfilePage((profileObj as ProfileViewModel).Id);
        }
        private async void DeleteAsync(object profileObj) 
        {
            if (profileObj != null)
            {
                var confirmConfig = new ConfirmConfig()
                {
                    Message = "You really want to delete this profile?",
                    OkText = "Delete",
                    CancelText = "Cancel"
                };

                var confirm = await UserDialogs.Instance.ConfirmAsync(confirmConfig);

                if (confirm)
                {
                    ProfileList.Remove(profileObj as ProfileViewModel);
                    await _profileManager.DeleteAsync((profileObj as ProfileViewModel).ToProfile());
                }
            }
        }
        private void ExitAuthorisation(object obj)
        {
            _authenticationService.ExitAuthorisation(); 

            GoSignInPage();
        }
        private void AddNewProfile(object obj)
        {
            Global.Id = -1;
            GoToAddEditProfilePage(-1); // внимание параметр id отрицательный
        }
        private void GoSettings(object obj)
        {
            GoToSettingsPage();
        }

        #endregion
    }
}
