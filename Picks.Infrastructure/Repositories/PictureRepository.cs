using Microsoft.Extensions.Configuration;
using Picks.Infrastructure.DataAccess;
using Picks.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Picks.Infrastructure.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private ApplicationDbContext ctx;

        public PictureRepository(ApplicationDbContext context)
        {
            ctx = context;
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
        }

        public IEnumerable<Picture> GetAllPictures()
        {
            return ctx.Pictures;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return ctx.Categories;
        }
    }
}