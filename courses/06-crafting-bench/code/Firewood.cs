// Core exercise answer key: the same two-interface shape as RareGem,
// applied to a second item.
public class Firewood : ICollectible, ISellable
{
    public string Name { get; }
    public int Price { get; }

    public Firewood(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public void Collect()
    {
        Console.WriteLine($"Picked up {Name}.");
    }

    public void Sell()
    {
        Console.WriteLine($"Sold {Name} for {Price} coins.");
    }
}
