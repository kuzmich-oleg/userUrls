using System.Linq;
using System.Collections.Generic;
using DataLayer.Interfaces;
using DataLayer.Entities;
using BusinessLayer.DTO;
using BusinessLayer.Infrastructure;

namespace BusinessLayer.Services
{
    public class UrlService
    {
        IRepository<UserUrl> _repository;

        public UrlService(IRepository<UserUrl> repository)
        {
            _repository = repository;
        }

        public IEnumerable<UrlDTO> GetUrls()
        {
            return _repository.GetAll().Select(
                url => new UrlDTO
                {
                    Id = url.Id,
                    Content = url.Content,
                    Url = url.Url
                });
        }

        public UrlDTO GetUrl(int id)
        {
            UserUrl userUrl = _repository.GetItem(id);

            if (userUrl == null)
                throw new ValidationException("Url not found", "");

            return new UrlDTO { Id = userUrl.Id, Content = userUrl.Content, Url = userUrl.Url };
        }

        public void AddUrl(UrlDTO url)
        {
            if (_repository.GetItem(url.Id) != null)
                throw new ValidationException("The url already exists", "");

            _repository.AddItem(new UserUrl { Id = url.Id, Content = url.Content, Url = url.Url });
            _repository.Save();
        }

        public void EditUrl(UrlDTO url)
        {
            UserUrl userUrl = _repository.GetItem(url.Id);
            if (userUrl == null)
                throw new ValidationException("Url not found", "");

            userUrl.Url = url.Url;
            userUrl.Content = url.Content;

            _repository.EditItem(userUrl);
            _repository.Save();
        }

        public string GetContentByUrl(string url)
        {
            string content = "";
            foreach (var u in _repository.GetAll())
                if (u.Url == url)
                    content = u.Content;

            while (content.Contains("_l_"))
                content = content.Replace("_l_", "\n");

            return content;
        }

        public void DeleteUrl(int id)
        {
            if (_repository.GetItem(id) == null)
                throw new ValidationException("Url not found", "");

            _repository.DeleteItem(id);
            _repository.Save();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
