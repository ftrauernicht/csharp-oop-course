🇬🇧 English | 🇩🇪 [Deutsch](../de/01-csharp-grundlagen.md)

[← Back to course overview](../../../README.md) · Previous: [Chapter 0 – Introduction & Tools](00-introduction.md)

# Chapter 1 – C# Basics

**Goal:** the small set of building blocks every C# program is made of —
values, variables, operators, methods, decisions, and repetition. Chapter 0
showed you *how* to create and run a project; this chapter is about *what*
you're allowed to write inside `Program.cs`. Nothing here is throwaway:
Course 2 and everything after it uses every single one of these.

Keep the project you created in [Chapter 0](00-introduction.md) open, and
type each example straight into `Program.cs` as you go. Replace the
`Hello, World!` line and just keep adding below it. A finished version of
this whole chapter, including the challenge at the end, lives in
[`code/Program.cs`](../code/Program.cs), in case you want to compare.

## 🟢 Core — Talking to the console

Two methods you'll use constantly:

```csharp
Console.WriteLine("What's your name?");
var name = Console.ReadLine();

Console.WriteLine($"Hello, {name}!");
```

[`Console.WriteLine`](https://learn.microsoft.com/en-us/dotnet/api/system.console.writeline)
prints a line of text.
[`Console.ReadLine`](https://learn.microsoft.com/en-us/dotnet/api/system.console.readline)
pauses your program and waits for the user to type something and press
Enter, handing you back what they typed.

That `$"Hello, {name}!"` is **string interpolation**: a `$` before the
quotes lets you drop a variable straight into the text inside `{}`, instead
of gluing pieces together by hand. You'll use this in almost every line of
output from here on. More:
[Microsoft Learn – String interpolation](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated).

Run it (<kbd>F5</kbd>, or `dotnet run` in a terminal) and actually type a
name when it asks. This course only really clicks once you're running your
own code, not just reading it.

(You might notice Visual Studio underlines `name` with a warning about a
"possible null reference": `Console.ReadLine()` can technically return
nothing at all if the input stream closes unexpectedly. Harmless here; not
worth worrying about yet.)

## 🟢 Core — Values and their types

A **value** is a single piece of data. Four kinds that show up in almost
every program:

```csharp
int age = 16;          // a whole number
double price = 4.5;    // a number with a decimal point
bool isStudent = true;  // a boolean — only ever true or false
string city = "Berlin"; // text
```

Here's the biggest difference from a language like JavaScript: C# is
**statically typed**. Once you declare `age` as an `int`, it can only ever
hold a whole number: `age = "sixteen";` doesn't run with a weird result,
it flat-out refuses to compile. That's the compiler from Chapter 0 at work
again: a whole class of bugs (mixing up what kind of data you're holding)
gets caught before your program ever starts. More:
[Microsoft Learn – Built-in types](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types).

You already met `var` in the console example above: it doesn't mean "any
type", it means "figure out the type from what I'm assigning, and lock it
in". `var name = Console.ReadLine();` is exactly as statically typed as
writing the type out yourself; it's just shorter to type when the type is
obvious from the right-hand side. Either style is fine in this course; you
now know why they're equivalent. More:
[Microsoft Learn – Implicitly typed local variables](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/implicitly-typed-local-variables).

## 🟢 Core — Operators

You've already used arithmetic ones
(`+` `-` `*` `/` `%`, see
[Microsoft Learn – Arithmetic operators](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/arithmetic-operators)).
Three more families matter just as much:

**Assignment**: `=` stores a value; the others are shorthand for "take the
current value, do something to it, store it back":

```csharp
int score = 10;
score += 5; // same as: score = score + 5;  → 15
score *= 2; // same as: score = score * 2;  → 30
```

**Comparison**: asking a true/false question about two values:

```csharp
5 == 5   // true
5 != 3   // true (not equal)
5 < 10   // true
5 >= 5   // true
```

Unlike some languages, there's no gotcha to warn you about here: because C#
is statically typed, `5 == "5"` doesn't even compile. A number and a string
can never accidentally be treated as equal. One equality operator, and it's
always safe. More:
[Microsoft Learn – Equality operators](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/equality-operators).

**Logical**: combining or inverting true/false values:

```csharp
true && false // false ("and" — both sides must be true)
true || false // true  ("or" — at least one side must be true)
!true         // false ("not" — flips it)
```

**Ternary (conditional)**: a compact, one-line if/else that produces a
*value* instead of running a block:

```csharp
int age = 16;
string label = age >= 18 ? "adult" : "minor";
// label is "adult" if age >= 18, otherwise "minor"
```

Read `condition ? valueIfTrue : valueIfFalse` left to right. Most useful for
short either/or choices like this one; for anything longer, a full
`if`/`else` (below) reads more clearly. More:
[Microsoft Learn – Conditional operator](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator).

## 🟢 Core — Parentheses and curly braces

**Parentheses `()`** have two jobs. Grouping, to control order of
evaluation exactly like in math:

```csharp
(2 + 3) * 4 // 20, not 14
```

...and calling a method, or listing what it accepts:

```csharp
Console.WriteLine("hi");              // calling WriteLine with one value
int Add(int a, int b) { ... }         // a and b are listed inside parentheses
```

**Curly braces `{}`** mark a **block**: a group of statements bundled
together that run as one unit. You'll see them around a method's body, an
`if`'s body, and a loop's body (all coming up in this same chapter). This
course doesn't need square brackets `[]` yet. They show up once collections
of values become useful, in a later course.

## 🟢 Core — Methods: naming a piece of behavior

```csharp
int Add(int a, int b)
{
    return a + b;
}

Console.WriteLine(Add(3, 4)); // 7
```

`a` and `b` are **parameters**: placeholders for whatever values get passed
in when the method is called, each with its own declared type. `int` before
`Add` is the **return type**: the type of value `return` sends back to
whoever called the method. A method whose job is only to print something and
hand nothing back uses `void` instead of a type:

```csharp
void Greet(string personName)
{
    Console.WriteLine($"Hi, {personName}!");
}

Greet("Alex"); // prints: Hi, Alex!
```

You'll build these properly, as part of an actual class, starting in
Course 2. For now, just the shape, so nothing looks unfamiliar later. More:
[Microsoft Learn – Methods](https://learn.microsoft.com/en-us/dotnet/csharp/methods).

## 🟢 Core — Making decisions: if / else

```csharp
int temperature = 8;

if (temperature < 10)
{
    Console.WriteLine("Wear a jacket.");
}
else if (temperature < 20)
{
    Console.WriteLine("A light sweater will do.");
}
else
{
    Console.WriteLine("Shorts weather!");
}
```

C# checks the conditions top to bottom and runs the block belonging to the
first one that's `true`; the rest are skipped entirely. `else` (and
`else if`) are both optional. More:
[Microsoft Learn – if statement](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements#the-if-statement).

## 🟢 Core — Repeating yourself: loops

A **loop** runs the same block of code multiple times. The
[`for`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-for-statement)
loop is the workhorse when you know roughly how many times you want to
repeat something:

```csharp
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine(i);
}
// 1
// 2
// 3
// 4
// 5
```

Its three parts, separated by semicolons: **start** (`int i = 1`: runs
once, before anything else), **condition** (`i <= 5`: checked before every
run; the loop stops the moment this is `false`), and **step** (`i++`: runs
after every iteration). `i++` is shorthand for `i = i + 1`.

[`while`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-while-statement)
is the simpler sibling, for when you don't know the exact count up front;
it just keeps going as long as its condition stays true:

```csharp
int count = 0;
while (count < 3)
{
    Console.WriteLine("Hello!");
    count++;
}
```

**Try it yourself:** write a `for` loop that prints the numbers 1 through
10. Then write one that prints only the even numbers, using `%` to check
"is this number divisible by 2?".

## 🟡 Optional — Comments

```csharp
// a single-line comment
/* a
   multi-line
   comment */
```

The compiler ignores comments entirely: they're notes for humans reading
the code, including future-you. Good practice (and this course's own rule)
is to comment the *why*, not the *what*: if the code already says what it
does, a comment repeating that just adds noise.

## 🟡 Optional — const, and when to reach for it

```csharp
const double TaxRate = 0.19;
```

[`const`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/const)
declares a value that's fixed at compile time and can never be reassigned,
useful for values that are genuinely constant for the life of your program
(a tax rate, the number of days in a week), as opposed to a regular variable
that's just not reassigned *in this particular run*.

## 🔴 Optional, genuine challenge — FizzBuzz

A small, famous exercise that combines loops, conditionals, and the modulo
operator (`%`). A solid capstone before moving on:

> Loop through the numbers 1 to 15. For each one: print `"Fizz"` if it's
> divisible by 3, `"Buzz"` if it's divisible by 5, `"FizzBuzz"` if it's
> divisible by both, otherwise print the number itself.

Try it yourself before peeking at [`code/Program.cs`](../code/Program.cs):
you have every tool you need from this chapter alone (a `for` loop,
`if`/`else if`/`else`, and `%`). A hint, if you want one: check "divisible
by both" *before* checking either one alone, or the more specific case never
gets a chance to run.

## What you learned

- Reading from and writing to the console (`Console.WriteLine`,
  `Console.ReadLine`), and string interpolation (`$"..."`)
- Values and their types (`int`, `double`, `bool`, `string`), static typing,
  and `var`
- Operators: arithmetic, assignment, comparison, logical, ternary
- Parentheses `()` for grouping/calling, curly braces `{}` for blocks
- Methods, by shape (parameters, return type, `void`)
- Conditionals (`if`/`else if`/`else`)
- Loops (`for`, `while`)
- *(optional)* Comments, `const`

## Next

This was Course 1's last chapter. Continue to
[Course 2 – Cat Roster](../../02-cat-roster/en/01-cat-roster.md), which
picks up immediately from here: you'll take these exact building blocks and
group them, for the first time, into a class, your first real step into
object-oriented programming.
