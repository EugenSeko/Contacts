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
        
        public ProfileModel Profile 
        { 
            get; 
            set; 
        }

        public ObservableCollection<ProfileModel> ProfileList 
        { 
            get; 
            set; 
        }

        #endregion

        public async Task<ObservableCollection<ProfileModel>> GetAllProfilesAsync() // получаем все профили определенного юзера
        {
            var profileList = await _repository.GetAllAsync<ProfileModel>();

            ProfileList = new ObservableCollection<ProfileModel>();

            var v = profileList.Where(p => p.Author == _settingsManager.UserName);

            foreach(ProfileModel m in v)
            {
                ProfileList.Add(m);
                //Console.WriteLine(m.Author); //временно
                //Console.WriteLine(m.FirstName);
                //Console.WriteLine(m.LastName);
                //Console.WriteLine(m.ImageUrl);
                //Console.WriteLine(m.Id);
                //Console.WriteLine(m.NicktName);

            }

            return ProfileList;

        }
    }
}
