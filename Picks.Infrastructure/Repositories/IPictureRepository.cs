using Picks.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picks.Infrastructure.Repositories
{

    public interface IPictureRepository
    {
        IEnumerable<Picture> Pictures { get; }
        IEnumerable<Category> Categories { get; }

        void SaveCategory(Category c);
        void SavePicture(Picture p);

        IEnumerable<Picture> GetAllPictures();
        IEnumerable<Category> GetAllCategories();
    }
}