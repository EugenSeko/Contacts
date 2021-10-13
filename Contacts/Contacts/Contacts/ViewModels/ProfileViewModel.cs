using Contacts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Contacts.ViewModels
{
    class ProfileViewModel : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ProfileModel ProfileModel { get; set; }
        public MainListViewModel ListViewModel { get; set; }

        public ProfileViewModel()
        {
            ProfileModel = new ProfileModel();
        }

        public int Id
        {
            get { return ProfileModel.Id; }
            set
            {
                if (ProfileModel.Id != value)
                {
                    ProfileModel.Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        public string Author
        {
            get { return ProfileModel.Author; }
            set
            {
                if (ProfileModel.Author != value)
                {
                    ProfileModel.Author = value;
                    OnPropertyChanged("Author");
                }
            }
        }

        public string NickName
        {
            get { return ProfileModel.NickName; }
            set
            {
                if (ProfileModel.NickName != value)
                {
                    ProfileModel.NickName = value;
                    OnPropertyChanged("NickName");
                }
            }
        }

        public string FirstName
        {
            get { return ProfileModel.FirstName; }
            set
            {
                if (ProfileModel.FirstName != value)
                {
                    ProfileModel.FirstName = value;
                    OnPropertyChanged("FirstName");
                }
            }
        }
        public string LastName
        {
            get { return ProfileModel.LastName; }
            set
            {
                if (ProfileModel.LastName != value)
                {
                    ProfileModel.LastName = value;
                    OnPropertyChanged("LastName");
                }
            }
        }
        public string ImageUrl
        {
            get { return ProfileModel.ImageUrl; }
            set
            {
                if (ProfileModel.ImageUrl != value)
                {
                    ProfileModel.ImageUrl = value;
                    OnPropertyChanged("ImageUrl");
                }
            }
        }
        public DateTime CreationTime
        {
            get { return ProfileModel.CreationTime; }
            set
            {
                if (ProfileModel.CreationTime != value)
                {
                    ProfileModel.CreationTime = value;
                    OnPropertyChanged("CreationTime");
                }
            }
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
