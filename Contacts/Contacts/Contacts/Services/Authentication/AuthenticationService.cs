using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using System.Threading.Tasks;

namespace Contacts.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ISettingsManager _settingsManager;
        private readonly IRepository _repository;
        public AuthenticationService(IRepository repository, ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            _repository = repository;
        }
        public async Task<bool> RegistrationAsync(string username, string password)
        {
            bool isDone = true;
            var list = await _repository.GetAllAsync<UserModel>();
            foreach (var um in list)
            {
                if (um.UserName == username) 
                {
                    isDone = false;
                }
            }
            if (isDone) 
            {
                var user = new UserModel()
                {
                    UserName = username,
                    Password = password,
                };
                 await _repository.InsertAsync(user); // добавляем в базу
            }
            return await Task.Run(() => isDone);
        }
        public async Task<string> AuthorisatonAsync(string username, string password)
        {
            string done = "login_missing";
            var list = await _repository.GetAllAsync<UserModel>();
            foreach (var um in list)
            {
                if (um.UserName == username)
                {
                    done = "pass_missing";
                    if(um.Password == password)
                    {
                        done = "done";   
                        _settingsManager.UserName = username;
                        _settingsManager.SortBy = um.Sortby;
                        _settingsManager.Descending = um.Descending;
                    }
                }
            }
            return await Task.Run(() => done);
        }
        public async void ExitAuthorisation()
        {
            var list = await _repository.GetAllAsync<UserModel>();
            foreach (var um in list) 
            {
                if (um.UserName == _settingsManager.UserName)
                {
                    um.Sortby = _settingsManager.SortBy;
                    um.Descending = _settingsManager.Descending;
                    await _repository.UpdateAsync(um);
                }
            }
            _settingsManager.UserName = null;
            _settingsManager.Descending = "true";
            _settingsManager.SortBy = "CreationTime";
        }
    }
}
