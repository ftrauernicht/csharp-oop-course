// Core exercise answer key: the same shape as Watcher, applied to a
// second machine type.
public class Thunderjaw : Machine
{
    public Thunderjaw() : base("Thunderjaw", 200)
    {
    }

    public override void Attack()
    {
        LogAttack("fires a devastating chest cannon", 40);
    }
}
