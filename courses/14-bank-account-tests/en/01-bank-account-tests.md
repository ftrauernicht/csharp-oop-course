🇬🇧 English | 🇩🇪 [Deutsch](../de/01-bankkonto-tests.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1](../../01-basics/en/01-csharp-basics.md) and [Course 2](../../02-cat-roster/en/01-cat-roster.md)

# Course 14 – Bank Account Tests

**Goal:** prove a class's validation and exception-throwing behavior
actually works, automatically, every time, without you re-reading
`Console.WriteLine` output with your own eyes. By the end, you'll know how
a test project is structured, differently from every course before this
one, and how to write and run real xUnit tests.

This course rebuilds a small version of Course 9's `BankAccount`
standalone, on purpose — the same sharing arrangement Courses 4 and 5 use,
just applied here to "the thing being tested" instead of "the domain being
extended." As in Courses 3, 4, 6, 8, 9, 10, 11, 12, and 13, the core
exercise below is described in steps for you to write yourself. The
finished version, including the optional and challenge parts, lives in
[`code/`](../code/).

## 🟢 Core — Why this course has two projects

Every course before this one was a single console app you could
`dotnet run`. Tests are different: they need to run *against* your code,
not run standalone printing things for you to eyeball. This course's
[`code/`](../code/) folder holds **two** projects instead of one:

- `BankAccount/` — a **class library** (`dotnet new classlib`), just
  `BankAccount.cs` and `InsufficientFundsException.cs`, no `Program.cs` at
  all. A class library has no entry point, because nothing runs it
  directly.
- `BankAccount.Tests/` — an **xUnit test project** (`dotnet new xunit`),
  referencing the class library (`dotnet add reference ../BankAccount/BankAccount.csproj`)
  so its tests can actually see `BankAccount` and `InsufficientFundsException`.

You don't need to memorize those two `dotnet new`/`dotnet add` commands
right now. They only run once, when a test project is first set up; from
here on, everything happens inside the files they created.

## 🟢 Core — Your first test

```csharp
public class BankAccountTests
{
    [Fact]
    public void Deposit_IncreasesBalance()
    {
        // Arrange
        var account = new BankAccount(100);

        // Act
        account.Deposit(50);

        // Assert
        Assert.Equal(150, account.Balance);
    }
}
```

[`[Fact]`](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test)
marks a method as a test xUnit should actually run — without it, this
would just be an ordinary, never-called method.
[`Assert.Equal(expected, actual)`](https://learn.microsoft.com/en-us/dotnet/api/xunit.assert.equal)
fails the test (with a clear message showing both values) if they don't
match. The three comments — **Arrange** (set up what you need), **Act**
(do the one thing you're testing), **Assert** (check the result) — are a
convention worth keeping even once you stop writing the comments
themselves. It keeps a test's three separate jobs from blurring together.

Run every test in a project from a terminal, inside
`BankAccount.Tests/`:

```
dotnet test
```

## 🟢 Core exercise — Test that an exception is actually thrown

```csharp
[Fact]
public void Withdraw_ThrowsInsufficientFundsException_WhenBalanceTooLow()
{
    // your code here: Arrange a BankAccount with a small balance, then
    // Assert that calling Withdraw with too much throws
    // InsufficientFundsException
}
```

[`Assert.Throws<TException>(() => ...)`](https://learn.microsoft.com/en-us/dotnet/api/xunit.assert.throws)
takes a lambda (Course 11's `=>` syntax, used here for something other
than LINQ) containing the one line expected to throw, and fails the test
if it *doesn't* — the opposite of `try`/`catch` from Course 9, which reacts
to an exception. A test instead makes throwing the exception itself the
thing being checked. Run `dotnet test` and confirm it passes. If you get
stuck, [`code/BankAccount.Tests/BankAccountTests.cs`](../code/BankAccount.Tests/BankAccountTests.cs)
has a working version.

## 🟡 Optional — Testing more than "did it throw"

```csharp
[Fact]
public void Withdraw_LeavesBalanceUnchanged_WhenItThrows()
{
    var account = new BankAccount(20);

    try
    {
        account.Withdraw(50);
    }
    catch (InsufficientFundsException)
    {
    }

    Assert.Equal(20, account.Balance);
}
```

Confirming an exception gets thrown isn't the whole story — Course 9's
`Withdraw` is only correct if `Balance` is *also* still exactly what it was
before, not partially changed. This test catches the exception on purpose
(an empty `catch` is normally a red flag, but here it's deliberate: this
test's whole point is what happens *after* the throw) and then checks the
state directly.

## 🔴 Optional, genuine challenge — One test, several inputs

```csharp
[Theory]
[InlineData(-1)]
[InlineData(-100)]
public void Deposit_ThrowsArgumentException_ForNegativeAmounts(int amount)
{
    var account = new BankAccount(100);

    Assert.Throws<ArgumentException>(() => account.Deposit(amount));
}
```

[`[Theory]`](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test)
with one or more `[InlineData(...)]` runs the *same* test method once per
data row, with `amount` bound to each value in turn — two actual test runs
from one method, instead of copy-pasting the whole test twice for `-1` and
`-100`. Add a third `[InlineData(...)]` of your own and confirm `dotnet
test` now reports one more passing test than before.
[`code/BankAccount.Tests/BankAccountTests.cs`](../code/BankAccount.Tests/BankAccountTests.cs)
has a working version of the whole chapter.

## What you learned

- Why a test project is structured differently: a class library with no
  entry point, plus a separate test project referencing it
- `[Fact]`, `Assert.Equal`, and the Arrange-Act-Assert shape of a test
- `Assert.Throws<TException>(() => ...)`, for making "this throws" itself
  the thing under test
- Testing state after an exception, not just whether one was thrown
- `[Theory]` + `[InlineData]`, for running one test method against several
  inputs instead of duplicating it

## Next

Course 14 stands on its own, needing only Courses 1 and 2. For the rest of
this repository's roadmap, see
[PROJECT-IDEAS.md](../../../PROJECT-IDEAS.md).
