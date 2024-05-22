using Nexus_MVC.Helpers;
using Nexus_MVC.Models;
using Nexus_MVC.Services.Interfaces;
using System.Xml.Serialization;

namespace Nexus_MVC.Services
{
    public class InvoiceService : IInvoiceService
    {
        public  Result<Invoice> GenerateInvoice(string orderXml, string priceXml)
        {
            var orderResult = Deserialize<Order>(orderXml);
            if (!orderResult.IsSuccess)
            {
                return Result<Invoice>.Failure(orderResult.Error);
            }

            var pricesResult = Deserialize<Price>(priceXml);
            if (!pricesResult.IsSuccess)
            {
                return Result<Invoice>.Failure(pricesResult.Error);
            }

            var order = orderResult.Value;
            var prices = pricesResult.Value;

            var validationResult = ValidateOrderAndPrices(order, prices);
            if (!validationResult.IsSuccess)
            {
                return Result<Invoice>.Failure(validationResult.Error);
            }


            var invoice = new Invoice
            {
                CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Supplier = order.Supplier,
                Customer = order.Customer,
                Items = new List<InvoiceItem>(),
                Podsumowanie = 0
            };

            foreach (var item in order.Items)
            {
                var price = prices.Products.FirstOrDefault(p => p.Id == item.Id);
                if (price == null)
                {
                    return Result<Invoice>.Failure(Errors.MissingProduct(item.Id));
                }

                var invoiceItem = new InvoiceItem
                {
                    Lp = item.Lp,
                    Id = item.Id,
					Name = item.Name,
					NettoPrice = price.NettoPrice,
					Count = item.Count,
                    BruttoPrice = price.NettoPrice * (1 + price.Vat / 100m),
                    Total = price.NettoPrice * (1 + price.Vat / 100m) * item.Count
                };

                invoice.Items.Add(invoiceItem);
                invoice.Podsumowanie += invoiceItem.Total;
            }

            return Result<Invoice>.Success(invoice);
        }

		private Result<T> Deserialize<T>(string xml)
		{
			if (string.IsNullOrWhiteSpace(xml))
			{
				return Result<T>.Failure(Errors.XmlNullOrEmpty());
			}

			try
			{
				var serializer = new XmlSerializer(typeof(T));
				using (var reader = new StringReader(xml))
				{
					var result = (T)serializer.Deserialize(reader);
					return Result<T>.Success(result);
				}
			}
			catch (InvalidOperationException ex)
			{
				return Result<T>.Failure(Errors.DeserializeError(ex.Message));
			}
		}

		private Result ValidateOrderAndPrices(Order order, Price prices)
        {
            if (order == null)
            {
                return Result.Failure(Errors.XmlNullOrEmpty("Order"));
            }

            if (prices == null || !prices.Products.Any())
            {
                return Result.Failure(Errors.XmlNullOrEmpty("Pricing"));
            }

            var missingProducts = order.Items
                .Where(item => prices.Products.All(p => p.Id != item.Id))
                .Select(item => item.Id)
                .ToList();

            if (missingProducts.Any())
            {
                return Result.Failure(Errors.MissingProducts(missingProducts));
            }

            return Result.Success();
        }
    }
}
