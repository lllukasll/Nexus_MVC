using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Nexus_MVC.Models
{
    [XmlRoot("cennik"), XmlType("cennik")]
    public class Price
    {
        [XmlElement("produkt")]
        public List<Produkt>? Products { get; set; }
    }

    [XmlType("produkt")]
    public class Produkt
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("cena_netto")]
        public decimal NettoPrice { get; set; }
        [XmlAttribute("vat")]
        public int Vat { get; set; }
    }
}
