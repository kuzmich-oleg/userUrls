using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UserUrls.Models;
using BusinessLayer.Services;
using BusinessLayer.Infrastructure;
using BusinessLayer.DTO;

namespace UserUrls.Controllers
{
    public class HomeController : Controller
    {
        UrlService _service;

        public HomeController(UrlService urlService)
        {
            _service = urlService;
        }

        public IActionResult Index()
        {
            return View(ConvertDTO(_service.GetUrls()));
        }

        public IActionResult LoadEditView()
        {
            return PartialView("EditView", new UrlViewModel());
        }

        [HttpPost]
        public IActionResult Save(string url, string content)
        {
            try
            {
                _service.AddUrl(new UrlDTO() { Url = url, Content = content });

                return PartialView("UrlList", ConvertDTO(_service.GetUrls()));
            }
            catch (ValidationException exc)
            {
                return View("Error", exc.Message);
            }            
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.DeleteUrl(id);

                return PartialView("UrlList", ConvertDTO(_service.GetUrls()));
            }
            catch (ValidationException exc)
            {
                return View("Error", exc.Message);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var url = _service.GetUrl(id);
                UrlViewModel model = new UrlViewModel(id, url.Url, url.Content);

                return PartialView("EditView", model);
            }
            catch (ValidationException exc)
            {
                return View("Error", exc.Message);
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, string url, string content)
        {
            try
            {
                _service.EditUrl(new UrlDTO { Id = id, Url = url, Content = content });

                return PartialView("UrlList", ConvertDTO(_service.GetUrls()));
            }
            catch (ValidationException exc)
            {
                return View("Error", exc.Message);
            }
        }

        private IEnumerable<UrlViewModel> ConvertDTO(IEnumerable<UrlDTO> urls)
        {
            var viewModels = new List<UrlViewModel>();

            foreach (var url in urls)
                viewModels.Add(new UrlViewModel(url.Id, url.Url, url.Content));     
            
            return viewModels;
        }

        protected override void Dispose(bool disposing)
        {
            _service.Dispose();
            base.Dispose(disposing);
        }
    }
}
