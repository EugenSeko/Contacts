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

        // private Lazy<SQLiteAsyncConnection> _database;
        private SQLiteAsyncConnection _database;



        public Repository()
        {

            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contacts.bd");
            _database = new SQLiteAsyncConnection(path);
            _database.CreateTableAsync<ProfileModel>();
            _database.CreateTableAsync<UserModel>();

            //_database = new Lazy<SQLiteAsyncConnection(() => 
            //{
            //    // Путь к базе на локальном устройстве
            //    var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "contacts.bd");
            //    var database = new SQLiteAsyncConnection(path);

            //    // инициализируем таблицы
            //    database.CreateTableAsync<ProfileModel>();
            //    database.CreateTableAsync<UserModel>();
            //    return database;
            //});
        }

        #region --- Common Methods ---

        public Task<int> DeleteAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.DeleteAsync(entity);
        }

        public Task<List<T>> GetAllAsync<T>() where T : IEntityBase, new()
        {
            return _database.Table<T>().ToListAsync();
        }

        public Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.InsertAsync(entity);
        }

        public Task<int> UpdateAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.UpdateAsync(entity);
        }
        public Task<int> DeleteAllAsync<T>() where T : IEntityBase, new()
        {
            return _database.DeleteAllAsync<T>();
            // return _database.Value.DropTableAsync<T>();
        }

        #endregion
        
    }
}
