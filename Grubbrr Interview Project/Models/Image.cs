using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grubbrr_Interview_Project.Models
{
    public class Image
    {
        public string InvoiceDoucmentURL { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}