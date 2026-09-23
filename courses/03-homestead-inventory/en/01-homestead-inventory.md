🇬🇧 English | 🇩🇪 [Deutsch](../de/01-hof-inventar.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1](../../01-basics/en/01-csharp-basics.md) and [Course 2](../../02-cat-roster/en/01-cat-roster.md)

# Course 3 – Homestead Inventory

**Goal:** manage a whole collection of objects instead of a few
individually-named ones, and give a class real control over its own data
instead of leaving it wide open. By the end, you'll have a small farm
tracking any number of crops, each of which enforces its own rules about
how much it can be watered. No matter who's doing the watering.

This chapter hands you noticeably less finished code than Courses 1 and 2
did, on purpose: the core exercise below is the actual point of this
chapter, so it's described in steps for you to write yourself, not handed
over pre-written. The finished version, including the optional parts and
the challenge, lives in [`code/`](../code/)
(`Crop.cs`, `Animal.cs`, and `Program.cs`), in case you get stuck or want
to compare once you're done.

## 🟢 Core — One variable per object doesn't scale

Course 2 created cats one named variable at a time:
`var whiskers = new Cat(...)`, `var mochi = new Cat(...)`. That's fine for
two or three, but a homestead might have a dozen crops, and you don't know
the exact number up front. You need a way to hold *many* objects of the
same type, of any quantity, in one place.

## 🟢 Core — List\<T\>

```csharp
var crops = new List<Crop>();
crops.Add(new Crop("Carrot"));
crops.Add(new Crop("Potato"));

Console.WriteLine(crops.Count); // 2
```

[`List<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)
is a growable, ordered collection. The `<T>` is a placeholder for "the
type of thing this list holds" (here, `Crop`), so `List<Crop>` reads as
"a list of Crops." `.Add(...)` appends one item, and `.Count` tells you how
many are in it right now. You can also fill one right at creation, which is
usually cleaner than several separate `.Add(...)` calls:

```csharp
var crops = new List<Crop>
{
    new Crop("Carrot"),
    new Crop("Potato"),
    new Crop("Pumpkin"),
};
```

## 🟢 Core — foreach

```csharp
foreach (var crop in crops)
{
    Console.WriteLine(crop.Name);
}
```

[`foreach`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-foreach-statement)
runs its block once for every item in a collection, with `crop` bound to
the current one each time: no manual counting, no risk of an off-by-one
error the way a hand-written index-based loop can have. Read it as "for
each crop in crops, do this." You'll reach for `foreach` any time you want
to do something to *every* item in a list, which is most of the time.

## 🟢 Core — A first version of Crop

Start with the same shape Course 2 used for `Cat`:

```csharp
public class Crop
{
    public string Name { get; set; }
    public int WaterLevel { get; set; }

    public Crop(string name)
    {
        Name = name;
        WaterLevel = 0;
    }
}
```

Wire it up and try it:

```csharp
var crops = new List<Crop>
{
    new Crop("Carrot"),
    new Crop("Potato"),
    new Crop("Pumpkin"),
};

foreach (var crop in crops)
{
    crop.WaterLevel += 40;
}

foreach (var crop in crops)
{
    Console.WriteLine($"{crop.Name}: water level {crop.WaterLevel}");
}
```

This runs fine. But nothing stops a mistake like `crop.WaterLevel = -999;`
or `crop.WaterLevel = 250;` from happening somewhere else in a bigger
program, leaving a crop in a state that shouldn't be possible. A crop's
water level should always stay between 0 (bone dry) and 100 (fully
watered), full stop, and that rule shouldn't depend on every single piece
of code that touches `WaterLevel` remembering to check it.

## 🟢 Core — A property that enforces its own rule

This is exactly what a class's own property can guarantee, if you write it
with real logic instead of the `{ get; set; }` shorthand from Course 2:

```csharp
public class Crop
{
    private int _waterLevel;

    public string Name { get; }

    public int WaterLevel
    {
        get => _waterLevel;
        set
        {
            // your code here: clamp `value` into the 0-100 range before
            // storing it in _waterLevel
        }
    }

    public Crop(string name)
    {
        Name = name;
        WaterLevel = 0;
    }

    public void Water(int amount)
    {
        WaterLevel += amount;
    }
}
```

A few new pieces here:

- `private int _waterLevel;` is a **private field**: unlike the properties
  you've used so far, `private` means only code *inside* this class can
  touch it directly. The leading underscore is a common C# convention for
  a private backing field, so it's visually obvious at a glance which is
  the field and which is the property.
- `public string Name { get; }` has no `set` at all: a **get-only
  property**. It can only be assigned inside the constructor (where you see
  `Name = name;` below) and never again after that. Not every property
  needs to be changeable.
- `WaterLevel` is now a **full property**: `get => _waterLevel;` hands back
  whatever's currently stored, and the `set { ... }` block runs *every*
  time someone writes `crop.WaterLevel = ...`, including from inside
  `Water(...)`, which now goes through the exact same rule as everything
  else, instead of touching a field directly.

Fill in the `set` block yourself: if `value` (the incoming number) is below
0, store 0 instead; if it's above 100, store 100 instead; otherwise store
`value` as given. Test it by trying to overshoot both ends:
`crop.Water(1000)` should leave `WaterLevel` at exactly 100, and
`crop.WaterLevel = -20;` should leave it at exactly 0. If you get stuck,
[`code/Crop.cs`](../code/Crop.cs) has a working version.

There's a name for what you just built: a class that controls access to
its own data, instead of trusting whoever's using it to always do the right
thing, is **encapsulation**. It's the same idea Course 2 already
introduced, now with an actual rule behind it instead of just a bundle of
fields. More:
[Microsoft Learn – Properties](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties).

## 🟡 Optional — A property only the class itself can set

`IsHarvestable` should become `true` once a crop is fully watered, but it
shouldn't be something outside code can just set directly. Nobody should
be able to write `crop.IsHarvestable = true;` and skip the watering
entirely. A **private setter** does exactly that:

```csharp
public bool IsHarvestable { get; private set; }
```

Anyone can *read* `crop.IsHarvestable`, but only code inside `Crop` itself
can *write* it. Set it to `true` inside `WaterLevel`'s `set` block, once
`_waterLevel` reaches 100, and add a `Harvest()` method:

```csharp
public void Harvest()
{
    if (IsHarvestable)
    {
        Console.WriteLine($"Harvested {Name}!");
        WaterLevel = 0;
        IsHarvestable = false;
    }
    else
    {
        Console.WriteLine($"{Name} isn't ready to harvest yet.");
    }
}
```

## 🔴 Optional, genuine challenge — Animals

Apply the exact same pattern to a second, independent class. Write an
`Animal` class with:

- A get-only `Name` property, set in the constructor.
- A `Happiness` property (`int`), validated the same way `WaterLevel` is,
  clamped between 0 and 100.
- A `Pet()` method that increases `Happiness` by 20.

Then build a `List<Animal>`, `foreach` over it calling `Pet()` on each, and
confirm the clamp works the same way it did for crops:
`animal.Happiness = 1000;` should leave it at exactly 100. Compare against
[`code/Animal.cs`](../code/Animal.cs) once it works.

## What you learned

- `List<T>`: a growable collection of objects of one type, `.Add(...)`,
  `.Count`
- `foreach`, for running the same code on every item in a collection
- Private fields (`private int _waterLevel;`) versus public properties
- Get-only properties (`{ get; }`), settable only from the constructor
- A full property with real validation logic in its `set` block:
  encapsulation, put to actual use
- A private setter (`{ get; private set; }`) for a property only its own
  class should be able to change

## Next

Course 3 stands on its own, needing only Courses 1 and 2. For the rest of
this repository's roadmap, see
[PROJECT-IDEAS.md](../../../PROJECT-IDEAS.md).
