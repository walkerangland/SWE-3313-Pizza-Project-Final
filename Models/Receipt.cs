namespace SWEPP.Models
{
    public class Receipt
    {
        public string CustomerName { get; set; }
        public List<MenuItem> Items { get; set; } = new List<MenuItem>();
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        public override string ToString()
        {
            string itemList = string.Join("\n", Items.Select(item => $"{item.Name} - {item.Price:C}"));
            return $"Customer: {CustomerName}\n" +
                   $"Date: {Date}\n" +
                   $"Items:\n{itemList}\n" +
                   $"Total: {TotalPrice:C}\n" +
                   $"Payment Method: {PaymentMethod}\n";
        }
    }
}