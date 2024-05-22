using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Nexus_MVC.Models
{
    [XmlRoot("zamowienie"), XmlType("zamowienie")]
    public class Order
    {
        [XmlElement("zamawiajacy")]
        public Customer? Customer { get; set; }
        [XmlElement("dostawca")]
        public Supplier? Supplier { get; set; }
        [XmlElement("pozycja")]
        public List<OrderItem>? Items { get; set; }
    }

    [XmlType("zamawiajacy")]
    public class Customer
    {
        [XmlElement("nazwa")]
        public string? Name { get; set; }
        [XmlElement("adres")]
        public string? Address { get; set; }
    }

    [XmlType("dostawca")]
    public class Supplier
    {
        [XmlElement("nazwa")]
        public string? Name { get; set; }
        [XmlElement("adres")]
        public string? Address { get; set; }
    }

    [XmlType("pozycja")]
    public class OrderItem
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("nazwa")]
        public string Name { get; set; }
        [XmlAttribute("lp")]
        public int Lp { get; set; }
        [XmlAttribute("sztuk")]
        public int Count { get; set; }
    }
}
