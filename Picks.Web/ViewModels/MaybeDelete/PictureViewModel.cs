using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picks.Web.ViewModels
{
    public class PictureViewModel
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string Tags { get; set; }
        public Guid CategoryId { get; set; }
    }
}