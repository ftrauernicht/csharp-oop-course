public class Herb : ICollectible
{
    public string Name { get; }

    public Herb(string name)
    {
        Name = name;
    }

    public void Collect()
    {
        Console.WriteLine($"Picked up {Name}.");
    }
}
