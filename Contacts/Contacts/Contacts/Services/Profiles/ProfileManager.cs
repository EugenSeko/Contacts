using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Contacts.Services.Profiles
{
   public class ProfileManager : IProfileManager
    {

        private ISettingsManager _settingsManager;
        private IRepository _repository;

        public ProfileManager(ISettingsManager settingsManager, IRepository repository)
        {
            _settingsManager = settingsManager;
            _repository = repository;
        }

        #region --- Public Properties ---
        public int Id 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        public string Author 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException();
        }

        public string NicktName
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException();
        }

        public string FirstName 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException();
        }

        public string LastName 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException();
        }

        public string ImageUrl 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException();
        }
        public DateTime CreationTime 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException();
        }

        public ObservableCollection<ProfileModel> ProfileList 
        { 
            get; 
            set; 
        }

        #endregion

        public async void GetAllProfiles() // получаем все профили определенного юзера
        {
            var profileList = await _repository.GetAllAsync<ProfileModel>();

            ProfileList = new ObservableCollection<ProfileModel>();

            var v = profileList.Where(p => p.Author == _settingsManager.UserName);

            foreach(ProfileModel m in v)
            {
                ProfileList.Add(m);
                Console.WriteLine(m.Author); //временно
                Console.WriteLine(m.FirstName);
                Console.WriteLine(m.LastName);
                Console.WriteLine(m.ImageUrl);
                Console.WriteLine(m.Id);
                Console.WriteLine(m.NicktName);

            }

        }
    }
}
