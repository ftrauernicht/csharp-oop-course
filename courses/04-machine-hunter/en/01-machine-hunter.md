🇬🇧 English | 🇩🇪 [Deutsch](../de/01-maschinenjaeger.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1](../../01-basics/en/01-csharp-basics.md) and [Course 2](../../02-cat-roster/en/01-cat-roster.md)

# Course 4 – Machine Hunter

**Goal:** several different machine types that all share a name, health,
and the ability to take damage, but each attacks in its own way — without
copy-pasting `Name`, `Health`, and `TakeDamage` into every single one. By
the end, you'll know what a base class and a subclass actually are, and
what `virtual`, `override`, `base`, and `protected` each do.

As in Course 3, the core exercise below is described in steps for you to
write yourself. The finished version, including the optional and challenge
parts, lives in [`code/`](../code/).

## 🟢 Core — The problem with copy-pasted classes

Imagine writing `Watcher` and `Thunderjaw` as two totally separate classes,
Course-2-style:

```csharp
public class Watcher
{
    public string Name { get; }
    public int Health { get; private set; }
    // ... TakeDamage, IsDefeated, all copy-pasted ...

    public void Attack()
    {
        Console.WriteLine($"{Name} shrieks and lunges for 8 damage!");
    }
}

public class Thunderjaw
{
    public string Name { get; }
    public int Health { get; private set; }
    // ... the exact same TakeDamage, IsDefeated, copy-pasted again ...

    public void Attack()
    {
        Console.WriteLine($"{Name} fires a devastating chest cannon for 40 damage!");
    }
}
```

Everything except `Attack()` is identical, and it'll only get worse with a
third or fourth machine type. **Inheritance** lets you write the shared
part exactly once, in a **base class**, and have every specific machine
type — a **subclass** — build on top of it, adding or changing only what's
actually different about it.

## 🟢 Core — The base class

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

Two things here are new, and both matter a lot:

- The constructor is `protected`, not `public`. **`protected`** means "only
  this class and its subclasses can use this" — one notch more open than
  `private` (this class only), one notch more closed than `public`
  (anyone). A plain, type-less `Machine` shouldn't exist on its own; only a
  specific machine type should. Try `new Machine("Test", 10)` from
  `Program.cs` once you've built the rest of this chapter, and the compiler
  refuses: `error CS0122: 'Machine.Machine(string, int)' is inaccessible
  due to its protection level`. Course 5 gives this idea a proper name and
  a dedicated keyword.
- `LogAttack` is also `protected` — a shared helper subclasses can call
  from their own `Attack()`, but that outside code (like `Program.cs`)
  can't reach at all. `public` members are your class's interface to the
  world; `protected` members are shared tools for the *family* of classes
  built on top of it.

## 🟢 Core — A subclass: `virtual`, `override`, and `base`

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
```

`: Machine` after the class name means "`Watcher` *is a* `Machine`" — it
automatically gets `Name`, `Health`, `TakeDamage`, and everything else
`Machine` defines, for free. Three pieces make this work:

- `: base("Watcher", 30)` on the constructor calls `Machine`'s own
  (`protected`) constructor with `Watcher`'s specific name and health,
  *before* `Watcher`'s own constructor body runs. Every subclass's
  constructor has to reach its base class's constructor somehow — this is
  how.
- `public virtual void Attack()` on `Machine` marked that method as
  **allowed to be overridden** by a subclass. Without `virtual`, `override`
  below wouldn't compile.
- `public override void Attack()` on `Watcher` replaces `Machine`'s generic
  version with `Watcher`'s own, specifically for `Watcher` objects. More:
  [Microsoft Learn – Inheritance](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/inheritance).

Try it:

```csharp
var watcher = new Watcher();
watcher.Attack();          // Watcher shrieks and lunges for 8 damage!
watcher.TakeDamage(15);    // Watcher takes 15 damage. Health: 15/30
```

`TakeDamage` wasn't written anywhere on `Watcher` — it's inherited from
`Machine`, unchanged, and it just works.

## 🟢 Core exercise — Write Thunderjaw yourself

Using `Watcher` above as your template, write a `Thunderjaw` class with:

- Name `"Thunderjaw"`, max health `200`
- An `Attack()` that logs `"fires a devastating chest cannon"` for `40`
  damage

```csharp
public class Thunderjaw : Machine
{
    // your code here: constructor with : base(...), and an Attack() override
}
```

Test it:

```csharp
var thunderjaw = new Thunderjaw();
thunderjaw.Attack();
thunderjaw.TakeDamage(250); // more than its max health -- should clamp at 0
Console.WriteLine($"{thunderjaw.Name} defeated: {thunderjaw.IsDefeated}");
```

If you get stuck, [`code/Thunderjaw.cs`](../code/Thunderjaw.cs) has a
working version.

## 🟡 Optional — Overriding is optional, not mandatory

`virtual` means a method *can* be overridden — nothing forces every
subclass to actually do it:

```csharp
public class Grazer : Machine
{
    public Grazer() : base("Grazer", 50)
    {
    }
}
```

`Grazer` has no `Attack()` override at all. Calling `grazer.Attack()` still
works — it runs `Machine`'s own generic version unchanged, printing
`"Grazer attacks!"`. An override that wants to *extend* the base behavior
instead of fully replacing it can also call it explicitly with
`base.Attack();` as its first line, then add more after — worth knowing,
even though none of this chapter's machines need it.

## 🔴 Optional, genuine challenge — A machine with something extra

A subclass isn't limited to overriding what it inherits — it can add
entirely new members that don't exist on the base class at all. Write a
`Strider` class (name `"Strider"`, max health `80`, `Attack()` logging
`"kicks"` for `12` damage) with one more method that only `Strider` has:

```csharp
public void Ride()
{
    Console.WriteLine($"You climb onto {Name} and ride across the frontier.");
}
```

`watcher.Ride()` shouldn't compile — only a `Strider` can be ridden.
Compare against [`code/Strider.cs`](../code/Strider.cs) once it works.

## What you learned

- Why copy-pasting shared members across similar classes is a sign you
  need a base class
- `: Machine` to inherit, `protected` for members only a class and its
  subclasses can use
- `virtual` (may be overridden) and `override` (this subclass's specific
  version)
- `base(...)` to reach a base class's constructor; `base.Method()` to call
  its version of a method explicitly
- A subclass can skip overriding a virtual method entirely, or add
  brand-new members the base class never had

## Next

Course 4 stands on its own, needing only Courses 1 and 2. For the rest of
this repository's roadmap — including Course 5, which puts every one of
these machines into a single list and fights them one by one — see
[PROJECT-IDEAS.md](../../../PROJECT-IDEAS.md).
