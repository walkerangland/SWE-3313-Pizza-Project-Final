using SWEPP.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SWEPP.Services
{
    public class CustomerService
    {
        private readonly string filePath = "customers.json";
        private List<Customer> customers;
        public Customer LoggedInCustomer { get; private set; }

        public CustomerService()
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                customers = JsonSerializer.Deserialize<List<Customer>>(json);
            }
            else
            {
                customers = new List<Customer>();
            }
        }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
            SaveCustomersToFile();
        }

        public List<Customer> GetCustomers()
        {
            return customers;
        }

        public Customer ValidateCustomer(string phoneNumber, string password)
        {
            return customers.FirstOrDefault(c => c.PhoneNumber == phoneNumber && c.Password == password);
        }

        public void Login(Customer customer)
        {
            LoggedInCustomer = customer;
        }

        public void Logout()
        {
            LoggedInCustomer = null;
        }

        public void SaveOrderToHistory(Receipt receipt)
        {
            if (LoggedInCustomer != null)
            {
                LoggedInCustomer.OrderHistory.Add(receipt);
                Console.WriteLine($"Order saved for: {LoggedInCustomer.Name}"); // Debugging
                Console.WriteLine($"Order count: {LoggedInCustomer.OrderHistory.Count}"); // Debugging
                SaveCustomersToFile();
            }
        }

        private void SaveCustomersToFile()
        {
            var json = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
