🇬🇧 English | 🇩🇪 [Deutsch](../de/01-werkbank.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1](../../01-basics/en/01-csharp-basics.md) and [Course 2](../../02-cat-roster/en/01-cat-roster.md)

# Course 6 – Crafting Bench

**Goal:** model two capabilities that some items have and others don't —
being collectible, being sellable — without forcing every item into one
rigid class hierarchy. By the end, you'll know what an interface is, why a
class can implement several of them at once (unlike inheriting from more
than one base class, which C# doesn't allow at all), and how that gives you
a second, more flexible way to reuse the polymorphism payoff from Course 5.

As in Courses 3-5, the core exercise below is described in steps for you to
write yourself. The finished version, including the optional and challenge
parts, lives in [`code/`](../code/).

## 🟢 Core — Two capabilities that don't line up with one hierarchy

A herb can be picked up, but not sold. A rare gem can be picked up *and*
sold. A treasure map (the challenge below) can be sold without ever being
"picked up" the way an item in a bag is. Modeling this with a base class —
say, a `CollectibleBase` with `Collect()`, and hoping sellable items also
inherit from it somehow — runs straight into a wall: **a C# class can only
inherit from one base class.** There's no clean way to be "a kind of
Collectible *and* a kind of Sellable" through inheritance alone.

## 🟢 Core — Interfaces: a contract, not a hierarchy

```csharp
public interface ICollectible
{
    string Name { get; }
    void Collect();
}
```

An [`interface`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface)
lists members a type must have — a property, a method, whatever's inside —
with **no implementation at all**, not even the generic fallback `virtual`
allowed in Course 4. It's purely a promise: "anything that implements
`ICollectible` definitely has a `Name` and a `Collect()`." The `I` prefix
(`ICollectible`, `ISellable`) is a naming convention you'll see throughout
real C# code, worth adopting from the start.

A class **implements** an interface the same way it inherits from a base
class:

```csharp
public class Herb : ICollectible
{
    public string Name { get; }

    public Herb(string name)
    {
        Name = name;
    }

    public void Collect()
    {
        Console.WriteLine($"Picked up {Name}.");
    }
}
```

## 🟢 Core — Implementing more than one interface

```csharp
public interface ISellable
{
    int Price { get; }
    void Sell();
}
```

```csharp
public class RareGem : ICollectible, ISellable
{
    public string Name { get; }
    public int Price { get; }

    public RareGem(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public void Collect()
    {
        Console.WriteLine($"Picked up {Name}, sparkling in the light.");
    }

    public void Sell()
    {
        Console.WriteLine($"Sold {Name} for {Price} coins.");
    }
}
```

`: ICollectible, ISellable` — a comma-separated list. This is exactly what
inheritance couldn't do: `RareGem` now has two, entirely independent
promises fulfilled at once, and you could add a third or fourth interface
the same way. This is **composition over inheritance** in its simplest
form: instead of forcing every type into one "is-a" tree, you assemble a
type out of however many small, focused capabilities it actually needs.
More: [Microsoft Learn – Interfaces](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces).

## 🟢 Core — Interfaces are polymorphic too

```csharp
var inventory = new List<ICollectible>
{
    new Herb("Wild Mint"),
    new RareGem("Sunstone", 50),
};

foreach (var item in inventory)
{
    item.Collect();
}
```

This should look familiar — it's the exact same payoff as Course 5's
`List<Machine>`, just through an interface instead of an abstract class.
`List<ICollectible>` can hold *any* type that implements `ICollectible`,
regardless of what else that type is or does, and `item.Collect()` still
dispatches to each one's own version. Abstract classes and interfaces are
two different tools for the same underlying idea: program against a shared
contract, let the runtime sort out the specifics.

## 🟢 Core exercise — Write Firewood yourself

Following `RareGem`'s shape, write a `Firewood` class implementing **both**
`ICollectible` and `ISellable`, with name `"Firewood"` and price `5`:

```csharp
public class Firewood : ICollectible, ISellable
{
    // your code here
}
```

Add `new Firewood("Firewood", 5)` to the `inventory` list above and confirm
it shows up correctly when you `Collect()` everything. If you get stuck,
[`code/Firewood.cs`](../code/Firewood.cs) has a working version.

## 🟡 Optional — Finding what's sellable, in a mixed list

`inventory` is a `List<ICollectible>` — as far as the list's type is
concerned, nothing in it is necessarily sellable. To find out, per item,
whether it *also* implements `ISellable`, use `is` together with a variable
name, not just a type:

```csharp
foreach (var item in inventory)
{
    if (item is ISellable sellable)
    {
        sellable.Sell();
    }
}
```

`item is ISellable sellable` checks the actual object's type *and*, if it
matches, hands you `sellable` — already typed as `ISellable`, with `.Price`
and `.Sell()` available — in one step. This runs `Sell()` only on the items
that actually support it (`RareGem` and, once you've added it, `Firewood`),
skipping `Herb` entirely, all from one list holding every kind of item at
once.

## 🔴 Optional, genuine challenge — A type that's only Sellable

Prove the two interfaces are genuinely independent, not secretly a
hierarchy: write a `TreasureMap` class that implements `ISellable` **only**
(a `Price`, a `Sell()`) — no `ICollectible`, no `Name`, no `Collect()`.

```csharp
public class TreasureMap : ISellable
{
    // your code here
}
```

Confirm two things: a `TreasureMap` works fine in a `List<ISellable>` of
its own, and `inventory.Add(new TreasureMap(30))` — trying to put it into
the `List<ICollectible>` from above — refuses to compile:
`error CS1503: Argument 1: cannot convert from 'TreasureMap' to
'ICollectible'`, because a `TreasureMap` genuinely isn't one. Compare
against [`code/TreasureMap.cs`](../code/TreasureMap.cs).

## What you learned

- Why some combinations of behavior don't fit a single inheritance tree
- `interface`: a contract with no implementation at all, `I`-prefixed by
  convention
- A class can implement any number of interfaces (`: ICollectible,
  ISellable`), unlike inheriting from more than one base class
- Composition over inheritance: assembling a type from independent
  capabilities instead of forcing it into one hierarchy
- Interfaces give you the same polymorphic `List<T>` + `foreach` payoff as
  an abstract base class
- `is Type variableName` pattern matching, to check for and use a more
  specific capability inside a loop over a more general type

## Next

Course 6 stands on its own, needing only Courses 1 and 2. For the rest of
this repository's roadmap, see
[PROJECT-IDEAS.md](../../../PROJECT-IDEAS.md).
