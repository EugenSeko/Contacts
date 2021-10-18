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
        private string _name;
        public string Name
        {
            get => _settingsManager.UserName;
            set { _settingsManager.UserName = value; }
        }

        #endregion


        public async Task<int>  DeleteAsync(ProfileModel profile)
        {
            return await _repository.DeleteAsync(profile);
        }

        public async Task<int> CreateAsync(ProfileModel profile)
        {
           return await _repository.InsertAsync<ProfileModel>(profile);
        }
        public async Task<int> UpdateAsync(ProfileModel profile)
        {
            return await _repository.UpdateAsync(profile);
        }
        public async Task<ProfileModel> GetProfileById(int id)
        {
           ProfileList = await GetAllProfilesAsync();
            ProfileModel pm = new ProfileModel(); // null?
            foreach(ProfileModel m in ProfileList)
            {
                if (m.Id == id) pm = m;
            }
            return pm;
        }

        public async Task<List<ProfileModel>> GetAllProfilesAsync() // получаем все профили определенного юзера
        {
            var profileList = await _repository.GetAllAsync<ProfileModel>();

            ProfileList = new List<ProfileModel>();
            //-------------
            var sortby = _settingsManager.SortBy;
            var descending = _settingsManager.Descending;
            if (descending == "true")
            {
                switch (sortby)
                {
                    case "Name":
                        var nm = profileList
                .Where(p => p.Author == _settingsManager.UserName)
                .OrderByDescending(x => x.Name);
                        foreach (ProfileModel m in nm)
                        {
                            ProfileList.Add(m);
                        }
                        break;
                    case "NickName":
                        var nn = profileList
                .Where(p => p.Author == _settingsManager.UserName)
                .OrderByDescending(x => x.NickName);
                        foreach (ProfileModel m in nn)
                        {
                            ProfileList.Add(m);
                        }
                        break;
                    case "CreationTime":
                        var ct = profileList
                .Where(p => p.Author == _settingsManager.UserName)
                .OrderByDescending(x => x.CreationTime);
                        foreach (ProfileModel m in ct)
                        {
                            ProfileList.Add(m);
                        }
                        break;
                }
            }
            else
            {
                switch (sortby)
                {
                    case "Name":
                        var nm = profileList
                .Where(p => p.Author == _settingsManager.UserName)
                .OrderBy(x => x.Name);
                        foreach (ProfileModel m in nm)
                        {
                            ProfileList.Add(m);
                        }
                        break;
                    case "NickName":
                        var nn = profileList
                .Where(p => p.Author == _settingsManager.UserName)
                .OrderBy(x => x.NickName);
                        foreach (ProfileModel m in nn)
                        {
                            ProfileList.Add(m);
                        }
                        break;
                    case "CreationTime":
                        var ct = profileList
                .Where(p => p.Author == _settingsManager.UserName)
                .OrderBy(x => x.CreationTime);
                        foreach (ProfileModel m in ct)
                        {
                            ProfileList.Add(m);
                        }
                        break;
                }
            }
            
            return ProfileList;

        }
    }
}
