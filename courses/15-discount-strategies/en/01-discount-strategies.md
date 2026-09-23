🇬🇧 English | 🇩🇪 [Deutsch](../de/01-rabatt-strategien.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1](../../01-basics/en/01-csharp-basics.md) and [Course 2](../../02-cat-roster/en/01-cat-roster.md)

# Course 15 – Discount Strategies

**Goal:** turn a growing tangle of `if`/`else if` into small, swappable
objects. Along the way, put a name to habits this repository's courses
have already been building since Course 4, without ever calling them
"patterns." This is the last course in this repository's currently
planned roadmap, and it's deliberately less about new syntax than about
recognizing what you already know how to do.

As in Courses 3, 4, 6, 8, 9, 10, 11, 12, 13, and 14, the core exercise
below is described in steps for you to write yourself. The finished
version, including the challenge, lives in [`code/`](../code/).

## 🟢 Core — The mess

```csharp
public class MessyPriceCalculator
{
    public int CalculatePrice(int basePrice, string customerType)
    {
        if (customerType == "regular")
        {
            return basePrice;
        }
        else if (customerType == "member")
        {
            return basePrice - (basePrice * 10 / 100);
        }
        else if (customerType == "vip")
        {
            return basePrice - 50;
        }
        else
        {
            throw new ArgumentException($"Unknown customer type: {customerType}");
        }
    }
}
```

This works today, but every new customer type means opening this method
and adding another branch. And testing "does the member discount math
work" (Course 14 style) means going through a method that also happens to
contain VIP and regular pricing, whether the test cares about them or not.

## 🟢 Core — The Strategy pattern

```csharp
public interface IDiscountStrategy
{
    int Apply(int basePrice);
}

public class NoDiscount : IDiscountStrategy
{
    public int Apply(int basePrice)
    {
        return basePrice;
    }
}

public class PercentageDiscount : IDiscountStrategy
{
    private readonly int _percent;

    public PercentageDiscount(int percent)
    {
        _percent = percent;
    }

    public int Apply(int basePrice)
    {
        return basePrice - (basePrice * _percent / 100);
    }
}
```

```csharp
public class PriceCalculator
{
    public int CalculatePrice(int basePrice, IDiscountStrategy discount)
    {
        return discount.Apply(basePrice);
    }
}
```

`PriceCalculator` no longer has a single `if` in it. Every discount rule
is its own small class, testable completely on its own: exactly Course
14's `Assert.Equal`, aimed at `new PercentageDiscount(10).Apply(100)`
directly, no `PriceCalculator` involved at all. This is the **Strategy
pattern**: an interface for "a way of doing this one thing," and separate
classes for each actual way of doing it, chosen by whoever's using it
instead of being baked into one big method. You've been doing this since
Course 4: `Watcher` and `Thunderjaw` are strategies for "how a machine
attacks," you just didn't have this name for it yet. More:
[Microsoft Learn – Interfaces](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/types/interfaces)
(the same feature, Course 6's chapter goes deeper on the mechanics).

## 🟢 Core exercise — Write FixedAmountDiscount yourself

Following `PercentageDiscount`'s shape, write a strategy that always
subtracts a fixed amount:

```csharp
public class FixedAmountDiscount : IDiscountStrategy
{
    // your code here: a constructor taking the amount to subtract, and
    // an Apply(basePrice) that subtracts it
}
```

Test it: `new PriceCalculator().CalculatePrice(100, new FixedAmountDiscount(50))`
should give `50`. If you get stuck,
[`code/FixedAmountDiscount.cs`](../code/FixedAmountDiscount.cs) has a
working version.

## 🟡 Optional — The Factory pattern

Something still has to turn a customer type string into the right
strategy object. That decision deserves exactly one place to live:

```csharp
public static class DiscountStrategyFactory
{
    public static IDiscountStrategy Create(string customerType)
    {
        if (customerType == "regular")
        {
            return new NoDiscount();
        }
        else if (customerType == "member")
        {
            return new PercentageDiscount(10);
        }
        else if (customerType == "vip")
        {
            return new FixedAmountDiscount(50);
        }
        else
        {
            throw new ArgumentException($"Unknown customer type: {customerType}");
        }
    }
}
```

Notice the `if`/`else if` chain didn't actually disappear. It moved, and
that's a fair way to put it: the **Factory pattern** doesn't eliminate
conditionals, it *isolates* one, so it's the only place in the whole
program that has to know how to turn a string into an object; the actual
discount math never branches on `customerType` at all anymore.
`static class` means `DiscountStrategyFactory` itself is never
instantiated with `new`: its one method is called directly on the class
name (`DiscountStrategyFactory.Create(...)`), appropriate for something
that holds no state of its own.

## 🔴 Optional, genuine challenge — Extend it without touching what exists

Add a fourth discount, anything you like (a seasonal sale, a bulk
discount), as its own new `IDiscountStrategy` class, and wire it into
`DiscountStrategyFactory`. Confirm you never had to open
`PriceCalculator.cs`, or any of the *existing* strategy classes, to do it:
only new files, plus one new branch in the factory. That's the actual
payoff of all of this, made concrete: the same "extend without modifying"
shape Course 5's polymorphic list and Course 13's events already gave you,
here applied to a real refactor instead of a from-scratch design.
[`code/SeasonalDiscount.cs`](../code/SeasonalDiscount.cs) has one example.

## What you learned

- Recognizing a growing `if`/`else if` chain as a sign a Strategy pattern
  might fit better: an interface plus one small class per actual behavior
- That you've been using this shape since Course 4 (`Machine` subclasses),
  Course 6 (`ICollectible`/`ISellable`), and Course 13 (event subscribers):
  Strategy is a name for something you already knew how to build
- The Factory pattern: isolating "which object do I need" into one place,
  without pretending conditionals disappear entirely
- `static class`, for a type that holds no instance state and is never
  constructed with `new`
- Extending a system by adding new classes instead of editing existing
  ones: the concrete meaning of "open for extension, closed for
  modification"

## What's next

Course 15 completes this repository's currently planned roadmap, Courses
1 through 15. There's no Course 16 planned yet. See
[PROJECT-IDEAS.md](../../../PROJECT-IDEAS.md) for how a future one would
fit in, following the same conventions documented in
[CONTRIBUTING.md](../../../CONTRIBUTING.md).
