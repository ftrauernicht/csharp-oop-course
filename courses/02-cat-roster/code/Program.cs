// Course 2 - Your first class: group a cat's data and behavior together,
// then create a few independent Cat objects from that one blueprint.

var whiskers = new Cat("Whiskers", 3, "crinkly ball");
var mochi = new Cat("Mochi", 1, "feather wand");
var tom = new Cat("Tom", 7, "cardboard box");

whiskers.Introduce();
mochi.Introduce();
tom.Introduce();

// --- A method that changes state, not just reads it ---
mochi.HaveBirthday();

// --- Challenge: compare two cats ---
if (tom.IsOlderThan(whiskers))
{
    Console.WriteLine($"{tom.Name} is older than {whiskers.Name}.");
}
