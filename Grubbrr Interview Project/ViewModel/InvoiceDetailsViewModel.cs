using Grubbrr_Interview_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grubbrr_Interview_Project.ViewModel
{
    public class InvoiceDetailsViewModel
    {
        public InvoiceInfo InvoiceInfo { get; set; }
        public InvoiceList InvoiceList { get; set; }
        public Image Image { get; set; }
        public IEnumerable<InvoiceList> InvoiceLists { get; set; }
        public IEnumerable<Customers> Customers { get; set; }
        public IEnumerable<Products> Products { get; set; }
        public IEnumerable<Status> InvoiceStatuses { get; set; }
    }
}