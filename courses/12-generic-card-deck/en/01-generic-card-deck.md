🇬🇧 English | 🇩🇪 [Deutsch](../de/01-generisches-kartendeck.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1](../../01-basics/en/01-csharp-basics.md) and [Course 2](../../02-cat-roster/en/01-cat-roster.md)

# Course 12 – Generic Card Deck

**Goal:** a `Deck<T>` you build yourself, that works identically for a deck
of playing cards, a deck of numbers, or anything else — the same way
`List<T>` has worked for anything you've handed it since Course 3. By the
end, you'll know how to write a generic class and a generic method, and
what a type constraint (`where T : ...`) actually buys you.

As in Courses 3, 4, 6, 8, 9, 10, and 11, the core exercise below is
described in steps for you to write yourself. The finished version,
including the challenge, lives in [`code/`](../code/).

## 🟢 Core — Writing a generic class

```csharp
public class Deck<T>
{
    private readonly List<T> _cards = new List<T>();

    public int Count
    {
        get { return _cards.Count; }
    }

    public void Add(T card)
    {
        _cards.Add(card);
    }

    public T Draw()
    {
        if (_cards.Count == 0)
        {
            throw new InvalidOperationException("The deck is empty.");
        }

        T card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }

    public void Shuffle()
    {
        var random = new Random();
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            T temp = _cards[i];
            _cards[i] = _cards[j];
            _cards[j] = temp;
        }
    }
}
```

`<T>` right after the class name is a **type parameter** — a placeholder
for whatever type you use this class with, filled in when you actually
write `Deck<Card>` or `Deck<int>`. Every `T` in the class body then means
"whatever type this particular deck was created with." This is exactly the
same mechanism `List<T>` itself uses — you've been *using* a generic class
since Course 3; `Deck<T>` is you *writing* one. More:
[Microsoft Learn – Generic classes](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/generics).

```csharp
var deck = new Deck<Card>();
deck.Add(new Card("Ace", "Spades"));
deck.Add(new Card("King", "Hearts"));

var numberDeck = new Deck<int>();
numberDeck.Add(7);
numberDeck.Add(42);
```

Same class, two completely unrelated element types, zero changes to
`Deck<T>` itself needed for either one.

`Shuffle` uses real randomness (the Fisher-Yates shuffle, if you did the
sibling JavaScript course's memory game — the same algorithm), so which
card ends up first will genuinely differ every time you run it. Only
`Count` staying the same is guaranteed:

```csharp
Console.WriteLine(deck.Count); // 2, before shuffling
deck.Shuffle();
Console.WriteLine(deck.Count); // still 2, order changed, count didn't
```

## 🟢 Core exercise — A generic method with a constraint

```csharp
var numbers = new List<int> { 3, 7, 2, 9, 4 };
Console.WriteLine(FindHighest(numbers)); // 9
```

Write `FindHighest`, a generic method that finds the largest item in any
`List<T>` — as long as `T` actually supports comparison:

```csharp
T FindHighest<T>(List<T> items) where T : IComparable<T>
{
    // your code here: loop through items, keep track of the highest one
    // seen so far (using .CompareTo, from Course 10), and return it
}
```

`where T : IComparable<T>` is a **type constraint**: it restricts which
types are even allowed to be used as `T` here, to ones that implement
[`IComparable<T>`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/reference-types#the-icomparable-interfaces) —
exactly the interface Course 10 had `Coin` implement. Without the
constraint, `item.CompareTo(highest)` inside the method wouldn't compile
at all: plain, unconstrained `T` could be *anything*, and the compiler has
no way to know it has a `CompareTo` method unless you tell it to require
one. `int` already implements `IComparable<int>`, which is exactly why
`FindHighest(numbers)` above just works. If you get stuck,
[`code/Program.cs`](../code/Program.cs) has a working version.

Try calling `FindHighest` with a `List<Card>` instead — it refuses to
compile: `error CS0311: The type 'Card' cannot be used as type parameter
'T'... There is no implicit reference conversion from 'Card' to
'System.IComparable<Card>'`. `Card` (from this chapter) never implemented
that interface, so the constraint correctly rejects it, at compile time,
before the program ever runs.

## 🔴 Optional, genuine challenge — A different kind of constraint

```csharp
T2 CreateDefault<T2>() where T2 : new()
{
    return new T2();
}
```

`where T2 : new()` is a different flavor of constraint: it doesn't require
implementing a specific interface, just having a public **parameterless
constructor** — so that `new T2()` inside the method is guaranteed to be
legal, whatever `T2` turns out to be. Try `CreateDefault<List<int>>()` (a
fresh, empty list) — and then try `CreateDefault<Card>()`, which refuses
to compile, because `Card`'s only constructor requires a `rank` and a
`suit`: `error CS0310: 'Card' must be a non-abstract type with a public
parameterless constructor`. [`code/Program.cs`](../code/Program.cs) has a
working version of the whole chapter.

## What you learned

- Writing a generic class (`class Deck<T>`) and using it with completely
  different type arguments without changing the class itself
- Generic methods (`T FindHighest<T>(...)`), separate from generic classes
- Type constraints: `where T : IComparable<T>` (must implement an
  interface) and `where T2 : new()` (must have a parameterless
  constructor) — and that violating either is a compile-time error, not a
  runtime surprise

## Next

Course 12 stands on its own, needing only Courses 1 and 2. For the rest of
this repository's roadmap, see
[PROJECT-IDEAS.md](../../../PROJECT-IDEAS.md).
