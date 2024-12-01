using System.Collections.Generic;
using System.Linq;

namespace SWEPP.Models
{
    public class Order
    {
        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        public List<MenuItem> Items
        {
            get
            {
                var allItems = new List<MenuItem>();
                allItems.AddRange(Pizzas);
                allItems.AddRange(MenuItems);
                return allItems;
            }
        }

        public decimal TotalPrice => Items.Sum(item => item.Price);

        public string PaymentStatus { get; private set; }
        public string Status { get; set; } = "Active";

        public Receipt Receipt { get; set; }

        public void ProcessPayment(string paymentType, string paymentDetails = null)
        {
            switch (paymentType)
            {
                case "Visa":
                case "MasterCard":
                    PaymentStatus = string.IsNullOrWhiteSpace(paymentDetails)
                        ? "Failed"
                        : "Success";
                    break;

                case "Cash":
                    PaymentStatus = "Success";
                    break;

                case "Check":
                    PaymentStatus = "Pending";
                    break;

                default:
                    PaymentStatus = "Failed";
                    break;
            }
        }

        public void MarkAsComplete()
        {
            Status = "Completed";
        }

        public string GenerateKitchenSlip()
        {
            if (!Items.Any())
            {
                return "No items in this order.";
            }

            var slip = "Kitchen Order Slip:\n\n";

            if (Pizzas.Any())
            {
                slip += "Pizzas:\n";
                foreach (var pizza in Pizzas)
                {
                    slip += $"- {pizza.Size} pizza with {string.Join(", ", pizza.Toppings)} on {pizza.Crust} crust\n";
                }
            }

            if (MenuItems.Any())
            {
                slip += "\nMenu Items:\n";
                foreach (var item in MenuItems)
                {
                    slip += $"- {item.Name}\n";
                }
            }

            return slip.TrimEnd();
        }

        public void AddPizza(Pizza pizza)
        {
            Pizzas.Add(pizza);
        }

        public void AddMenuItem(MenuItem item)
        {
            MenuItems.Add(item);
        }

        public void ClearOrder()
        {
            Pizzas.Clear();
            MenuItems.Clear();
            Status = "Active";
            PaymentStatus = null;
        }
    }
}
