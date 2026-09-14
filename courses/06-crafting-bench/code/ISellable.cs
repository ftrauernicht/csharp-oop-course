// A second, independent capability: anything that can be sold.
// Completely unrelated to ICollectible -- a type can implement either,
// both, or neither.
public interface ISellable
{
    int Price { get; }
    void Sell();
}
