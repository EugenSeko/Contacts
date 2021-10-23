using Contacts.Services.Authentication;
using Contacts.Services.Profiles;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using Contacts.ViewModels;
using Contacts.Views;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

namespace Contacts
{
    public partial class App : PrismApplication
    {
        public App() { }
        #region ---Overrides---
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.RegisterInstance<IRepository>(Container.Resolve<Repository>());
            containerRegistry.RegisterInstance<ISettingsManager>(Container.Resolve<SettingsManager>());
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            containerRegistry.RegisterInstance<IProfileManager>(Container.Resolve<ProfileManager>());
            // Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignInView, SignInViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
            //Dialogs
            containerRegistry.RegisterDialog<Dialogs.ImageDialog, Dialogs.ImageDialogViewModel>();
        }
        protected override void OnInitialized()
        {
            InitializeComponent();
            //  NavigationService.NavigateAsync($"/{nameof(SettingsView)}");
            var settingsManager = Container.Resolve<ISettingsManager>();
            Converters.Global.ThemeStyle = settingsManager.ThemeStyle;

            if (settingsManager.UserName == null)
            {
                NavigationService.NavigateAsync("/" + nameof(SignInView));
            }
            else
            {
                NavigationService.NavigateAsync("/" + nameof(MainListView));
            }
        }
        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
        #endregion
    }
}
