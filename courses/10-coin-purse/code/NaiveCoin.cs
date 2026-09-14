// The default behavior, before this chapter's overrides: == compares
// references, not values.
public class NaiveCoin
{
    public int Denomination { get; }

    public NaiveCoin(int denomination)
    {
        Denomination = denomination;
    }
}
