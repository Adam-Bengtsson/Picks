using Picks.Infrastructure.DataAccess;
using Picks.Infrastructure.Models;
using System;
using System.Collections.Generic;
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
    }
}