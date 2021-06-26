using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grubbrr_Interview_Project.Models
{
    public class InvoiceList
    {
        [Key]
        public int ID { get; set; }
        public int InvoiceInfoID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        public decimal Tax { get; set; }
        [MaxLength(1000)]
        public string InvoiceNote { get; set; }
        public string InvoiceDoucmentURL { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Products InvoiceStatus { get; set; }
        public virtual InvoiceInfo InvoiceInfo { get; set; }
    }
}