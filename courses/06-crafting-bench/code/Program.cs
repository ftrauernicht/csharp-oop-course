// Course 6 - Crafting Bench: items that can be collected, some of which
// can also be sold -- two independent capabilities via interfaces.

var inventory = new List<ICollectible>
{
    new Herb("Wild Mint"),
    new RareGem("Sunstone", 50),
    new Firewood("Firewood", 5),
};

foreach (var item in inventory)
{
    item.Collect();
}

// --- Find and sell everything that supports it ---
foreach (var item in inventory)
{
    if (item is ISellable sellable)
    {
        sellable.Sell();
    }
}

// --- Total up the sellable value ---
int total = 0;
foreach (var item in inventory)
{
    if (item is ISellable sellable)
    {
        total += sellable.Price;
    }
}
Console.WriteLine($"Total sellable value: {total}");

// --- Challenge: a type that's ISellable but not ICollectible at all ---
var contracts = new List<ISellable>
{
    new TreasureMap(30),
};

foreach (var contract in contracts)
{
    contract.Sell();
}
