using Contacts.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
   public class ProfileViewModel : BindableBase
    {
        #region --- Public Properties ---
        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);

        }

        private string _author;
        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
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

        private string _imageurl;
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

        #region --- Command ---
        private ICommand _deletecommand;
        public ICommand DeleteCommand
        {
            get => _deletecommand;
            set => SetProperty(ref _deletecommand, value);

        }
        private ICommand _editcommand;
        public ICommand EditCommand
        {
            get => _editcommand;
            set => SetProperty(ref _editcommand, value);

        }
        #endregion

    }
}
