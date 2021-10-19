using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace Contacts.Dialogs
{
    class ImageDialogViewModel : BindableBase, IDialogAware
    {
        public ImageDialogViewModel()
        {
            CloseCommand = new DelegateCommand(() => RequestClose(null));
        }
        #region --- Properties ---
        private string _message;
        public string Name
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
        private string _imageurl;
        public string ImageUrl
        {
            get => _imageurl;
            set => SetProperty(ref _imageurl, value);
        }
        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        #endregion
        public DelegateCommand CloseCommand { get; }
        public event Action<IDialogParameters> RequestClose;
        public bool CanCloseDialog() => true;
        public void OnDialogClosed()
        {
            throw new NotImplementedException();
        }
        public void OnDialogOpened(IDialogParameters parameters)
        {
            Name = parameters.GetValue<string>("name");
            ImageUrl = parameters.GetValue<string>("imageurl");
            Description = parameters.GetValue<string>("description");
        }
    }
}
