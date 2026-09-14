public class Coin : IComparable<Coin>
{
    public int Denomination { get; }

    public Coin(int denomination)
    {
        Denomination = denomination;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Coin other)
        {
            return false;
        }

        return Denomination == other.Denomination;
    }

    public override int GetHashCode()
    {
        return Denomination.GetHashCode();
    }

    public static bool operator ==(Coin? left, Coin? right)
    {
        if (left is null)
        {
            return right is null;
        }

        return left.Equals(right);
    }

    public static bool operator !=(Coin? left, Coin? right)
    {
        return !(left == right);
    }

    public int CompareTo(Coin? other)
    {
        if (other is null)
        {
            return 1;
        }

        return Denomination.CompareTo(other.Denomination);
    }

    public static bool operator <(Coin left, Coin right)
    {
        return left.CompareTo(right) < 0;
    }

    public static bool operator >(Coin left, Coin right)
    {
        return left.CompareTo(right) > 0;
    }

    public override string ToString()
    {
        return $"{Denomination}-cent coin";
    }
}
