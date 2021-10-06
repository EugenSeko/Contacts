using Contacts.Services.Settings;
using Prism.Mvvm;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contacts.ViewModels
{
    class TestPageViewModel:BindableBase
    {
        private ISettingsManager _settingsManager;
        public TestPageViewModel(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            Name = _settingsManager.Name;
            Count = _settingsManager.Count;
        }
        #region --- Public Properties ---
        private string _name;
        private int _count;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }
        public ICommand IncrementButtonTapCommand => new Command(Increment);
        public ICommand ResetButtonTapCommand => new Command(Reset);
        #endregion
        #region --- Overrides ---
        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            if(args.PropertyName == nameof(Name))
            {
                _settingsManager.Name = Name;
            }
            if(args.PropertyName == nameof(Count))
            {
                _settingsManager.Count = Count;
            }
        }
        #endregion
        #region --- Privat Helpers ---
        private void Increment()
        {
            Count++;
            Name += "!";
        }
        private void Reset()
        {
            Count=0;
            Name = "";
        }
        #endregion
    }
}
