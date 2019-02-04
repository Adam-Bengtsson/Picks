using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Picks.Infrastructure.Models;
using Picks.Infrastructure.Repositories;
using Picks.Web.ViewModels;

namespace Picks.Web.Controllers
{
    public class UploadController : Controller
    {
        private IPictureRepository pictureRepo;
        private IHostingEnvironment _hostingEnvironment;

        public UploadController(IPictureRepository p, IHostingEnvironment hostingEnvironment)
        {
            pictureRepo = p;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            if (pictureRepo.Categories.Count() == 0)
            {
                TempData["Info"] = "There are no categories";
            }

            var u = new UploadViewModel
            {
                Category = new Category(),
                Categories = ToSelectList(pictureRepo.Categories.OrderBy(x => x.Name))
            };

            return View(u);
        }

        [HttpPost]
        public IActionResult CreateCategory(UploadViewModel u)
        {
            if (ModelState.IsValid)
            {
                pictureRepo.SaveCategory(u.Category);
                TempData["Success"] = $"Succé! Kategori \"{u.Category.Name}\" har lagts till";
                return RedirectToAction(nameof(Upload));
            }
            else
            {
                return RedirectToAction(nameof(Upload));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadPictures(UploadViewModel u)
        {
            var pictures = Request.Form.Files;

            foreach (var file in pictures)
            {
                if (file.Length > 0)
                {
                    var pictureGuid = Guid.NewGuid();

                    using (var stream = new FileStream(_hostingEnvironment.WebRootPath + "\\pictures\\" + pictureGuid + "-" + file.FileName, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                        u.Picture.Id = pictureGuid;
                        u.Picture.FileName = pictureGuid + "-" + file.FileName;
                        u.Picture.CategoryId = u.Picture.CategoryId;
                        pictureRepo.SavePicture(u.Picture);
                    }
                }
            }
            return RedirectToAction(nameof(Upload));
        }

        public List<SelectListItem> ToSelectList(IEnumerable<Category> categories, Category c = null)
        {
            var list = categories.Select(
                x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = (x.Id == c?.Id)
                }).ToList();
            return list;
        }
    }
}