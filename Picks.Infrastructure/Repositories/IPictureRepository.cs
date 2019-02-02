using Picks.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.Infrastructure.Repositories
{

    public interface IPictureRepository
    {
        IEnumerable<Picture> Pictures { get; }
        IEnumerable<Category> Categories { get; }
    }
}