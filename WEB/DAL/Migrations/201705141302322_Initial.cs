namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Url = c.String(),
                        UserId = c.Guid(nullable: false),
                        AestheticsStatus = c.String(),
                        AestheticsPersent = c.String(),
                        IsForSale = c.Boolean(nullable: false),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(),
                        Login = c.String(),
                        AddedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagImages",
                c => new
                    {
                        Tag_Id = c.Guid(nullable: false),
                        Image_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Image_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Images", t => t.Image_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Image_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "UserId", "dbo.Users");
            DropForeignKey("dbo.TagImages", "Image_Id", "dbo.Images");
            DropForeignKey("dbo.TagImages", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagImages", new[] { "Image_Id" });
            DropIndex("dbo.TagImages", new[] { "Tag_Id" });
            DropIndex("dbo.Images", new[] { "UserId" });
            DropTable("dbo.TagImages");
            DropTable("dbo.Users");
            DropTable("dbo.Tags");
            DropTable("dbo.Images");
        }
    }
}
