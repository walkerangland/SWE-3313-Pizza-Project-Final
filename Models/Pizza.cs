using System.Collections.Generic;

namespace SWEPP.Models
{
    public class Pizza : MenuItem
    {
        public string Size { get; set; } = "Medium"; // Default size
        public string Crust { get; set; } = "Regular"; // Default crust
        public string SauceAmount { get; set; } = "Normal"; // Default sauce amount
        public string CheeseAmount { get; set; } = "Normal"; // Default cheese amount
        public List<string> Toppings { get; set; } = new List<string>(); // Toppings list
    }
}