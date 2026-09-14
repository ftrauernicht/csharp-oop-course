// Course 4 - Machine Hunter: a Machine base class, and a few concrete
// machine types that each attack differently.

var watcher = new Watcher();
watcher.Attack();
watcher.TakeDamage(15);

var thunderjaw = new Thunderjaw();
thunderjaw.Attack();
thunderjaw.TakeDamage(250); // more than its max health -- clamps at 0
Console.WriteLine($"{thunderjaw.Name} defeated: {thunderjaw.IsDefeated}");

// --- Optional: a machine that doesn't override Attack() at all ---
var grazer = new Grazer();
grazer.Attack(); // uses Machine's own generic Attack()

// --- Challenge: a machine with an extra method beyond Machine ---
var strider = new Strider();
strider.Attack();
strider.Ride();
