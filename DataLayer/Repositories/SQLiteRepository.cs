using DataLayer.EF;
using DataLayer.Entities;
using DataLayer.Interfaces;
using System.Collections.Generic;
using System;

namespace DataLayer.Repositories
{
    public class SQLiteRepository : IRepository<UserUrl>, IDisposable
    {
        readonly UrlDbContext _db;
        bool _disposed = false;

        public SQLiteRepository(UrlDbContext context)
        {
            _db = context;
        }

        public IEnumerable<UserUrl> GetAll()
        {
            return _db.Urls;
        }

        public UserUrl GetItem(int id)
        {
            return _db.Urls.Find(id);
        }

        public void AddItem(UserUrl url)
        {
            _db.Urls.Add(url);
        }

        public void EditItem(UserUrl url)
        {
            _db.Entry(url).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void DeleteItem(int id)
        {
            UserUrl url = GetItem(id);
            if (url != null)
                _db.Urls.Remove(url);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _db.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
