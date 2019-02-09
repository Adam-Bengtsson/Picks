using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Picks.Infrastructure.Models;
using Picks.Infrastructure.Repositories;

namespace Picks.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly Basket _basket;
        private readonly CdnSettings _cdnSettings;
        private readonly AzureStorageConfig _storageConfig;

        public BasketController(IOptions<AzureStorageConfig> storageConfig, IOptions<CdnSettings> cdnSettings, IPictureRepository pictureRepo, Basket basketService)
        {
            _storageConfig = storageConfig.Value;
            _cdnSettings = cdnSettings.Value;
            _pictureRepository = pictureRepo;
            _basket = basketService;
        }

        public IActionResult Basket()
        {
            return View(_basket);
        }

        public RedirectToActionResult RemoveFromBasket(Guid pictureId)
        {
            _basket.RemovePicture(pictureId);

            return RedirectToAction(nameof(Basket));
        }

        public RedirectToActionResult EmptyBasket()
        {
            _basket.EmptyBasket();

            return RedirectToAction(nameof(Basket));
        }

        public RedirectToActionResult DownloadZipOfPicturesInBasket()
        {
            if (_cdnSettings.Enabled)
            {
                _basket.DownloadZipOfPicturesInBasket(_cdnSettings.EndpointStorageUrl);
            }

            else
            {
                _basket.DownloadZipOfPicturesInBasket(_storageConfig.BlobBaseUrl);
            }

            TempData["Success"] = "Yes!";

            return RedirectToAction(nameof(Basket));
        }
    }
}