namespace Grubbrr_Interview_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.InvoiceInfo",
                c => new
                    {
                        InvoiceInfoID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        InvoiceNumber = c.String(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        StatusID = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false, defaultValue: false),
                    })
                .PrimaryKey(t => t.InvoiceInfoID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.StatusID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        StatusID = c.Int(nullable: false, identity: true),
                        StatusName = c.String(),
                    })
                .PrimaryKey(t => t.StatusID);
            
            CreateTable(
                "dbo.InvoiceList",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InvoiceInfoID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 500),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Tax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvoiceNote = c.String(maxLength: 1000),
                        InvoiceDoucmentURL = c.String(),
                        IsDeleted = c.Boolean(nullable: false,defaultValue:false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.InvoiceInfo", t => t.InvoiceInfoID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.InvoiceInfoID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);


        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceList", "ProductID", "dbo.Products");
            DropForeignKey("dbo.InvoiceList", "InvoiceInfoID", "dbo.InvoiceInfo");
            DropForeignKey("dbo.InvoiceInfo", "StatusID", "dbo.Status");
            DropForeignKey("dbo.InvoiceInfo", "CustomerID", "dbo.Customers");
            DropIndex("dbo.InvoiceList", new[] { "ProductID" });
            DropIndex("dbo.InvoiceList", new[] { "InvoiceInfoID" });
            DropIndex("dbo.InvoiceInfo", new[] { "StatusID" });
            DropIndex("dbo.InvoiceInfo", new[] { "CustomerID" });
            DropTable("dbo.Products");
            DropTable("dbo.InvoiceList");
            DropTable("dbo.Status");
            DropTable("dbo.InvoiceInfo");
            DropTable("dbo.Customers");
        }
    }
}
