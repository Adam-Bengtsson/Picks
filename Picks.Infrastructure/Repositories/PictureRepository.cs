using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Picks.Infrastructure.DataAccess;
using Picks.Infrastructure.Extensions;
using Picks.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Picks.Infrastructure.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private ApplicationDbContext ctx;
        private readonly IDistributedCache _cache;

        public PictureRepository(ApplicationDbContext context, IDistributedCache cache)
        {
            ctx = context;
            _cache = cache;
        }

        public IEnumerable<Picture> Pictures => ctx.Pictures;
        public IEnumerable<Category> Categories => ctx.Categories;

        public void SaveCategory(Category c)
        {
            c.Id = Guid.NewGuid();
            c.CreatedDate = DateTime.Now;
            ctx.Categories.Add(c);
            ctx.SaveChanges();
        }

        public void SavePicture(Picture p)
        {
            p.UploadDate = DateTime.Now;
            ctx.Pictures.Add(p);
            ctx.SaveChanges();
            _cache.Remove("startpageImages");
            _cache.SetValue("startpageImages", ctx.Pictures);
        }

        public IEnumerable<Picture> GetAllPictures()
        {
            var pictures = _cache.GetValue<IEnumerable<Picture>>("startpageImages");
            if (pictures != null)
            {
                return pictures;
            }
            pictures = ctx.Pictures;
            _cache.SetValue("startpageImages", pictures);
            return pictures;
        }

        //public IEnumerable<Picture> GetAllPictures()
        //{
        //    return ctx.Pictures;
        //}

        public IEnumerable<Category> GetAllCategories()
        {
            return ctx.Categories;
        }
    }
}