using Nexus_MVC.Helpers;
using Nexus_MVC.Models;

namespace Nexus_MVC.Services.Interfaces
{
    public interface IInvoiceService
    {
        Result<Invoice> GenerateInvoice(string orderXml, string priceXml);
        Result<string> SerializeInvoiceToXml(Invoice invoice, string filePath);

	}
}
