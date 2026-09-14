// Course 12 - Generic Card Deck: a Deck<T> you build yourself, plus
// generic methods with type constraints.

var deck = new Deck<Card>();
deck.Add(new Card("Ace", "Spades"));
deck.Add(new Card("King", "Hearts"));
deck.Add(new Card("Queen", "Diamonds"));
deck.Add(new Card("Jack", "Clubs"));

Console.WriteLine($"Deck count before shuffling: {deck.Count}");
deck.Shuffle();
Console.WriteLine($"Deck count after shuffling: {deck.Count}");

// Shuffle uses real randomness, so which card comes out first will differ
// between runs -- only the count is guaranteed.
var drawn = deck.Draw();
Console.WriteLine($"Drew: {drawn}");
Console.WriteLine($"Deck count after drawing: {deck.Count}");

// --- The exact same class, with a completely different T ---
var numberDeck = new Deck<int>();
numberDeck.Add(7);
numberDeck.Add(42);
Console.WriteLine($"numberDeck count: {numberDeck.Count}");

// --- Core exercise: a generic method with a type constraint ---
var numbers = new List<int> { 3, 7, 2, 9, 4 };
Console.WriteLine($"Highest number: {FindHighest(numbers)}");

var ranks = new List<Card>
{
    new Card("2", "Spades"),
};
// FindHighest(ranks) would not compile -- Card doesn't implement
// IComparable<Card>, so the constraint below rejects it at compile time.

T FindHighest<T>(List<T> items) where T : IComparable<T>
{
    T highest = items[0];
    foreach (var item in items)
    {
        if (item.CompareTo(highest) > 0)
        {
            highest = item;
        }
    }

    return highest;
}

// --- Challenge: the new() constraint ---
var freshList = CreateDefault<List<int>>();
Console.WriteLine($"Freshly created, empty: {freshList.Count == 0}");

T2 CreateDefault<T2>() where T2 : new()
{
    return new T2();
}
