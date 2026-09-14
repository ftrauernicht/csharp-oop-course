🇬🇧 English | 🇩🇪 [Deutsch](../de/01-geldbeutel.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1](../../01-basics/en/01-csharp-basics.md) and [Course 2](../../02-cat-roster/en/01-cat-roster.md)

# Course 10 – Coin Purse

**Goal:** two `Coin` objects that represent the same denomination count as
equal, sort correctly next to each other, and don't create duplicates in a
`HashSet`. By the end, you'll know why none of that happens automatically
for a class you write, and the standard pattern that fixes it.

As in Courses 3, 4, 6, 8, and 9, the core exercise below is described in
steps for you to write yourself. The finished version, including the
challenge, lives in [`code/`](../code/).

## 🟢 Core — The surprise: `==` compares references by default

```csharp
public class NaiveCoin
{
    public int Denomination { get; }

    public NaiveCoin(int denomination)
    {
        Denomination = denomination;
    }
}
```

```csharp
var naive1 = new NaiveCoin(5);
var naive2 = new NaiveCoin(5);
Console.WriteLine(naive1 == naive2); // False
```

`False` — even though both represent "5 cents". For a plain class, `==`
and the inherited [`Equals`](https://learn.microsoft.com/en-us/dotnet/api/system.object.equals)
compare *reference equality*: "is this literally the same object in
memory," not "do these two objects represent the same value." `int` and
`string` feel like they compare values because .NET already overrides
this behavior for them — a class you write doesn't get that for free.

## 🟢 Core — Equals and GetHashCode, together

```csharp
public override bool Equals(object? obj)
{
    if (obj is not Coin other)
    {
        return false;
    }

    return Denomination == other.Denomination;
}

public override int GetHashCode()
{
    return Denomination.GetHashCode();
}
```

`obj is not Coin other` combines Course 6's pattern-matching `is` with
`not` — `false` if `obj` isn't a `Coin` at all, otherwise `other` is ready
to use, already typed as `Coin`. **These two overrides are a matched
pair, by contract**: if `Equals` says two objects are equal, `GetHashCode`
**must** return the same value for both, or collection types that rely on
hashing (`HashSet<T>`, `Dictionary<TKey, TValue>`) will misbehave in ways
that are genuinely hard to debug. Here, both are entirely driven by
`Denomination`, so the contract holds automatically. More:
[Microsoft Learn – Equals and GetHashCode](https://learn.microsoft.com/en-us/dotnet/api/system.object.equals#notes-to-inheritors).

## 🟢 Core — Overloading == and !=

```csharp
public static bool operator ==(Coin? left, Coin? right)
{
    if (left is null)
    {
        return right is null;
    }

    return left.Equals(right);
}

public static bool operator !=(Coin? left, Coin? right)
{
    return !(left == right);
}
```

Overriding `Equals` alone doesn't change what the `==` *operator* does —
they're two separate things that happen to usually agree. `static bool
operator ==(...)` is
[**operator overloading**](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-overloading):
defining what `==` itself means for your type. C# requires `==` and `!=`
to be overloaded together, as a pair. The `left is null` check matters —
without it, `left.Equals(right)` would throw if `left` itself were `null`,
which is exactly the kind of case `==` needs to handle gracefully.

```csharp
var coin1 = new Coin(5);
var coin2 = new Coin(5);
Console.WriteLine(coin1 == coin2); // True
```

## 🟢 Core — What this actually buys you: deduplication

```csharp
var uniqueCoins = new HashSet<Coin>
{
    new Coin(5),
    new Coin(10),
    new Coin(5), // a duplicate
};

Console.WriteLine(uniqueCoins.Count); // 2
```

[`HashSet<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1)
only keeps one of anything it considers equal — and "considers equal"
means exactly `Equals`/`GetHashCode`, the pair you just wrote. Without
them, `NaiveCoin(5)` and another `NaiveCoin(5)` would both make it in,
since neither one equals the other by reference.

## 🟢 Core exercise — Write IComparable\<Coin\> yourself

```csharp
public class Coin : IComparable<Coin>
{
    // ... Equals, GetHashCode, ==, != from above ...

    public int CompareTo(Coin? other)
    {
        // your code here: return a negative number if this coin is worth
        // less than `other`, zero if equal, positive if worth more --
        // return 1 if `other` is null (this coin "comes after" nothing)
    }
}
```

[`IComparable<T>`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/reference-types#the-icomparable-interfaces)
is what
[`List<T>.Sort()`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.sort)
actually calls, internally, to decide ordering. You don't have to write
the comparison logic by hand — `int` already has `.CompareTo(...)`, and
`Denomination` is an `int`, so this is one line delegating to it. Test it:

```csharp
var purse = new List<Coin> { new Coin(25), new Coin(1), new Coin(10), new Coin(5) };
purse.Sort();
Console.WriteLine(string.Join(", ", purse)); // 1, 5, 10, 25 cents, in order
```

If you get stuck, [`code/Coin.cs`](../code/Coin.cs) has a working version.

## 🟡 Optional — ToString, overridden too

`string.Join(", ", purse)` above only reads cleanly because `Coin`
overrides one more inherited method:

```csharp
public override string ToString()
{
    return $"{Denomination}-cent coin";
}
```

Every class already has a `ToString()` — the default one just prints the
class's name, which is why `Console.WriteLine(someObject)` on a
non-overridden class prints something unhelpful like `Coin`. Overriding it
is what makes `$"{coin}"` and `Console.WriteLine(coin)` print something
meaningful, without needing to remember to write `coin.Denomination`
everywhere instead.

## 🔴 Optional, genuine challenge — Comparison operators, built on CompareTo

Add `<` and `>` to `Coin`, defined in terms of the `CompareTo` you already
wrote:

```csharp
public static bool operator <(Coin left, Coin right)
{
    // your code here
}

public static bool operator >(Coin left, Coin right)
{
    // your code here
}
```

C# requires `<`/`>` as a pair too (and separately, `<=`/`>=` as their own
pair, which this challenge doesn't ask for). Confirm
`new Coin(5) < new Coin(10)` is `true` and `new Coin(25) > new Coin(10)`
is `true`. [`code/Coin.cs`](../code/Coin.cs) has a working version.

## What you learned

- Why `==`/`Equals` compare references by default for a class you write,
  not values
- Overriding `Equals` and `GetHashCode` together — and why they must agree
- Overloading `==`/`!=` as operators, separately from overriding `Equals`
- Why `HashSet<T>` deduplication depends entirely on a correct
  `Equals`/`GetHashCode` pair
- `IComparable<T>` and `CompareTo`, what `List<T>.Sort()` actually calls
- Overriding `ToString()`, and `<`/`>` built on top of `CompareTo`

## Next

Course 10 stands on its own, needing only Courses 1 and 2. For the rest of
this repository's roadmap, see
[PROJECT-IDEAS.md](../../../PROJECT-IDEAS.md).
