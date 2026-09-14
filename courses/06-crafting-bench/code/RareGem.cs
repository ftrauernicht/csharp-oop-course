public class RareGem : ICollectible, ISellable
{
    public string Name { get; }
    public int Price { get; }

    public RareGem(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public void Collect()
    {
        Console.WriteLine($"Picked up {Name}, sparkling in the light.");
    }

    public void Sell()
    {
        Console.WriteLine($"Sold {Name} for {Price} coins.");
    }
}
