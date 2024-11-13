namespace SWEPP.Models
{
    public class Pizza : MenuItem
    {
        public string Size { get; set; }
        public List<string> Toppings { get; set; } = new List<string>();
        public string Crust { get; set; }
    }
}