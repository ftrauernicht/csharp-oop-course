// Core exercise answer key: a third machine type, added to prove the
// foreach loop in Program.cs needs zero changes to handle it.
public class Strider : Machine
{
    public Strider() : base("Strider", 80)
    {
    }

    public override void Attack()
    {
        LogAttack("kicks", 12);
    }
}
