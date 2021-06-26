namespace Grubbrr_Interview_Project.Migrations
{
    using Grubbrr_Interview_Project.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GRUBBRR.Context.InvoiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GRUBBRR.Context.InvoiceContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            var products = new List<Products>
            {
                new Products{ProductID=1,ProductName="Bosch - Starters"},
                new Products{ProductID=2,ProductName="Delphi - Housings"},
                new Products{ProductID=3,ProductName="TRW Automotive - Alternators"},
                new Products{ProductID=4,ProductName="Bosch-Transmission Parts-Gears,Tie-Rod Ends,Ball Joints,U-J Cross"},
                new Products{ProductID=5,ProductName="Brake Parts & Rubber Components,Brake Hoses,Fuel Lines,"},
                new Products{ProductID=6,ProductName="SUMIKASUPER™ E101 - Washing Machine"},
                new Products{ProductID=7,ProductName="Home Appliances & Electronics>TVs, PCs"},
                new Products{ProductID=8,ProductName="Daily Life>Miscellaneous goods"},
                new Products{ProductID=9,ProductName="IT-related Materials>Display materials"},
                new Products{ProductID=10,ProductName="Home Appliances & Electronics>Lighting"},
                new Products{ProductID=11,ProductName="Mobile Phone"},
                new Products{ProductID=12,ProductName="Laptop"},
                new Products{ProductID=13,ProductName="Head Phone"},
                new Products{ProductID=14,ProductName="Charger"},
                new Products{ProductID=15,ProductName="Books"},
            };

            products.ForEach(prod => context.Products.Add(prod));
            context.SaveChanges();

            var status = new List<Status>
            {
                new Status{StatusID=1,StatusName="Draft" },
                new Status{StatusID=2,StatusName="Sent" },
                new Status{StatusID=3,StatusName="Paid" }
            };
            status.ForEach(st => context.InvoiceStatuses.Add(st));
            context.SaveChanges();

            var customers = new List<Customers>
            {
                new Customers{CustomerID=1,CustomerName="Jahir Hussan" },
                new Customers{CustomerID=2,CustomerName="Bowzia Begam" },
                new Customers{CustomerID=3,CustomerName="Sree" },
                new Customers{CustomerID=4,CustomerName="Muthu" },
                new Customers{CustomerID=5,CustomerName="David" },
                new Customers{CustomerID=6,CustomerName="Peter" },
                new Customers{CustomerID=7,CustomerName="Patrick" },
                new Customers{CustomerID=8,CustomerName="Ljutica" },
                new Customers{CustomerID=9,CustomerName="Dennis" },
                new Customers{CustomerID=10,CustomerName="Ivan" },
            };
            customers.ForEach(cus => context.Customers.Add(cus));
            context.SaveChanges();


            var invoiceinfo = new List<InvoiceInfo>
             {
                    new InvoiceInfo
                    {
                        CustomerID = 1,
                        InvoiceNumber = "INV1",
                        InvoiceDate = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(10),
                        StatusID = 1
                    },
                    new InvoiceInfo
                    {
                        CustomerID = 2,
                        InvoiceNumber = "INV2",
                        InvoiceDate = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(10),
                        StatusID = 3
                    },
                    new InvoiceInfo
                    {
                        CustomerID = 5,
                        InvoiceNumber = "INV3",
                        InvoiceDate = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(10),
                        StatusID = 2
                    },
                    new InvoiceInfo
                    {
                        CustomerID = 4,
                        InvoiceNumber = "INV4",
                        InvoiceDate = DateTime.Now,
                        DueDate = DateTime.Now.AddDays(10),
                        StatusID = 1
                    }
             };

            invoiceinfo.ForEach(invinfo => context.InvoiceInfos.Add(invinfo));
            context.SaveChanges();

            var invoiceList = new List<InvoiceList>
            {
                new InvoiceList{ ProductID=1,InvoiceInfoID=1,Description="Applie iphone",Price=100,Quantity=5},
                new InvoiceList{ProductID=2,InvoiceInfoID=1,Description="Toys",Price=500,Quantity=10}
            };
            invoiceList.ForEach(invList => context.InvoiceList.Add(invList));
            context.SaveChanges();
        }
    }
}
