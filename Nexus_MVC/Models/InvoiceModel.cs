using System.Xml.Serialization;

namespace Nexus_MVC.Models
{
	[XmlRoot("faktura")]
	public class Invoice
	{
		[XmlAttribute("data_wystawienia")]
		public string CreateDate { get; set; }

		[XmlElement("wystawiajacy")]
		public Supplier Supplier { get; set; }

		[XmlElement("zamawiajacy")]
		public Customer Customer { get; set; }

		[XmlElement("pozycja")]
		public List<InvoiceItem> Items { get; set; }

		[XmlElement("podsumowanie")]
		public Summary Summary { get; set; }
	}

	public class InvoiceItem
	{
		[XmlAttribute("lp")]
		public int Lp { get; set; }

		[XmlAttribute("id")]
		public string Id { get; set; }

		[XmlAttribute("nazwa")]
		public string Name { get; set; }

		[XmlAttribute("cena_netto")]
		public decimal NettoPrice { get; set; }

		[XmlAttribute("sztuk")]
		public int Count { get; set; }

		[XmlAttribute("cena_brutto")]
		public decimal BruttoPrice { get; set; }

		[XmlAttribute("razem")]
		public decimal Total { get; set; }
	}

	public class Summary
	{
		[XmlAttribute("razem")]
		public decimal Total { get; set; }
	}
}
