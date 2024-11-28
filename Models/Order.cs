using System.Collections.Generic;
using System.Linq;

namespace SWEPP.Models
{
    public class Order
    {
        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
        public decimal TotalPrice => Pizzas.Sum(pizza => pizza.Price);
        public string PaymentStatus { get; private set; }
        public string Status { get; set; } = "Active";

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
            if (!Pizzas.Any())
            {
                return "No items in this order.";
            }

            var slip = "Kitchen Order Slip:\n\n";
            foreach (var pizza in Pizzas)
            {
                slip += $"- {pizza.Size} pizza with {string.Join(", ", pizza.Toppings)} on {pizza.Crust} crust\n";
            }
            return slip.TrimEnd();
        }
    }
}