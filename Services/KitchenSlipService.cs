using System.Collections.Generic;
using System.Linq;
using SWEPP.Models;

namespace SWEPP.Services
{
    public class KitchenSlipService
    {
        private readonly List<Order> ActiveOrders = new List<Order>();

        public void AddOrder(Order order)
        {
            ActiveOrders.Add(order);
        }

        public void MarkOrderAsComplete(Order order)
        {
            order.MarkAsComplete();
            ActiveOrders.Remove(order);
        }

        public List<string> GetActiveKitchenSlips()
        {
            return ActiveOrders.Select(order => order.GenerateKitchenSlip()).ToList();
        }

        public List<Order> GetActiveOrders()
        {
            return ActiveOrders.ToList();
        }
    }
}