using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace Picks.Infrastructure.Models
{
    public class Basket
    {
        internal const string customerBasketKey = "customer_basket";

        private List<Picture> _basketRows = new List<Picture>();

        public virtual void AddPicture(Picture p)
        {

            var cartRow = _basketRows.Where(x => x.Id.Equals(p.Id)).FirstOrDefault();
            if (cartRow == null)
            {
                _basketRows.Add(p);
            }
        }

        public virtual void RemovePicture(Guid id)
        {
            _basketRows.RemoveAll(x => x.Id.Equals(id));
        }

        public virtual void EmptyBasket()
        {
            _basketRows.Clear();
        }

        public virtual void DownloadZipOfPicturesInBasket(string EndpointStorageUrlOrBlobBaseUrl)
        {
            string zipFilePath = "wwwroot/pictures/pictures.zip";

            if (File.Exists(zipFilePath))
            {
                File.Delete(zipFilePath);
            }

            using (ZipArchive archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
            {
                foreach (var pic in _basketRows)
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile($"{EndpointStorageUrlOrBlobBaseUrl}{pic.FileName}", $"wwwroot{pic.FileName}");
                    }
                    /* Remove(0,10) is there to remove "/pictures/" in the beginning of every pic.FileName, without it the
                    generated zip-file will contain two unnecessary folders before you reach the images */
                    archive.CreateEntryFromFile($"wwwroot{pic.FileName}", pic.FileName.Remove(0,10));
                    if (File.Exists($"wwwroot{pic.FileName}"))
                    {
                        File.Delete($"wwwroot{pic.FileName}");
                    }
                }
            }
        }

        public virtual IEnumerable<Picture> BasketRows => _basketRows;
    }
}