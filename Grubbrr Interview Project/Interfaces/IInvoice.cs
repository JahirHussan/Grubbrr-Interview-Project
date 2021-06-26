using Grubbrr_Interview_Project.Models;
using Grubbrr_Interview_Project.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grubbrr_Interview_Project.Interfaces
{
    public interface IInvoice : IDisposable
    {
        Task<IEnumerable<InvoiceListViewModel>> invoiceLists();
        Task<InvoiceInfo> GetInfoAsync(int invoiceInfoID, bool Isdeleted);
        Task<int> DisableinvoiceInfoAsync(int invoiceInfoID);
        Task<IEnumerable<InvoiceList>> GetInvoiceByIDAsync(int invoiceInfoID);
        Task<IEnumerable<Customers>> GetAllCustomerAsync();
        Task<IEnumerable<Products>> GetAllProductAsync();
        Task<IEnumerable<Status>> GetAllInvoiceStatusAsync();
        Task<int> SaveInvoice(InvoiceDetailsViewModel invoiceDetailsViewModel);
    }
}
