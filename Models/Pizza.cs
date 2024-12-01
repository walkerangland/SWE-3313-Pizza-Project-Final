using SWEPP.Models;

public class Pizza : MenuItem
{
    private decimal? _customPrice; // Allows static menu pizzas to retain their predefined prices

    public string Size { get; set; } = "Medium";
    public string Crust { get; set; } = "Regular";
    public string SauceAmount { get; set; } = "Normal";
    public string CheeseAmount { get; set; } = "Normal";
    public List<string> Toppings { get; set; } = new List<string>();

    public Pizza()
    {
        _customPrice = null;
    }

    public decimal Price
    {
        get => _customPrice ?? CalculatePrice();
        set => _customPrice = value;
    }

    public void RecalculatePrice()
    {
        _customPrice = null;
        Price = CalculatePrice();
    }

    private decimal CalculatePrice()
    {
        decimal basePrice = Size switch
        {
            "Small" => 8.99M,
            "Medium" => 10.99M,
            "Large" => 12.99M,
            "Extra Large" => 14.99M,
            _ => 10.99M
        };

        decimal crustPrice = Crust switch
        {
            "Thin" => 0.00M,
            "Regular" => 1.00M,
            "Stuffed" => 2.00M,
            _ => 0.00M
        };

        decimal saucePrice = SauceAmount switch
        {
            "Light" => 0.00M,
            "Normal" => 0.50M,
            "Extra" => 1.00M,
            _ => 0.00M
        };

        decimal cheesePrice = CheeseAmount switch
        {
            "Light" => 0.00M,
            "Normal" => 0.50M,
            "Extra" => 1.00M,
            _ => 0.00M
        };

        decimal toppingPrice = Toppings.Count * 0.25M;

        return basePrice + crustPrice + saucePrice + cheesePrice + toppingPrice;
    }
}
