// Challenge solution: a subclass can add entirely new members, not just
// override inherited ones. Ride() exists only on Strider.
public class Strider : Machine
{
    public Strider() : base("Strider", 80)
    {
    }

    public override void Attack()
    {
        LogAttack("kicks", 12);
    }

    public void Ride()
    {
        Console.WriteLine($"You climb onto {Name} and ride across the frontier.");
    }
}
