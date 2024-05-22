using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nexus_MVC.Helpers;
using Nexus_MVC.Helpers.XmlSchema;
using Nexus_MVC.Models;
using Nexus_MVC.Models.DTOs;
using Nexus_MVC.Services.Interfaces;
using System.Diagnostics;

namespace Nexus_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly XmlSchemaSettings _xmlSchemaSettings;
		private readonly ILogger<HomeController> _logger;

		public HomeController(IInvoiceService invoiceService, IOptions<XmlSchemaSettings> xmlSchemaSettings, ILogger<HomeController> logger)
        {
            _invoiceService = invoiceService;
            _xmlSchemaSettings = xmlSchemaSettings.Value;
			_logger = logger;
		}

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateInvoice(GenerateInvoiceDTO generateInvoiceDTO)
        {
            if (!ModelState.IsValid)  return View("Index");

            try
            {
                var orderXml = await FileHelper.ReadFileContentAsync(generateInvoiceDTO.OrderFile);
                XmlValidator.ValidateXml(orderXml, Path.Combine(Directory.GetCurrentDirectory(), _xmlSchemaSettings.OrderSchemaPath));

                var priceXml = await FileHelper.ReadFileContentAsync(generateInvoiceDTO.PriceFile);
                XmlValidator.ValidateXml(priceXml, Path.Combine(Directory.GetCurrentDirectory(), _xmlSchemaSettings.PricingSchemaPath));

                var invoiceResult = _invoiceService.GenerateInvoice(orderXml, priceXml);
                if (!invoiceResult.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, invoiceResult.Error);
                    return View("Index");
                }

                return View("Invoice", invoiceResult.Value);
            }
            catch (Exception ex)
            {
				_logger.LogError(ex, Errors.ProcessingError(ex.Message));
				ModelState.AddModelError(string.Empty, Errors.ProcessingError(ex.Message));
                return View("Index");
            }
        }
    }
}
