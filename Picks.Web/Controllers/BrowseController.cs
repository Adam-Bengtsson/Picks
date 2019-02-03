using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picks.Infrastructure.Repositories;
using Picks.Web.ViewModels;

namespace Picks.Web.Controllers
{
    public class BrowseController : Controller
    {
        private readonly IPictureRepository _pictureRepository;

        public BrowseController(IPictureRepository pictureRepo)
        {
            _pictureRepository = pictureRepo;
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
    }
}