using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using Contacts.Services.Settings;

namespace Contacts.Services.Repository
{
    public class Repository : IRepository
    {
        
        private Lazy<SQLiteAsyncConnection> _database;


        public Repository()
        {

            _database = new Lazy<SQLiteAsyncConnection>(() => 
            {
                // Путь к базе на локальном устройстве
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contacts.bd");
                var database = new SQLiteAsyncConnection(path);

                // инициализируем таблицы
                database.CreateTableAsync<ProfileModel>();
                database.CreateTableAsync<UserModel>();
                return database;
            });
        }

        #region --- Common Methods ---

        public Task<int> DeleteAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.Value.DeleteAsync(entity);
        }

        public Task<List<T>> GetAllAsync<T>() where T : IEntityBase, new()
        {
            return _database.Value.Table<T>().ToListAsync();
        }

        public Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.Value.InsertAsync(entity);
        }

        public Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.Value.UpdateAsync(entity);
        }
        public Task<int> DeleteAllAsync<T>() where T : IEntityBase, new()
        {
            return _database.Value.DeleteAllAsync<T>();
            // return _database.Value.DropTableAsync<T>();
        }

        #endregion
        
    }
}
