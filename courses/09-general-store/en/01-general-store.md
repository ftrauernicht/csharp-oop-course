🇬🇧 English | 🇩🇪 [Deutsch](../de/01-gemischtwarenladen.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1](../../01-basics/en/01-csharp-basics.md) and [Course 2](../../02-cat-roster/en/01-cat-roster.md)

# Course 9 – General Store

**Goal:** make "you don't have enough money" a real, impossible-to-ignore
failure instead of a bug waiting to happen. A purchase that can't succeed
either stops the program with useful information or gets handled on
purpose, but it never silently does nothing. By the end, you'll know how to
define your own exception type and when that beats reusing one .NET
already gives you.

As in Courses 3, 4, 6, and 8, the core exercise below is described in
steps for you to write yourself. The finished version, including the
optional and challenge parts, lives in [`code/`](../code/).

## 🟢 Core — What happens when an action just can't succeed?

```csharp
public void Withdraw(int amount)
{
    Balance -= amount;
}
```

Nothing stops `Balance` from going negative here. You could add an `if`
that just... does nothing when there isn't enough money. But then calling
code has no way to know the withdrawal silently failed, and might
confidently report "purchase complete" when nothing actually happened.
[`throw`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#the-throw-statement)ing
an **exception** instead makes the failure impossible to accidentally
ignore: unless something explicitly catches it, the program stops right
there, with a message pointing at exactly what went wrong.

## 🟢 Core — Defining your own exception

```csharp
public class InsufficientFundsException : Exception
{
    public int Requested { get; }
    public int Available { get; }

    public InsufficientFundsException(int requested, int available)
        : base($"Tried to spend {requested} coins, but only {available} are available.")
    {
        Requested = requested;
        Available = available;
    }
}
```

`: Exception` here is inheritance again, exactly like Course 4's
`Machine` subclasses, just inheriting from a class .NET provides instead
of one you wrote. `: base(message)` calls
[`Exception`](https://learn.microsoft.com/en-us/dotnet/api/system.exception)'s
own constructor with a human-readable message (available afterward as
`.Message`), and `Requested`/`Available` are ordinary properties, exactly
like any class you've written since Course 2. Nothing about `Exception`
stops you from adding your own data to it.

## 🟢 Core — Throwing and catching

```csharp
try
{
    store.Purchase(account, "Sword", 50);
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine($"Purchase failed: {ex.Message}");
}
```

[`try`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#the-try-statement)
wraps code that might throw; `catch (SomeException ex)` runs only if that
exact exception type (or one of its subclasses; exceptions can form
hierarchies too) is thrown inside the `try` block, with `ex` giving you
access to everything on it, including your own custom properties. Code
after the `catch` block keeps running normally. The exception didn't
crash the program, it got handled.

## 🟢 Core exercise — Write Withdraw yourself

Given `Deposit`, already guarding against a nonsensical negative amount:

```csharp
public void Deposit(int amount)
{
    if (amount < 0)
    {
        throw new ArgumentException("Amount cannot be negative.");
    }

    Balance += amount;
}
```

Write `Withdraw` with **two** checks: the same negative-amount guard as
`Deposit`, and a check for whether `amount` exceeds the current `Balance`,
throwing `InsufficientFundsException` if it does.

```csharp
public void Withdraw(int amount)
{
    // your code here
}
```

Test it: withdrawing more than the balance should throw and leave
`Balance` completely unchanged (the subtraction should never run if the
check throws first). If you get stuck,
[`code/BankAccount.cs`](../code/BankAccount.cs) has a working version.

## 🟡 Optional — finally: code that always runs

```csharp
try
{
    store.Purchase(account, "Potion", 12);
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine($"Purchase failed: {ex.Message}");
}
finally
{
    Console.WriteLine("Transaction attempt finished.");
}
```

[`finally`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/exception-handling-statements#the-try-finally-statement)
runs no matter what — whether the `try` block succeeded, threw an
exception that got caught, or (rarer, but worth knowing) threw one that
didn't. It's the right place for cleanup that has to happen either way.

## 🔴 Optional, genuine challenge — Catching more than one exception type

`ArgumentException` is a **built-in** .NET exception. Not every mistake
needs a brand-new exception class of your own; reach for one .NET already
provides when it genuinely fits. Try to trigger both kinds of failure and
catch them separately:

```csharp
try
{
    account.Deposit(-10);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Deposit rejected: {ex.Message}");
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine($"Purchase failed: {ex.Message}");
}
```

Multiple `catch` blocks are checked top to bottom, same as `if`/`else if`:
the first one whose type matches (or is a base type of) the thrown
exception runs, and the rest are skipped. Confirm that swapping the two
`catch` blocks' order still works here (these two exception types are
unrelated to each other, so order doesn't matter). Then look up what
would happen if you `catch (Exception ex)` first instead, before the more
specific types. [`code/Program.cs`](../code/Program.cs) has a working
version of the whole chapter.

## What you learned

- Why silently failing (or returning a magic "it didn't work" value) is
  worse than an exception for a genuine failure case
- Defining a custom exception by inheriting from `Exception`, adding your
  own properties, and building a message via `base(...)`
- `try`/`catch (SomeException ex)`, and that code after a handled
  exception keeps running normally
- `finally`, for code that must run whether the `try` block succeeded or
  failed
- Multiple `catch` blocks, checked top to bottom like `if`/`else if`, and
  reaching for a built-in exception type instead of always inventing your
  own

## Next

Course 9 stands on its own, needing only Courses 1 and 2. For the rest of
this repository's roadmap, see
[PROJECT-IDEAS.md](../../../PROJECT-IDEAS.md).
