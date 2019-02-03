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

        public IActionResult Browse()
        {
            //var pictures = _pictureRepository.GetAllPictures();

            var vm = new BrowseViewModel
            {
                Categories = _pictureRepository.GetAllCategories(),
                Pictures = _pictureRepository.GetAllPictures()
            };
            return View(vm);

        }
    }
}