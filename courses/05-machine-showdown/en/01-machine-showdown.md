🇬🇧 English | 🇩🇪 [Deutsch](../de/01-maschinen-showdown.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1](../../01-basics/en/01-csharp-basics.md) and [Course 2](../../02-cat-roster/en/01-cat-roster.md) — sharing a domain with, but not requiring, [Course 4](../../04-machine-hunter/en/01-machine-hunter.md)

# Course 5 – Machine Showdown

**Goal:** put every different machine type into one single list, and run
the exact same loop over all of them — each one still attacks its own way,
even though the loop itself has no idea which specific machine it's looking
at each time. This is **polymorphism**: the payoff for everything Course 4
quietly set up.

*(If you've done [Course 4](../../04-machine-hunter/en/01-machine-hunter.md),
the class shapes below will look very familiar — skim the recap and jump to
"The payoff.")*

## 🟢 Core — Rebuilding the machine hierarchy

This course stands on its own, so here's a quick version of the same base
class and two machine types from Course 4:

```csharp
public class Machine
{
    public string Name { get; }
    public int MaxHealth { get; }
    public int Health { get; private set; }

    protected Machine(string name, int maxHealth)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }

    public virtual void Attack()
    {
        Console.WriteLine($"{Name} attacks!");
    }

    protected void LogAttack(string flavorText, int damage)
    {
        Console.WriteLine($"{Name} {flavorText} for {damage} damage!");
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0)
        {
            Health = 0;
        }
        Console.WriteLine($"{Name} takes {amount} damage. Health: {Health}/{MaxHealth}");
    }

    public bool IsDefeated
    {
        get { return Health <= 0; }
    }
}
```

```csharp
public class Watcher : Machine
{
    public Watcher() : base("Watcher", 30)
    {
    }

    public override void Attack()
    {
        LogAttack("shrieks and lunges", 8);
    }
}

public class Thunderjaw : Machine
{
    public Thunderjaw() : base("Thunderjaw", 200)
    {
    }

    public override void Attack()
    {
        LogAttack("fires a devastating chest cannon", 40);
    }
}
```

## 🟢 Core — abstract: making the contract airtight

Course 4 used a `protected` constructor to *discourage* creating a bare
`Machine` — but nothing stopped a subclass from simply not overriding
`Attack()` at all (that's exactly what Course 4's `Grazer` did, on
purpose, to show `virtual` is optional). For a base class that should
*never* exist on its own, and whose every subclass *must* define its own
behavior, C# has a stronger tool: **`abstract`**.

```csharp
public abstract class Machine
{
    // ... Name, MaxHealth, Health, the constructor, LogAttack, TakeDamage,
    // IsDefeated: all unchanged from above ...

    public abstract void Attack();
}
```

Two changes, both load-bearing:

- `abstract class Machine` means `new Machine(...)` is *never* legal,
  anywhere, for any reason — not just discouraged like `protected` was.
  Try it, and the compiler says
  `error CS0144: Cannot create an instance of the abstract type or
  interface 'Machine'`.
- `public abstract void Attack();` has **no body at all** — just a
  signature and a semicolon. Every non-abstract class that inherits from
  `Machine` is now *required* to provide an `Attack()` override, or it
  won't compile either. There's no generic fallback message anymore, and
  no way to accidentally skip it the way `Grazer` did.

(The constructor stays `protected`, even though `abstract` alone already
blocks direct instantiation — that's still the correct, idiomatic way to
write a constructor that only ever runs via a subclass's `base(...)`.)
More: [Microsoft Learn – Abstract classes](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance#abstract-base-classes).

## 🟢 Core — The payoff: one list, many behaviors

```csharp
var machines = new List<Machine>
{
    new Watcher(),
    new Thunderjaw(),
};

foreach (var machine in machines)
{
    machine.Attack();
}
```

```
Watcher shrieks and lunges for 8 damage!
Thunderjaw fires a devastating chest cannon for 40 damage!
```

Look closely at that `foreach`: it says `machine.Attack()` exactly once,
with no `if` checking what kind of machine it's looking at — and yet
`Watcher` and `Thunderjaw` each print something completely different. The
list is typed `List<Machine>`, so at compile time all the loop "knows" is
that every item is *some* `Machine`. Which `Attack()` actually runs is
decided at *runtime*, based on the object's real type. That's
**polymorphism** ("many forms"): one call, `machine.Attack()`, taking on a
different shape depending on what it's actually called on. More:
[Microsoft Learn – Polymorphism](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism).

You've technically been relying on pieces of this since Course 4 —
`watcher.Attack()` always ran `Watcher`'s version, never `Machine`'s
generic one. What's new here is the practical superpower: the loop, and
any code like it, never needs to change when a new machine type shows up.

## 🟢 Core exercise — Add a third machine, change nothing else

Write a `Strider` class (same stats as Course 4: name `"Strider"`, max
health `80`, `Attack()` logging `"kicks"` for `12` damage):

```csharp
public class Strider : Machine
{
    // your code here
}
```

Add `new Strider()` to the `machines` list. Run it again — the `foreach`
loop above needs **zero changes** to handle it correctly. That's the
actual point of this exercise: verify it for yourself, rather than take it
on faith. If you get stuck, [`code/Strider.cs`](../code/Strider.cs) has a
working version.

## 🟡 Optional — A short battle

Reuse the same list for something more game-like:

```csharp
foreach (var machine in machines)
{
    machine.TakeDamage(50);
}

foreach (var machine in machines)
{
    Console.WriteLine($"{machine.Name} defeated: {machine.IsDefeated}");
}
```

`Watcher` (30 max health) won't survive 50 damage; `Thunderjaw` and
`Strider` will. Same polymorphic loop pattern, now doing something with
actual stakes.

## 🔴 Optional, genuine challenge — When you still need to know the type

Polymorphism handles "every machine attacks" elegantly, but sometimes you
genuinely need to single out one specific type for something that isn't
part of the shared contract at all — say, a warning specifically for the
biggest machine. The [`is`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/type-testing-and-cast#the-is-operator)
operator checks an object's actual runtime type:

```csharp
foreach (var machine in machines)
{
    if (machine is Thunderjaw)
    {
        Console.WriteLine("Massive machine incoming!");
    }

    machine.Attack();
}
```

Add this to your loop and confirm the warning only ever prints for the
`Thunderjaw`. Used sparingly, `is` is a reasonable escape hatch; used for
*everything* (`if (machine is Watcher) ... else if (machine is
Thunderjaw) ... else if ...`), it defeats the entire point of polymorphism
— that's the line worth noticing.

## What you learned

- `abstract class` and `abstract` methods: a base class that can never be
  instantiated, with members that have no default implementation at all,
  forcing every concrete subclass to provide its own
- Polymorphism: calling the same method through a shared base type and
  having the *actual* runtime type decide what really runs
- Why a `List<Machine>` (or any collection of a shared base/abstract type)
  scales to new subclasses without the code that uses the list ever
  changing
- The `is` operator, for the rare case where you genuinely need to know an
  object's specific type — and why leaning on it everywhere defeats the
  purpose of polymorphism

## Next

Course 5 stands on its own, needing only Courses 1 and 2. For the rest of
this repository's roadmap, see
[PROJECT-IDEAS.md](../../../PROJECT-IDEAS.md).
