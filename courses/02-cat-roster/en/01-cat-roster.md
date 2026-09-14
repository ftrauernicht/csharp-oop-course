🇬🇧 English | 🇩🇪 [Deutsch](../de/01-katzenkartei.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1 – Basics](../../01-basics/en/01-csharp-basics.md)

# Course 2 – Cat Roster

**Goal:** your first real step into object-oriented programming — grouping
related data and the behavior that belongs to it into one thing, instead of
juggling a pile of separate variables. By the end, you'll have a handful of
independent `Cat` objects that can introduce themselves, and you'll know
exactly what a class, an object, a constructor, and a method actually are.

A finished version of everything in this chapter lives in
[`code/Cat.cs`](../code/Cat.cs) and [`code/Program.cs`](../code/Program.cs),
including the challenge at the end.

## 🟢 Core — The problem loose variables run into

Say you wanted to track three cats with everything Course 1 gave you —
variables and `Console.WriteLine`:

```csharp
string cat1Name = "Whiskers";
int cat1Age = 3;
string cat1FavoriteToy = "crinkly ball";

string cat2Name = "Mochi";
int cat2Age = 1;
string cat2FavoriteToy = "feather wand";

Console.WriteLine($"Hi, I'm {cat1Name}! Age: {cat1Age}. Favorite toy: {cat1FavoriteToy}.");
Console.WriteLine($"Hi, I'm {cat2Name}! Age: {cat2Age}. Favorite toy: {cat2FavoriteToy}.");
```

This works, but it scales badly, and nothing stops you from typing
`cat2Name` where you meant `cat1Name` — the connection between "these three
variables belong to the same cat" exists only in the names you happened to
pick, not in the code itself. A **class** fixes exactly this: it lets you
define "a cat has a name, an age, and a favorite toy" *once*, as a
blueprint, and then create as many actual cats from it as you like.

## 🟢 Core — Defining a class

```csharp
public class Cat
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string FavoriteToy { get; set; }
}
```

`Name`, `Age`, and `FavoriteToy` are **properties** — this is the normal,
idiomatic way to expose a piece of data on a C# class. The `{ get; set; }`
part looks like it's doing nothing beyond what a plain variable would, and
right now, it is: it's a shorthand that quietly creates a hidden field
behind the scenes and default "read it" / "write it" logic for you. Course 3
opens that shorthand up and puts real logic inside it. For now, treat a
property exactly like a labeled slot of data belonging to the class. More:
[Microsoft Learn – Properties](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties).

This `Cat` class by itself doesn't represent any particular cat — it's a
blueprint, the same way a cookie cutter isn't a cookie. Nothing has actually
been baked yet.

## 🟢 Core — Creating objects with `new`

```csharp
var whiskers = new Cat();
whiskers.Name = "Whiskers";
whiskers.Age = 3;
whiskers.FavoriteToy = "crinkly ball";
```

`new Cat()` uses the blueprint to build one actual **object** (also called
an **instance**) in memory, and `whiskers` is a variable holding onto it.
You can make as many as you like from the same class:

```csharp
var mochi = new Cat();
mochi.Name = "Mochi";
mochi.Age = 1;
mochi.FavoriteToy = "feather wand";
```

`whiskers` and `mochi` are both `Cat`s, built from the exact same blueprint,
holding completely independent data — changing `mochi.Age` never touches
`whiskers.Age`. This line-by-line setup works, but it's clunky and, worse,
nothing forces you to actually set every property before using the object.
The fix is a **constructor**.

## 🟢 Core — The constructor

A constructor is a special method that runs automatically the moment `new`
builds an object, so you can hand it everything the object needs right at
creation:

```csharp
public class Cat
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string FavoriteToy { get; set; }

    public Cat(string name, int age, string favoriteToy)
    {
        Name = name;
        Age = age;
        FavoriteToy = favoriteToy;
    }
}
```

A constructor is named exactly like its class, has no return type (not even
`void`), and its job is to leave the new object in a sensible starting
state. Now creating a fully set-up cat is one line:

```csharp
var whiskers = new Cat("Whiskers", 3, "crinkly ball");
var mochi = new Cat("Mochi", 1, "feather wand");
```

**A note on `this`:** you'll often see constructors written with
`this.Name = name;` instead of plain `Name = name;`. Here, both do exactly
the same thing, because the parameter (`name`, lowercase) and the property
(`Name`, capitalized) have different casing — this is precisely why real C#
code follows that casing convention: it avoids the collision in the first
place. `this` means "the object this code is currently running on", and it
becomes *necessary*, not just a style choice, the moment a parameter's name
really does match a property's exactly:

```csharp
public Cat(string Name) // parameter deliberately named like the property
{
    this.Name = Name; // this.Name is the property; plain Name is the parameter
    // without `this.`, `Name = Name;` would assign the parameter to itself
    // and the property would silently stay empty
}
```

More: [Microsoft Learn – Constructors](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors).

## 🟢 Core — Instance methods

A property holds data; a **method** on a class gives it behavior:

```csharp
public class Cat
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string FavoriteToy { get; set; }

    public Cat(string name, int age, string favoriteToy)
    {
        Name = name;
        Age = age;
        FavoriteToy = favoriteToy;
    }

    public void Introduce()
    {
        Console.WriteLine($"Hi, I'm {Name}! Age: {Age}. Favorite toy: {FavoriteToy}.");
    }
}
```

`Introduce` looks like the methods from Course 1, with one difference that
matters a lot: it doesn't take `Name`, `Age`, or `FavoriteToy` as
parameters — it just uses them directly, because when you call it on a
specific cat, it already knows which one it's running on:

```csharp
whiskers.Introduce(); // Hi, I'm Whiskers! Age: 3. Favorite toy: crinkly ball.
mochi.Introduce();    // Hi, I'm Mochi! Age: 1. Favorite toy: feather wand.
```

Same method, same code, two completely different outputs — because
`whiskers` and `mochi` are different objects, each with their own values for
those properties.

**Try it yourself:** add a fourth property, `FavoriteFood`, to `Cat`, pass
it into the constructor, and add it to `Introduce`'s output.

## 🟡 Optional — A method that changes state

Methods aren't limited to reading properties and printing them — they can
change an object's own state too:

```csharp
public void HaveBirthday()
{
    Age++;
    Console.WriteLine($"{Name} just turned {Age}!");
}
```

```csharp
mochi.HaveBirthday(); // Mochi just turned 2!
```

`mochi.Age` is now permanently `2` — the change sticks around exactly like
any other property update, because a method has full access to (and can
modify) the object it belongs to.

## 🔴 Optional, genuine challenge — Comparing two cats

Add a method to `Cat` that takes another `Cat` as a parameter and returns
whether this cat is older:

```csharp
public bool IsOlderThan(Cat other)
{
    // your code here
}
```

Try it yourself before peeking at [`code/Cat.cs`](../code/Cat.cs). Once it
works:

```csharp
if (tom.IsOlderThan(whiskers))
{
    Console.WriteLine($"{tom.Name} is older than {whiskers.Name}.");
}
```

A hint, if you want one: inside the method, `Age` refers to *this* cat's
age (the one the method is called on), and `other.Age` reaches into the
`Cat` that got passed in — the same dot syntax you've used everywhere else,
just on a parameter instead of on `whiskers` or `mochi` directly.

## What you learned

- Why grouping related data into a class beats a pile of loose variables
- Defining a class with properties (`public string Name { get; set; }`)
- Creating objects with `new`, and that each is an independent instance
- Constructors, and what `this` means (and when it's actually required)
- Instance methods that read (`Introduce`) or change (`HaveBirthday`) an
  object's own state
- A method that takes another instance of the same class as a parameter
  (`IsOlderThan`)

There's a name for what you just built: bundling an object's data together
with the behavior that operates on it, so that using a `Cat` only requires
knowing what it can do (`Introduce()`, `HaveBirthday()`) and not how its
insides work, is called **encapsulation**. More:
[Microsoft Learn – Object-oriented programming (C#)](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/).
You'll meet it again, on purpose, in Course 3 — this time with properties
that actually enforce rules about the data they hold.

## Next

This was the second, and last, of this repository's two required
foundation courses. Course 3 onward can each be done in any order, as long
as Courses 1 and 2 are done first. Course 3 isn't published yet; check the
[repository overview](../../../README.md) for what's currently available.
