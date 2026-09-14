public class Watcher : Machine
{
    public Watcher() : base("Watcher", 30)
    {
    }

    public override void Attack()
    {
        LogAttack("shrieks and lunges", 8);
    }
}
