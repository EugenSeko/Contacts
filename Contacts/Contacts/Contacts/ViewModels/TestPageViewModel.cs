using Acr.UserDialogs;
using Contacts.Dialogs;
using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    class TestPageViewModel : BaseViewModel, IInitializeAsync
    {
        private ISettingsManager _settingsManager;
        private IRepository _repository;
        private IDialogService _dialogService { get; }
        public TestPageViewModel(ISettingsManager settingsManager, 
                                 IRepository repository,
                                 IDialogService dialogservice, 
                                 INavigationService navigationService) : base(navigationService)
        {
            _dialogService = dialogservice;
            _settingsManager = settingsManager;
            _repository = repository;
        }
       


        #region --- Public Properties ---

        public ICommand DeleteTapCommand => new Command(OnDeleteTap);
        public ICommand UpdateTapCommand => new Command(OnUpdateTap);

        

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

        private ProfileModel _selectedItem;
        public ProfileModel SelectedItem
        {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }

        private ObservableCollection<ProfileModel> _profileList; 

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

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if (args.PropertyName == nameof(SelectedItem))
            {
                SelectedItem.Name = FirstName;
                SelectedItem.Description = LastName;
            }
        }

        #endregion
        #region --- Privat Helpers ---
        private async void OnAddButtonTap(object obj)
        {
            var profile = new ProfileModel()
            {
                Author = _settingsManager.UserName, 
                Name = FirstName,
                Description = LastName,
                CreationTime = DateTime.Now
            };
            // await _repository.InsertAsync<ProfileModel>(profile);
            var id = await _repository.InsertAsync(profile);
            profile.Id = id;
            ProfileList.Add(profile);
           // await _repository.DeleteAllAsync<ProfileModel>(); // удаление таблицы
        }

        private async void OnUpdateTap(object obj)
        {
            if (SelectedItem != null)
            {

                var profile = new ProfileModel()
                {
                    Id = SelectedItem.Id,
                    Name = FirstName,
                    Description = LastName,
                    CreationTime = DateTime.Now
                };

                var index = ProfileList.IndexOf(SelectedItem);

                await _repository.UpdateAsync(profile);

                ProfileList.Remove(SelectedItem);

                ProfileList.Insert(index, profile);
            }
        }

        private async void OnDeleteTap()
        {
          
        }

        #endregion
    }
}
