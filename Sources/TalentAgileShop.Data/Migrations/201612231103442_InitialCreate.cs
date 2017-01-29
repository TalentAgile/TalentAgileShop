namespace TalentAgileShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Size = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        Category_Id = c.String(maxLength: 128),
                        Image_Id = c.Int(),
                        Origin_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.ProductImages", t => t.Image_Id)
                .ForeignKey("dbo.Countries", t => t.Origin_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.Image_Id)
                .Index(t => t.Origin_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Origin_Id", "dbo.Countries");
            DropForeignKey("dbo.Products", "Image_Id", "dbo.ProductImages");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "Origin_Id" });
            DropIndex("dbo.Products", new[] { "Image_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropTable("dbo.Products");
            DropTable("dbo.ProductImages");
            DropTable("dbo.Countries");
            DropTable("dbo.Categories");
        }
    }
}
