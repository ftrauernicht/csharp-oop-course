🇬🇧 English | 🇩🇪 [Deutsch](../de/01-persoenliche-bibliothek.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1](../../01-basics/en/01-csharp-basics.md) and [Course 2](../../02-cat-roster/en/01-cat-roster.md)

# Course 8 – Personal Library

**Goal:** encapsulate a *collection*, not just a single value the way
Course 3's `WaterLevel` did. By the end, you'll know why handing out your
class's internal list directly is a common, easy-to-miss way to break
encapsulation entirely, and what the standard fix looks like.

As in Courses 3-7, the core exercise below is described in steps for you to
write yourself. The finished version, including the challenge, lives in
[`code/`](../code/).

## 🟢 Core — The problem: handing out the list itself

```csharp
public class BadLibrary
{
    public List<Book> Books { get; } = new List<Book>();
}
```

This looks harmless. `Books` is a `get`-only property, so nobody can
*replace* it with a different list. But `List<T>` itself is mutable, and
handing out a reference to it hands out full control over its contents:

```csharp
var badLibrary = new BadLibrary();
badLibrary.Books.Add(new Book("Dune", "Frank Herbert"));
badLibrary.Books.Clear(); // nothing stops this -- the whole library, gone
```

`Clear()` isn't a typo or an edge case, but a completely ordinary
`List<T>` method, available to *anyone* holding a reference to that list,
with zero involvement from `BadLibrary` itself. A `get`-only property
protects the reference; it does nothing at all to protect what that
reference points to.

## 🟢 Core — IReadOnlyList\<T\>, and how much it actually protects

```csharp
public class Library
{
    private readonly List<Book> _books = new List<Book>();

    public IReadOnlyList<Book> Books
    {
        get { return _books.AsReadOnly(); }
    }

    public void AddBook(Book book)
    {
        _books.Add(book);
        Console.WriteLine($"Added \"{book.Title}\" to the library.");
    }
}
```

Two changes fix the problem:

- The actual list is now a `private readonly` field, `_books`: nothing
  outside `Library` can reach it directly at all.
- `Books` returns [`IReadOnlyList<Book>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)
  instead of `List<Book>`, an interface with no `Add`, `Remove`, or
  `Clear` at all. `library.Books.Add(...)` doesn't just misbehave, it
  **fails to compile**: `error CS1061: 'IReadOnlyList<Book>' does not
  contain a definition for 'Add'`.

One subtlety worth knowing, because it's easy to get almost right:
`return _books;` (a plain upcast to `IReadOnlyList<Book>`) blocks this at
*compile time* only. The object underneath is still the exact same
`List<Book>`. Determined code could cast it right back
(`(List<Book>)library.Books`) and mutate it anyway.
[`_books.AsReadOnly()`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.asreadonly)
is stronger: it wraps `_books` in a genuine, separate `ReadOnlyCollection<T>`
object. Casting *that* back to `List<Book>` doesn't just misbehave either.
It throws an `InvalidCastException` at runtime, because it really isn't a
`List<Book>` underneath. Prefer `AsReadOnly()` for exactly that reason.

## 🟢 Core exercise — Write RemoveBook yourself

```csharp
public bool RemoveBook(string title)
{
    // your code here: find the book with this title in _books, remove
    // it, print a confirmation, and return true -- or, if no book with
    // that title exists, print that and return false
}
```

Everything you need is already familiar: an indexed `for` loop over
`_books`, comparing `_books[i].Title` to `title`, and
[`List<T>.RemoveAt(index)`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1.removeat)
once you've found it. Test it against a `Library` with a couple of books
added. If you get stuck, [`code/Library.cs`](../code/Library.cs) has a
working version.

## What you learned

- Why a `get`-only property doesn't protect a *mutable* object it returns
  a reference to, only the reference itself
- `IReadOnlyList<T>`, and the difference between a plain upcast (blocks
  misuse at compile time only) and `.AsReadOnly()` (blocks it at runtime
  too, by wrapping the list in a genuinely separate object)
- Applying Course 3's encapsulation lesson to a collection instead of a
  single value

## Next

Course 8 stands on its own, needing only Courses 1 and 2. For the rest of
this repository's roadmap, see
[PROJECT-IDEAS.md](../../../PROJECT-IDEAS.md).
