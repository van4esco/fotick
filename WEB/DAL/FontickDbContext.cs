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
            Database.CreateIfNotExists();
            Database.SetInitializer(new DropCreateDatabaseAlways<FontickDbContext>());
        }
   
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public static FontickDbContext Create() => new FontickDbContext();
    }
}
