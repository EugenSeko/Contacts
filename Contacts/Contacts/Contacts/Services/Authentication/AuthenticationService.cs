using Contacts.Models;
using Contacts.Services.Repository;
using Contacts.Services.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Contacts.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        // Подтягиваем вспомогательные сервисы.
        private ISettingsManager _settingsManager;
        private IRepository _repository;

        public AuthenticationService(IRepository repository, ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
            _repository = repository;
        }

        public  async void GetAllUsers() //временно
        {
            var list =   await _repository.GetAllAsync<UserModel>();
            
            foreach(var um in list)
            {
            Console.WriteLine(um.Id);
            Console.WriteLine(um.UserName);
            Console.WriteLine(um.Password);

            }
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

               // await _repository.DeleteAllAsync<UserModel>(); //временно 
               
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

                        _settingsManager.UserName = username; // Сохраняем в настройки
                    }
                }
            }

            return await Task.Run(() => done);
        }
    }
}
