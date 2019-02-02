using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Picks.Infrastructure.Models
{
    public class Picture
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public string Tags { get; set; }

        [Required(ErrorMessage = "You must select a category for the picture")]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}