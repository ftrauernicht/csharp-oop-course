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
