// `abstract` upgrades Course 4's pattern: Machine can never be
// instantiated (not just discouraged via `protected`), and `abstract void
// Attack()` has no body at all -- every concrete subclass MUST provide one,
// with no accidental fallback to a generic default.
public abstract class Machine
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

    public abstract void Attack();

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
