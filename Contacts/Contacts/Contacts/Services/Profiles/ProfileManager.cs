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

        public List<ProfileModel> ProfileList 
        { 
            get; 
            set; 
        }

        #endregion

        public async Task<List<ProfileModel>> GetAllProfilesAsync() // получаем все профили определенного юзера
        {
            var profileList = await _repository.GetAllAsync<ProfileModel>();

            ProfileList = new List<ProfileModel>();

            var v = profileList.Where(p => p.Author == _settingsManager.UserName);

            foreach(ProfileModel m in v)
            {
                ProfileList.Add(m);
            }

            return ProfileList;

        }
    }
}
