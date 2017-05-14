using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
namespace DAL
{
    public class FontickDbContext:DbContext
    {
        public FontickDbContext():base("DefaultConnection")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public static FontickDbContext Create() => new FontickDbContext();
    }
}
