// A capability, not a class hierarchy: anything that can be picked up.
// No implementation here at all -- only what a type must provide.
public interface ICollectible
{
    string Name { get; }
    void Collect();
}
