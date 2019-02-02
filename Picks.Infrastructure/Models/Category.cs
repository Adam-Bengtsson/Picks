using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Picks.Infrastructure.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "You must submit a category name")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}