using SWEPP.Models;
using System.Collections.Generic;
using System.Linq;

namespace SWEPP.Services
{
    public class OrderService
    {
        private readonly CustomerService customerService;

        public OrderService(CustomerService customerService)
        {
            this.customerService = customerService;
        }

        public List<MenuItem> CurrentOrder { get; private set; } = new List<MenuItem>();
        public Receipt? CurrentReceipt { get; private set; }
        public List<Receipt> DailyReceipts { get; private set; } = new List<Receipt>();

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
            CurrentReceipt = receipt;

            if (customerService.LoggedInCustomer != null)
            {
                customerService.SaveOrderToHistory(receipt);
            }

            return receipt;
        }

        public Receipt? GetCurrentReceipt()
        {
            return CurrentReceipt;
        }

        public void SetCurrentReceipt(Receipt receipt)
        {
            CurrentReceipt = receipt;
        }

        public Order CreateOrder(Receipt receipt)
        {
            var order = new Order
            {
                Receipt = receipt
            };

            foreach (var item in CurrentOrder)
            {
                if (item is Pizza pizza)
                {
                    order.Pizzas.Add(pizza);
                }
                else
                {
                    order.MenuItems.Add(item);
                }
            }

            return order;
        }
    }
}
