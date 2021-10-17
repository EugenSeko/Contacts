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
using Contacts.Services.Settings;
using Xamarin.Essentials;
using Contacts.Converters;
using Acr.UserDialogs;

namespace Contacts.ViewModels
{
    class AddEditProfileViewModel : BaseViewModel, INavigationAware
    {
        private readonly IProfileManager _profileManager;
        private readonly ISettingsManager _settingsManager;
        public AddEditProfileViewModel(INavigationService navigationService, 
                                       IProfileManager profileManager, 
                                       ISettingsManager settingsManager
                                                                        ) : base(navigationService)
        {
            _profileManager = profileManager;
            _settingsManager = settingsManager;
            _id = Global.Id;
             Init(_id);
        }

        private int _id;

        #region --- Public Properties ---

        private ProfileModel _profile;
        public ProfileModel Profile
        {
            get => _profile;
            set => SetProperty(ref _profile, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _nickname;
        public string NickName
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private string _imageurl="user.png";
        public string ImageUrl
        {
            get => _imageurl;
            set => SetProperty(ref _imageurl, value);
        }
        private DateTime _creationtime;
        public DateTime CreationTime
        {
            get => _creationtime;
            set => SetProperty(ref _creationtime, value);
        }
        #endregion
        #region --- Commands ---
        public ICommand OnSaveButton => new Command(Save);
        public ICommand OnPlusTapButton => new Command(GoToTestPage);
        public ICommand OnLArrowTapButton => new Command(GoToMainPage);
        public ICommand OnTapImage => new Command(ActionDialog);
        #endregion
        #region --- Private Helpers ---
        private async void Init(int id)
        {
            if(id >= 0)
            {
                Profile = await _profileManager.GetProfileById(_id);
                Name = Profile.Name;
                CreationTime = Profile.CreationTime;
                NickName = Profile.NickName;
                Description = Profile.Description;
                ImageUrl = Profile.ImageUrl;
            }
            else if(id ==-2)
            {
                Profile.Name = Name;
                Profile.NickName = NickName;
                Profile.CreationTime = DateTime.Now;
                Profile.Description = Description;
                Profile.ImageUrl = ImageUrl;
                Profile.Author = _settingsManager.UserName;
            }
        }
        private async void Save()
        {
            if(_id >= 0)// Update // текущее
            {
                Profile = new ProfileModel();
                Profile.Id = _id;
                Init(-2);
                await _profileManager.UpdateAsync(Profile);
            }
            else// Create
            {
                Profile = new ProfileModel();
                Init(-2);
                await _profileManager.CreateAsync(Profile);
            }
        }
        private void ActionDialog()
        {
            UserDialogs.Instance.ActionSheet(new ActionSheetConfig()
                           .SetTitle("Choose Type")
                           .Add("Capture", PickImage, "woman.png")
                           .Add("File", PickFile, "woman.png")
                           .SetUseBottomSheet(false));
        }
        private async void PickImage()
        {
             {
                try
                {
                    var result = await MediaPicker.CapturePhotoAsync();
                    if (result != null)
                        ImageUrl = result.FullPath;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
        private async void PickFile()
        {
            {
                try
                {
                    var result = await FilePicker.PickAsync(PickOptions.Default);
                    if (result != null)
                        ImageUrl = result.FullPath;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
        #endregion
        #region --- Navigation ---
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _id = parameters.GetValue<int>("id");
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        #endregion
    }
}
