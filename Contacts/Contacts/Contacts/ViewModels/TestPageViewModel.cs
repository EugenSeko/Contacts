using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    class TestPageViewModel : BindableBase, IInitializeAsync
    {
        private ISettingsManager _settingsManager;
        private IRepository _repository;
        public TestPageViewModel(ISettingsManager settingsManager, IRepository repository )
        {
            _settingsManager = settingsManager;
            _repository = repository;
        }

        #region --- Public Properties ---

       public ICommand AddButtonTapCommand => new Command(OnAddButtonTap);

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private ObservableCollection<ProfileModel> _profileList; // паблик?

        public ObservableCollection<ProfileModel> ProfileList
        {
            get => _profileList;
            set => SetProperty(ref _profileList, value);
        }


        #endregion
        #region --- Public Methods ---
        public async Task InitializeAsync(INavigationParameters parameters)
        {
            var profileList = await _repository.GetAllAsync<ProfileModel>();
            ProfileList = new ObservableCollection<ProfileModel>(profileList);
        }
        #endregion
        #region --- Overrides ---
        #endregion
        #region --- Privat Helpers ---
        private async void OnAddButtonTap(object obj)
        {
            var profile = new ProfileModel()
            {
                FirstName = FirstName,
                LastName = LastName,
                CreationTime = DateTime.Now
            };
            // await _repository.InsertAsync<ProfileModel>(profile);
            var id = await _repository.InsertAsync(profile);
            profile.Id = id;
            ProfileList.Add(profile);
            //await _repository.DeleteAllAsync<ProfileModel>();
        }

      
        #endregion
    }
}
