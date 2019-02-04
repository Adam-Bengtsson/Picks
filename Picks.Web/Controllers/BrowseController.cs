using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picks.Infrastructure.Models;
using Picks.Infrastructure.Repositories;
using Picks.Web.ViewModels;

namespace Picks.Web.Controllers
{
    public class BrowseController : Controller
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly Basket _basket;

        public BrowseController(IPictureRepository pictureRepo, Basket basketService)
        {
            _pictureRepository = pictureRepo;
            _basket = basketService;

        }

        public IActionResult Browse(string categoryName)
        {
            if (categoryName == null)
            {
                var vm = new BrowseViewModel
                {
                    Categories = _pictureRepository.GetAllCategories().OrderBy(x => x.Name),
                    Pictures = _pictureRepository.GetAllPictures().OrderByDescending(x => x.UploadDate)
                };
                return View(vm);
            }
            else
            {
                var vm = new BrowseViewModel
                {
                    Categories = _pictureRepository.GetAllCategories().OrderBy(x => x.Name),
                    Pictures = _pictureRepository.GetAllPictures().Where(x => x.Category.Name == categoryName).OrderByDescending(x => x.UploadDate)
                };
                return View(vm);
            }
        }

        [HttpPost]
        public RedirectToActionResult AddToBasket(Guid pictureId)
        {
            var p = _pictureRepository.Pictures.FirstOrDefault(x => x.Id.Equals(pictureId));
            if (p != null)
            {
                _basket.AddPicture(p);
            }

            return RedirectToAction(nameof(Browse));
        }
    }
}