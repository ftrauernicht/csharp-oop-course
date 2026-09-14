// Course 5 - Machine Showdown: the payoff for Course 4's inheritance --
// one loop, over a list of a shared abstract type, dispatches to each
// machine's own Attack() automatically.

var machines = new List<Machine>
{
    new Watcher(),
    new Thunderjaw(),
    new Strider(),
};

foreach (var machine in machines)
{
    if (machine is Thunderjaw)
    {
        Console.WriteLine("Massive machine incoming!");
    }
    machine.Attack();
}

// --- A short battle: every machine takes the same amount of damage ---
foreach (var machine in machines)
{
    machine.TakeDamage(50);
}

foreach (var machine in machines)
{
    Console.WriteLine($"{machine.Name} defeated: {machine.IsDefeated}");
}
