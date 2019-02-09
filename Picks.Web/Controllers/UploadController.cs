using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Picks.Infrastructure.Models;
using Picks.Infrastructure.Repositories;
using Picks.Web.ViewModels;

namespace Picks.Web.Controllers
{
    public class UploadController : Controller
    {
        private readonly AzureStorageConfig _storageConfig;
        private IPictureRepository pictureRepo;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UploadController(IOptions<AzureStorageConfig> storageConfig, IPictureRepository p, IHostingEnvironment hostingEnvironment)
        {
            _storageConfig = storageConfig.Value;
            pictureRepo = p;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            if (pictureRepo.Categories.Count() == 0)
            {
                TempData["Info"] = "There are no categories, please create one";
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
                TempData["Success"] = $"Success! The category \"{u.Category.Name}\" has been added";
                return RedirectToAction(nameof(Upload));
            }
            else
            {
                return RedirectToAction(nameof(Upload));
            }
        }

        public async Task<IActionResult> UploadPictures(UploadViewModel u)
        {
            var pictures = Request.Form.Files;
            string[] formats = { ".jpg", ".png", ".jpeg" };

            foreach (var file in pictures)
            {
                if (formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase) == true))
                {
                    var pictureGuid = Guid.NewGuid();

                    StorageCredentials storageCredentials = new StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey);
                    CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);
                    CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer container = blobClient.GetContainerReference("pictures");
                    CloudBlockBlob blockBlob = container.GetBlockBlobReference(pictureGuid + "-" + file.FileName);

                    using (Stream fileStream = file.OpenReadStream())
                    {
                        await blockBlob.UploadFromStreamAsync(fileStream);
                    }

                    u.Picture.Id = pictureGuid;
                    u.Picture.FileName = "/pictures/" + pictureGuid + "-" + file.FileName;
                    u.Picture.CategoryId = u.Picture.CategoryId;
                    pictureRepo.SavePicture(u.Picture);
                    TempData["Success"] = $"Success! The pictures have been added";
                }
                else
                {
                    TempData["Info"] = "You have used a non-allowed file type, please use .jpg, .jpeg or .png files only";
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