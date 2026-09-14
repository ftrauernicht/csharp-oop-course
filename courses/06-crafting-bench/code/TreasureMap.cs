// Challenge solution: ISellable without ICollectible at all -- proof the
// two interfaces are genuinely independent, not a hierarchy.
public class TreasureMap : ISellable
{
    public int Price { get; }

    public TreasureMap(int price)
    {
        Price = price;
    }

    public void Sell()
    {
        Console.WriteLine($"Sold a treasure map for {Price} coins.");
    }
}
