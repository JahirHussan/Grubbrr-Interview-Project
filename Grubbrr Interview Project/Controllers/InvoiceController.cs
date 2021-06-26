using GRUBBRR.Context;
using Grubbrr_Interview_Project.Interfaces;
using Grubbrr_Interview_Project.Models;
using Grubbrr_Interview_Project.Respository;
using Grubbrr_Interview_Project.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Grubbrr_Interview_Project.Controllers
{
    public class InvoiceController : Controller
    {
        public IInvoice invoice;


        public InvoiceController()
        {
            this.invoice = new InvoiceRepository(new InvoiceContext());
        }

        public InvoiceController(IInvoice invoice)
        {
            this.invoice = invoice
             ?? throw new ArgumentNullException(nameof(invoice));
        }



        // GET: Invoice
        public async Task<ActionResult> Index()
        {
            try
            {
                var InvoiceList = await invoice.invoiceLists();
                return View(InvoiceList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var InvoiceList = await invoice.GetInfoAsync(id, false);
                if (InvoiceList != null)
                {
                    await invoice.DisableinvoiceInfoAsync(id);
                }
                Message messsage = new Message()
                {
                    Description = InvoiceList.IsDeleted == true ? "Invoice Deatils Deted Successfully" : "",
                    MessageType = MessageType.Success
                };
                TempData["Message"] = messsage;
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Save(InvoiceDetailsViewModel invoiceDetailsViewModel)
        {
            try
            {
                if (invoiceDetailsViewModel.Image.ImageFile != null)
                {
                    string FileName = Path.GetFileNameWithoutExtension(invoiceDetailsViewModel.Image.ImageFile.FileName);
                    string FileExtension = Path.GetExtension(invoiceDetailsViewModel.Image.ImageFile.FileName);
                    FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + FileName.Trim() + FileExtension;
                    string UploadPath = Path.Combine(Server.MapPath("/UserImage"), FileName);
                    invoiceDetailsViewModel.Image.InvoiceDoucmentURL = UploadPath + FileName;
                    invoiceDetailsViewModel.Image.ImageFile.SaveAs(UploadPath);
                }
                await invoice.SaveInvoice(invoiceDetailsViewModel);           

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            InvoiceDetailsViewModel creatinvoiceDetailsViewModel = new InvoiceDetailsViewModel();
            try
            {
                creatinvoiceDetailsViewModel.Customers = await invoice.GetAllCustomerAsync();
                creatinvoiceDetailsViewModel.Products = await invoice.GetAllProductAsync();
                creatinvoiceDetailsViewModel.InvoiceStatuses = await invoice.GetAllInvoiceStatusAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(creatinvoiceDetailsViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            InvoiceDetailsViewModel invoiceDetailsViewModel = new InvoiceDetailsViewModel();
            try
            {
                var vaildInvoices = await invoice.GetInfoAsync(id, false);
                if (vaildInvoices != null)
                {
                    invoiceDetailsViewModel.Customers = await invoice.GetAllCustomerAsync();
                    invoiceDetailsViewModel.Products = await invoice.GetAllProductAsync();
                    invoiceDetailsViewModel.InvoiceStatuses = await invoice.GetAllInvoiceStatusAsync();
                    invoiceDetailsViewModel.InvoiceLists = await invoice.GetInvoiceByIDAsync(id);
                    invoiceDetailsViewModel.InvoiceInfo = await invoice.GetInfoAsync(id, false);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(invoiceDetailsViewModel);
        }

    }
}