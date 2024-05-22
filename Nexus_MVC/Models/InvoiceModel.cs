namespace Nexus_MVC.Models
{
    public class Invoice
    {
        public string CreateDate { get; set; }
        public Supplier Supplier { get; set; }
        public Customer Customer { get; set; }
        public List<InvoiceItem> Items { get; set; }
        public decimal Podsumowanie { get; set; }
    }

    public class InvoiceItem
    {
        public int Lp { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal NettoPrice { get; set; }
        public int Count { get; set; }
        public decimal BruttoPrice { get; set; }
        public decimal Total { get; set; }
    }
}
