using System.Collections.Generic;
using System;

namespace DataLayer.Interfaces
{
    public interface IRepository<T> :IDisposable where T : class 
    {
        IEnumerable<T> GetAll();
        T GetItem(int id);
        void AddItem(T item);
        void EditItem(T item);
        void DeleteItem(int id);
        void Save();
    }
}
