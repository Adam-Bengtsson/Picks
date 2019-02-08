using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Picks.Infrastructure.Models;
using Picks.Infrastructure.Repositories;

namespace Picks.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly IPictureRepository _pictureRepository;
        private readonly Basket _basket;
        private readonly AzureStorageConfig _storageConfig;

        public BasketController(IOptions<AzureStorageConfig> storageConfig, IPictureRepository pictureRepo, Basket basketService)
        {
            _storageConfig = storageConfig.Value;
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
            //StorageCredentials storageCredentials = new StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey);
            //CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);
            //CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            //CloudBlobContainer container = blobClient.GetContainerReference("pictures");
            //CloudBlockBlob blockBlob = container.GetBlockBlobReference("Picture.zip");

            //using (Stream fileStream = file.OpenReadStream())
            //{
            //    await blockBlob.UploadFromStreamAsync(fileStream);
            //}

            _basket.DownloadZipOfPicturesInBasket();

            TempData["Success"] = "Yes!";

            return RedirectToAction(nameof(Basket));
        }

        //public ActionResult Download()
        //{
        //    var cloudStorageAccount = new CloudStorageAccount(new StorageCredentials(_storageConfig.AccountName, _storageConfig.AccountKey), true);
        //    var container = cloudStorageAccount.CreateCloudBlobClient().GetContainerReference("pictures");
        //    var blobFileNames = new string[] { "file1.png", "file2.png", "file3.png", "file4.png" };
        //    using (var zipOutputStream = new ZipOutputStream(Response.OutputStream))
        //    {
        //        foreach (var blobFileName in blobFileNames)
        //        {
        //            zipOutputStream.SetLevel(0);
        //            var blob = container.GetBlockBlobReference(blobFileName);
        //            var entry = new ZipEntry(blobFileName);
        //            zipOutputStream.PutNextEntry(entry);
        //            blob.DownloadToStream(zipOutputStream);
        //        }
        //        zipOutputStream.Finish();
        //        zipOutputStream.Close();
        //    }
        //    Response.BufferOutput = false;
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + "Pictures.zip");
        //    Response.ContentType = "application/octet-stream";
        //    Response.Flush();
        //    Response.End();
        //    return null;
        //}
    }
}