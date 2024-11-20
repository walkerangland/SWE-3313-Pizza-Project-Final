using SWEPP.Models;
using System.Collections.Generic;
using System.Linq;

namespace SWEPP.Services
{
    public class OrderService
    {
        public List<MenuItem> CurrentOrder { get; private set; } = new List<MenuItem>();
        public Receipt? CurrentReceipt { get; private set; }
        public List<Receipt> DailyReceipts { get; private set; } = new List<Receipt>();
        private readonly CustomerService customerService;

        public OrderService(CustomerService customerService)
        {
            this.customerService = customerService;
        }

        public void AddToOrder(MenuItem item)
        {
            CurrentOrder.Add(item);
        }

        public void RemoveFromOrder(MenuItem item)
        {
            CurrentOrder.Remove(item);
        }

        public void ClearOrder()
        {
            CurrentOrder.Clear();
        }

        public Receipt GenerateReceipt(string customerName, string paymentMethod)
        {
            decimal totalPrice = CurrentOrder.Sum(item => item.Price);

            var receipt = new Receipt
            {
                CustomerName = customerName,
                Items = new List<MenuItem>(CurrentOrder),
                TotalPrice = totalPrice,
                PaymentMethod = paymentMethod
            };

            DailyReceipts.Add(receipt);

            if (customerService.LoggedInCustomer != null)
            {
                customerService.SaveOrderToHistory(receipt);
            }

            return receipt;
        }

        public void SetCurrentReceipt(Receipt receipt)
        {
            CurrentReceipt = receipt;
        }

        public string GenerateDailySalesSummary()
        {
            if (!DailyReceipts.Any())
            {
                return "No sales recorded for today.";
            }

            decimal totalSales = DailyReceipts.Sum(r => r.TotalPrice);
            int totalOrders = DailyReceipts.Count;
            var itemCounts = DailyReceipts
                .SelectMany(r => r.Items)
                .GroupBy(i => i.Name)
                .Select(g => new { ItemName = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count);

            var summary = $"Daily Sales Summary:\n\n";
            summary += $"Total Sales: {totalSales:C}\n";
            summary += $"Total Orders: {totalOrders}\n";
            summary += "\nMost Popular Items:\n";

            foreach (var item in itemCounts)
            {
                summary += $"- {item.ItemName}: {item.Count} sold\n";
            }

            return summary;
        }

        public string GenerateKitchenSlip()
        {
            if (!CurrentOrder.Any())
            {
                return "No items in the order.";
            }

            var slip = "Kitchen Order Slip:\n\n";
            foreach (var item in CurrentOrder)
            {
                slip += $"- {item.Name}\n";
            }

            slip += $"\nTotal Items: {CurrentOrder.Count}";
            return slip;
        }
    }
}
