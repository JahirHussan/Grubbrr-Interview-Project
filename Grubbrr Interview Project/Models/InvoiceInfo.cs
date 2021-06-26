using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grubbrr_Interview_Project.Models
{
    public class InvoiceInfo
    {
        [Key]
        public int InvoiceInfoID { get; set; }
        public int CustomerID { get; set; }
        [Required]
        public string InvoiceNumber { get; set; }
        [Required]
        public DateTime InvoiceDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public int StatusID { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Customers Customers { get; set; }
        public virtual Status InvoiceStatus { get; set; }
    }
}