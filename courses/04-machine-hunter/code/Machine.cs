// The shared base class for every machine type. Its constructor is
// `protected`, not `public` -- only a subclass can call it (via `base(...)`),
// so there's no such thing as a bare, type-less Machine.
public class Machine
{
    public string Name { get; }
    public int MaxHealth { get; }
    public int Health { get; private set; }

    protected Machine(string name, int maxHealth)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }

    public virtual void Attack()
    {
        Console.WriteLine($"{Name} attacks!");
    }

    // A helper only subclasses can call -- outside code has no business
    // formatting attack messages directly.
    protected void LogAttack(string flavorText, int damage)
    {
        Console.WriteLine($"{Name} {flavorText} for {damage} damage!");
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0)
        {
            Health = 0;
        }
        Console.WriteLine($"{Name} takes {amount} damage. Health: {Health}/{MaxHealth}");
    }

    public bool IsDefeated
    {
        get { return Health <= 0; }
    }
}
