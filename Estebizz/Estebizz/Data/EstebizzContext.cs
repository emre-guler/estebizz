using Estebizz.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estebizz.Data
{
    public class EstebizzContext : DbContext
    {
        public EstebizzContext (DbContextOptions options) : base (options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}
