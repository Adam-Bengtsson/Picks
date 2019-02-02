using Microsoft.EntityFrameworkCore;
using Picks.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.Infrastructure.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}