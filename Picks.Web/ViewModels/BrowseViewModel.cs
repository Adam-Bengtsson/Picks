using Picks.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picks.Web.ViewModels
{
    public class BrowseViewModel
    {
        public IEnumerable<Picture> Pictures { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}