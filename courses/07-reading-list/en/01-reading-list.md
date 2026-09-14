🇬🇧 English | 🇩🇪 [Deutsch](../de/01-leseliste.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1](../../01-basics/en/01-csharp-basics.md) and [Course 2](../../02-cat-roster/en/01-cat-roster.md)

# Course 7 – Reading List

**Goal:** a `List<Book>` that survives closing and reopening the program —
saved to a real file on disk, in a format any other program (or a text
editor) can also read. By the end, you'll know how to turn a list of your
own objects into text and back, on purpose, using the format almost every
API and config file on the planet already speaks: JSON.

As in Courses 3, 4, and 6, the core exercise below is described in steps
for you to write yourself. The finished version, including the optional
and challenge parts, lives in [`code/`](../code/).

## 🟢 Core — The problem: everything so far has been temporary

Every course before this one built objects, lists, whole little systems —
and every single one vanished the moment the program stopped running.
That's fine for a demo, but a real reading list needs to remember what you
added yesterday. **Serialization** is turning an object into a format that
can be stored (a file, sent over a network, whatever) and **deserialization**
is turning it back into a real object later. C# has several ways to do
this; this chapter uses [`System.Text.Json`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json),
built into .NET, because JSON is also the format you'll meet constantly
outside this course.

## 🟢 Core — A plain class to save

```csharp
public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsFinished { get; set; }

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
        IsFinished = false;
    }
}
```

Nothing new here — the same shape Course 2 used for `Cat`.

## 🟢 Core — Turning objects into text

```csharp
using System.Text.Json;

var books = new List<Book>
{
    new Book("Dune", "Frank Herbert"),
};

string json = JsonSerializer.Serialize(books);
Console.WriteLine(json);
```

```
[{"Title":"Dune","Author":"Frank Herbert","IsFinished":false}]
```

[`JsonSerializer.Serialize(...)`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializer.serialize)
looks at an object (or, here, a whole `List<Book>`) and produces a string:
every public property becomes a `"Name": value` pair, arrays become `[...]`,
and objects become `{...}` — recognizable even if you've never seen C#, which
is exactly why JSON is everywhere.

## 🟢 Core — Writing and reading a file

```csharp
File.WriteAllText("books.json", json);
```

```csharp
string savedJson = File.ReadAllText("books.json");
```

[`File.WriteAllText(path, contents)`](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.writealltext)
creates the file if it doesn't exist yet, or replaces it entirely if it
does. [`File.ReadAllText(path)`](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.readalltext)
reads the whole thing back as one string — both live in
[`System.IO`](https://learn.microsoft.com/en-us/dotnet/api/system.io),
automatically available via this project's `ImplicitUsings`.

## 🟢 Core — Turning text back into objects

```csharp
var loadedBooks = JsonSerializer.Deserialize<List<Book>>(savedJson) ?? new List<Book>();
```

[`JsonSerializer.Deserialize<T>(...)`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializer.deserialize)
does the reverse: read the JSON text, and build real `Book` objects from
it — matching each JSON property to a constructor parameter by name
(case-insensitively: `"Title"` in the JSON finds `title` in `Book`'s
constructor), then setting any remaining public property (`IsFinished`)
from the leftover JSON afterward. These are genuine, newly-built `Book`
objects, not the ones you started with — proof this really went through
text and back, not just a reference to the same objects in memory.

`Deserialize<T>` can technically return `null` (if the JSON text was
literally `"null"`), so its result type is `List<Book>?`, not `List<Book>`.
[`??`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator)
("null-coalescing") reads as "use the left side, unless it's `null` — then
use the right side instead": here, an empty list rather than `null`, so the
rest of your code never has to check for `null` before using the result.

## 🟢 Core exercise — Wrap it into two reusable methods

Using the four pieces above, write:

```csharp
void SaveBooks(List<Book> booksToSave, string path)
{
    // your code here: serialize booksToSave, then write it to path
}

List<Book> LoadBooks(string path)
{
    // your code here: read the file at path, deserialize it, and
    // return an empty list instead of null if deserialization fails
}
```

Then use them for a full round trip:

```csharp
const string FilePath = "books.json";

var books = new List<Book>
{
    new Book("Dune", "Frank Herbert"),
    new Book("Project Hail Mary", "Andy Weir"),
};

SaveBooks(books, FilePath);
Console.WriteLine($"Saved {books.Count} books to {FilePath}.");

var loadedBooks = LoadBooks(FilePath);
Console.WriteLine($"Loaded {loadedBooks.Count} books from {FilePath}:");
foreach (var book in loadedBooks)
{
    Console.WriteLine($"- {book.Title} by {book.Author} (finished: {book.IsFinished})");
}
```

Run it, then check your project folder — there's a real `books.json` file
sitting next to your code now. Open it in a text editor; it's exactly the
string you saw printed earlier. If you get stuck,
[`code/Program.cs`](../code/Program.cs) has working versions of both
methods.

## 🟡 Optional — Readable JSON

The file you just created is one long line — fine for a program to read,
hard for a human to skim. [`JsonSerializerOptions`](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions)
lets you ask for formatting:

```csharp
var options = new JsonSerializerOptions { WriteIndented = true };
string json = JsonSerializer.Serialize(booksToSave, options);
```

```json
[
  {
    "Title": "Dune",
    "Author": "Frank Herbert",
    "IsFinished": false
  }
]
```

Pass the same `options` to `Serialize` inside `SaveBooks`, delete your old
`books.json`, and run again to see the difference.

One more robustness note, worth knowing even though this chapter's demo
always saves before it ever loads: a real app typically loads *first
thing* on startup, before anything has necessarily been saved yet.
[`File.Exists(path)`](https://learn.microsoft.com/en-us/dotnet/api/system.io.file.exists)
lets `LoadBooks` return an empty list instead of crashing when there's no
save file at all:

```csharp
List<Book> LoadBooks(string path)
{
    if (!File.Exists(path))
    {
        return new List<Book>();
    }

    string json = File.ReadAllText(path);
    return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
}
```

## 🔴 Optional, genuine challenge — Read, modify, save, reload

Persistence only matters if changes actually stick. After loading your
books back:

1. Mark the first book as finished: `loadedBooks[0].IsFinished = true;`
2. Save that same list again, to the same file, overwriting it.
3. Load it once more, into a third variable, and print every book's
   `IsFinished` value.

Confirm the change survived the full round trip — saved, reloaded, and the
update is still there — not just held in the variable you happened to
modify. [`code/Program.cs`](../code/Program.cs) has one way to write it.

## What you learned

- Serialization and deserialization: turning objects into a storable
  format and back
- `JsonSerializer.Serialize(...)` and `JsonSerializer.Deserialize<T>(...)`
- `File.WriteAllText(...)` and `File.ReadAllText(...)`
- Why `Deserialize<T>`'s result is nullable, and `??` as the idiomatic way
  to fall back to a default instead of `null`
- `JsonSerializerOptions` for formatting choices like `WriteIndented`
- `File.Exists(...)`, for handling "there's no save file yet" without
  crashing
- That real persistence means a change survives a full save-and-reload
  cycle, not just staying true for the variable you happened to modify

## Next

Course 7 stands on its own, needing only Courses 1 and 2. This completes
this repository's currently planned roadmap — see
[PROJECT-IDEAS.md](../../../PROJECT-IDEAS.md) for what a future course
might add next.
