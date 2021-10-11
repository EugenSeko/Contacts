using Contacts.Services.Authentication;
using Contacts.Services.Profiles;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using Contacts.ViewModels;
using Contacts.Views;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts
{
    public partial class App : PrismApplication
    {
        public App()
        {
           
        }
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
            containerRegistry.RegisterForNavigation<TestPage, TestPageViewModel>();
            containerRegistry.RegisterForNavigation<MainListView, MainListViewModel>();
            containerRegistry.RegisterForNavigation<SignUpView, SignUpViewModel>();
            containerRegistry.RegisterForNavigation<AddEditProfileView, AddEditProfileViewModel>();
        }

        protected override void OnInitialized()
        {
            InitializeComponent();
            // NavigationService.NavigateAsync($"{nameof(TestPage)}");


            // NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainListView)}");

            var settingsManager = Container.Resolve<ISettingsManager>();

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
