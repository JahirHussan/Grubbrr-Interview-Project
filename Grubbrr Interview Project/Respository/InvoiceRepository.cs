using GRUBBRR.Context;
using Grubbrr_Interview_Project.Interfaces;
using Grubbrr_Interview_Project.Models;
using Grubbrr_Interview_Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Grubbrr_Interview_Project.Respository
{
    public class InvoiceRepository : IInvoice, IDisposable
    {

        private InvoiceContext context;

        public InvoiceRepository(InvoiceContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<InvoiceListViewModel>> invoiceLists()
        {
            var customerList = await context.Customers.ToListAsync();
            var invoicestatusesList = await context.InvoiceStatuses.ToListAsync();
            var invoiceInfo = await context.InvoiceInfos.Where(a => a.IsDeleted == false).ToListAsync();
            var invoiceList = await context.InvoiceList.Where(a => a.IsDeleted == false).ToListAsync();

            var Invoice = (from invoice in invoiceInfo
                           join customer in customerList on invoice.CustomerID equals customer.CustomerID into Table1
                           from customer in Table1.ToList()
                           join status in invoicestatusesList on invoice.StatusID equals status.StatusID into Table2
                           from status in Table2.ToList()
                           join price in invoiceList on invoice.InvoiceInfoID equals price.InvoiceInfoID
                           group new { invoice, price } by invoice.InvoiceInfoID
                                          into list
                           select new ListOfInvoiceDetails
                           {
                               InvoiceInfoID = list.Select(c => c.invoice.InvoiceInfoID).FirstOrDefault(),
                               InvoiceNumber = list.Select(c => c.invoice.InvoiceNumber).FirstOrDefault(),
                               BillTo = list.Select(c => c.invoice.Customers.CustomerName).FirstOrDefault(),
                               InvoiceDate = list.Select(c => c.invoice.InvoiceDate.ToString("dd/MM/yyyy")).FirstOrDefault(),
                               DueDate = list.Select(c => c.invoice.DueDate.ToString("dd/MM/yyyy")).FirstOrDefault(),
                               Status = list.Select(c => c.invoice.InvoiceStatus.StatusName).FirstOrDefault(),
                               Amount = list.Sum(c => c.price.Price)
                           }).ToList();
            var InvoiceList = from list in Invoice
                              select new InvoiceListViewModel
                              {
                                  listOfInvoice = list
                              };
            return InvoiceList;
        }

        public async Task<InvoiceInfo> GetInfoAsync(int invoiceInfoID, bool Isdeleted)
        {
            var invoiceInfo = await context.InvoiceInfos.Where(inv => inv.InvoiceInfoID == invoiceInfoID && inv.IsDeleted == Isdeleted).SingleOrDefaultAsync();
            return invoiceInfo;
        }

        public async Task<int> DisableinvoiceInfoAsync(int invoiceInfoID)
        {
            var InvoiceToDisable = context.InvoiceInfos
                                       .Where(c => c.InvoiceInfoID == invoiceInfoID).FirstOrDefault();
            var InvoiceListToDisable = context.InvoiceList
                           .Where(c => c.InvoiceInfoID == invoiceInfoID).ForEachAsync(i => i.IsDeleted = true);

            if (InvoiceToDisable == null && InvoiceListToDisable == null)
            {
                throw new Exception("Invalid Invoice Details");
            }
            InvoiceToDisable.IsDeleted = !InvoiceToDisable.IsDeleted;
            context.Entry(InvoiceToDisable).State = EntityState.Modified;
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InvoiceList>> GetInvoiceByIDAsync(int invoiceInfoID)
        {

           var InvoiceListEntity = await context.InvoiceList
                                        .Where(info => info.InvoiceInfoID == invoiceInfoID)
                                        .Include(info => info.InvoiceStatus)
                                        .ToListAsync();

            return InvoiceListEntity;
        }

        public async Task<IEnumerable<Customers>> GetAllCustomerAsync()
        {
            var customerList = await context.Customers.ToListAsync();

            return customerList;
        }

        public async Task<IEnumerable<Products>> GetAllProductAsync()
        {
            var productList = await context.Products.ToListAsync();

            return productList;
        }

        public async Task<IEnumerable<Status>> GetAllInvoiceStatusAsync()
        {
            var invoicestatusesList = await context.InvoiceStatuses.ToListAsync();

            return invoicestatusesList;
        }

        public async Task<int> SaveInvoice(InvoiceDetailsViewModel invoiceDetailsViewModel)
        {
            int ID = 0;
            try
            {
                InvoiceInfo invoiceInfo = new InvoiceInfo();
                invoiceInfo.CustomerID = invoiceDetailsViewModel.InvoiceInfo.CustomerID;
                invoiceInfo.InvoiceNumber = invoiceDetailsViewModel.InvoiceInfo.InvoiceNumber;
                invoiceInfo.InvoiceDate = invoiceDetailsViewModel.InvoiceInfo.InvoiceDate;
                invoiceInfo.DueDate = invoiceDetailsViewModel.InvoiceInfo.DueDate;
                invoiceInfo.StatusID = invoiceDetailsViewModel.InvoiceInfo.StatusID;
                invoiceInfo.IsDeleted = false;
                context.InvoiceInfos.Add(invoiceInfo);
                context.SaveChanges();

                ID = invoiceInfo.InvoiceInfoID;
                InvoiceList invoiceList = new InvoiceList();
                invoiceList.InvoiceInfoID = ID;
                invoiceList.ProductID = invoiceDetailsViewModel.InvoiceList.ProductID;
                invoiceList.Description = invoiceDetailsViewModel.InvoiceList.Description;
                invoiceList.Price = invoiceDetailsViewModel.InvoiceList.Price;
                invoiceList.Quantity = invoiceDetailsViewModel.InvoiceList.Quantity;
                invoiceList.Tax = invoiceDetailsViewModel.InvoiceList.Tax;
                invoiceList.InvoiceNote = invoiceDetailsViewModel.InvoiceList.InvoiceNote;
                invoiceList.InvoiceDoucmentURL = invoiceDetailsViewModel.Image.InvoiceDoucmentURL;
                invoiceList.IsDeleted = false;
                context.InvoiceList.Add(invoiceList);
                context.SaveChanges();


            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return ID;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}