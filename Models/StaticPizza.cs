using SWEPP.Models;

public class StaticPizza : MenuItem
{
    public string Size { get; set; } = "Medium";
    public string Crust { get; set; } = "Regular";
    public List<string> Toppings { get; set; } = new List<string>();

    public StaticPizza(string name, string size, string crust, decimal price, List<string> toppings, string imagePath)
    {
        Name = name;
        Size = size;
        Crust = crust;
        Price = price;
        Toppings = toppings;
        ImagePath = imagePath;
    }
}