using Microsoft.AspNetCore.Mvc.Rendering;
using Picks.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picks.Web.ViewModels
{
    public class UploadViewModel
    {
        public Picture Picture { get; set; }
        public Category Category { get; set; }
        public List<SelectListItem> Categories { get; set; }
    }
}