using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using System.IO;

namespace Contacts.Services.Repository
{
    public class Repository : IRepository
    {
         private Lazy<SQLiteAsyncConnection> _database;
        public Repository()
        {
            _database = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment
                                         .SpecialFolder.LocalApplicationData), "contacts.bd");
                var database = new SQLiteAsyncConnection(path);
                database.CreateTableAsync<ProfileModel>().Wait();
                database.CreateTableAsync<UserModel>().Wait();
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
        }
        #endregion
    }
}
