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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Image>().HasMany(p => p.Tags).WithMany(p => p.Images).Map(p => p.MapLeftKey("image_id").MapRightKey("tags_id").ToTable("ImageTags"));
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public static FontickDbContext Create() => new FontickDbContext();
    }
}
