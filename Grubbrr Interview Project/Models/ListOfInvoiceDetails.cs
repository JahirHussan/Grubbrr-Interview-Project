using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grubbrr_Interview_Project.Models
{
    public class ListOfInvoiceDetails
    {
        public int InvoiceInfoID { get; set; }
        public string InvoiceNumber { get; set; }
        public string BillTo { get; set; }
        public string InvoiceDate { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
    }
}