using DataLayer.Entities;
using DataLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataLayer.Repositories
{
    public class MockRepository : IRepository<UserUrl>
    {
        static ICollection<UserUrl> _urls = new List<UserUrl>();
        static int _id = 1;

        public IEnumerable<UserUrl> GetAll()
        {
            return _urls.ToList();
        }

        public UserUrl GetItem(int id)
        {
            if(id != 0)
                return _urls.Where(url => url.Id == id).First();
            return null;
        }

        public void AddItem(UserUrl item)
        {
            item.Id = _id;
            _id++;

            _urls.Add(item);
        }

        public void EditItem(UserUrl item)
        {
            UserUrl url = GetItem(item.Id);
            if (url != null)
            {
                url.Content = item.Content;
                url.Url = item.Url;
            }
        }

        public void DeleteItem(int id)
        {
            UserUrl url = GetItem(id);
            if (url != null)
                _urls.Remove(url);
        }

        public void Dispose() { }
        public void Save() { }
    }
}
