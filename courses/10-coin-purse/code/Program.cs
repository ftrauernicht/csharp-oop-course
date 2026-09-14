// Course 10 - Coin Purse: coins that can genuinely be compared, sorted,
// and deduplicated -- not just compared by which object they happen to be.

var naive1 = new NaiveCoin(5);
var naive2 = new NaiveCoin(5);
Console.WriteLine($"Two NaiveCoin(5) objects, == : {naive1 == naive2}");

var coin1 = new Coin(5);
var coin2 = new Coin(5);
Console.WriteLine($"Two Coin(5) objects, == : {coin1 == coin2}");
Console.WriteLine($"Two Coin(5) objects, Equals: {coin1.Equals(coin2)}");

// --- Deduplicating with a HashSet<Coin> ---
var uniqueCoins = new HashSet<Coin>
{
    new Coin(5),
    new Coin(10),
    new Coin(5), // a duplicate -- HashSet relies on Equals/GetHashCode
};
Console.WriteLine($"Unique coin count: {uniqueCoins.Count}");

// --- Sorting ---
var purse = new List<Coin>
{
    new Coin(25),
    new Coin(1),
    new Coin(10),
    new Coin(5),
};
purse.Sort();
Console.WriteLine("Sorted purse: " + string.Join(", ", purse));

// --- Challenge: < and > built on CompareTo ---
Console.WriteLine($"Coin(5) < Coin(10): {new Coin(5) < new Coin(10)}");
Console.WriteLine($"Coin(25) > Coin(10): {new Coin(25) > new Coin(10)}");
