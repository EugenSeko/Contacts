﻿using Contacts.Services.Settings;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    class SettingsViewModel:BaseViewModel
    {
        private readonly ISettingsManager _settingsManager;
        public SettingsViewModel(INavigationService navigationService,
                                 ISettingsManager settingsManager) :base(navigationService)
        {
            _settingsManager = settingsManager;
            RadioButtonsInit();
        }

        public ICommand OnSaveButton => new Command(Save);
        public ICommand LeftArrowCommand => new Command(ExitPage);

        #region --- Public Properties ---
        private string _selectedsorting;
        public string SelectedSorting
        {
            get => _selectedsorting;
            set => SetProperty(ref _selectedsorting, value);
        }

        private string _selecteddescending;
        public string SelectedDescending
        {
            get => _selecteddescending;
            set => SetProperty(ref _selecteddescending, value);
        }

        private bool _isnamechecked;
        public bool IsNameChecked
        {
            get => _isnamechecked;
            set => SetProperty(ref _isnamechecked, value);
        }

        private bool _isnicknamechecked;
        public bool IsNickNameChecked
        {
            get => _isnicknamechecked;
            set => SetProperty(ref _isnicknamechecked, value);
        }

        private bool _isCreationTimechecked;
        public bool IsCreationTimechecked
        {
            get => _isCreationTimechecked;
            set => SetProperty(ref _isCreationTimechecked, value);
        }

        private bool _isdescendchecked;
        public bool IsDescendChecked
        {
            get => _isdescendchecked;
            set => SetProperty(ref _isdescendchecked, value);
        }

        private bool _isascendchecked;
        public bool IsAscendChecked
        {
            get => _isascendchecked;
            set => SetProperty(ref _isascendchecked, value);
        }
        #endregion
        #region --- Privat Helpers ---
        private void RadioButtonsInit()
        {
            switch (_settingsManager.Descending)
            {
                case "true":
                    IsDescendChecked = true;
                    break;
                case "false":
                    IsAscendChecked = true;
                    break;
            }
            switch (_settingsManager.SortBy)
            {
                case "Name":
                    IsNameChecked = true;
                    break;
                case "NickName":
                    IsNickNameChecked = true;
                    break;
                case "CreationTime":
                    IsCreationTimechecked = true;
                    break;
            }
        }
        private void ExitPage()
        {
            Save();
            GoToMainPage();
        }
        private void Save()
        {
            _settingsManager.SortBy = SelectedSorting != null ? SelectedSorting : "CreationTime";
            _settingsManager.Descending = SelectedDescending != null ? SelectedDescending : "true";
        }
        #endregion
    }
}
