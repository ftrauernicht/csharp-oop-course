🇬🇧 English | 🇩🇪 [Deutsch](../de/01-playlist-abfragen.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1](../../01-basics/en/01-csharp-basics.md) and [Course 2](../../02-cat-roster/en/01-cat-roster.md)

# Course 11 – Playlist Queries

**Goal:** filter, sort, and summarize a `List<Song>` without writing a
single manual `foreach` + `if`. By the end, you'll read and write LINQ,
one of the most common things you'll see in real C# code, and know when
it makes code clearer than a loop and when a loop is still the more
honest choice.

As in Courses 3, 4, 6, 8, 9, and 10, the core exercise below is described
in steps for you to write yourself. The finished version, including the
challenge, lives in [`code/`](../code/).

## 🟢 Core — The manual way, as a reminder

```csharp
var rockSongsOld = new List<Song>();
foreach (var song in songs)
{
    if (song.Genre == "Rock")
    {
        rockSongsOld.Add(song);
    }
}
```

Filter, collect, repeat: you've written this exact shape since Course 3.
It works, but the actual *intent* ("give me the rock songs") is buried
inside four lines of *mechanism* (a new list, a loop, an `if`, an `Add`).

## 🟢 Core — Where, and your first lambda

```csharp
var rockSongs = songs.Where(s => s.Genre == "Rock").ToList();
```

One line, same result.
[`Where`](https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/filtering-data)
is a method [`List<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)
gets for free once [LINQ](https://learn.microsoft.com/en-us/dotnet/csharp/linq/)
is involved, already available without any extra `using` thanks to this
project's `ImplicitUsings`. `s => s.Genre == "Rock"` is a **lambda
expression**: a small, unnamed function written inline. Read `s => ...` as
"given a song, called `s` here, ...". `s` is the parameter (its type,
`Song`, is inferred from context), and everything after `=>` is what gets
evaluated for each one. `Where` calls this lambda once per item and keeps
only the ones it returns `true` for. `.ToList()` at the end turns the
result back into a real `List<Song>` you can use exactly like any other.

## 🟢 Core — Select: reshaping, not just filtering

```csharp
var titles = songs.Select(s => s.Title).ToList();
```

[`Select`](https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/projection-operations)
runs its lambda once per item too, but keeps *what the lambda returns*
instead of the original items: here, a `List<string>` (just titles), not
a `List<Song>` anymore. `Where` picks a subset of what you have; `Select`
transforms every item into something else entirely.

## 🟢 Core — OrderBy

```csharp
var byDuration = songs.OrderBy(s => s.DurationSeconds).ToList();
```

[`OrderBy`](https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/sorting-data)
sorts by whatever the lambda returns for each item. Unlike Course 10's
`List<T>.Sort()`, no `IComparable<T>` is required. That's the tradeoff:
`Sort()` needs the type itself to know how to compare its own instances;
`OrderBy` just needs you to point at the property to sort by, right where
you're using it.
[`OrderByDescending`](https://learn.microsoft.com/en-us/dotnet/api/system.linq.enumerable.orderbydescending)
is the same idea, reversed.

## 🟢 Core — Aggregating: Sum, Count, and friends

```csharp
int totalDuration = songs.Sum(s => s.DurationSeconds);
```

[`Sum`](https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/aggregation-operations)
adds up whatever the lambda returns for every item. `Count()`, `Average()`,
`Max()`, and `Min()` all follow the same shape: no loop, no running total
variable to manage yourself.

## 🟢 Core exercise — Chain Where and OrderByDescending

Find every song longer than 200 seconds, longest first:

```csharp
var longSongsDescending = songs
    // your code here: .Where(...) for songs over 200 seconds,
    // then .OrderByDescending(...) by duration, then .ToList()
```

LINQ methods chain naturally. Each one returns something you can call the
next method on, so `songs.Where(...).OrderByDescending(...).ToList()` reads
almost like a sentence: "take the songs, keep the long ones, sort them by
duration descending, and give me a list." If you get stuck,
[`code/Program.cs`](../code/Program.cs) has a working version.

## 🔴 Optional, genuine challenge — GroupBy

Group the whole playlist by genre and print how many songs are in each:

```csharp
var byGenre = songs.GroupBy(s => s.Genre);

foreach (var group in byGenre)
{
    Console.WriteLine($"{group.Key}: {group.Count()} song(s)");
}
```

[`GroupBy`](https://learn.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/grouping-data)
is the one LINQ method here that doesn't have a simple manual-loop
equivalent worth writing by hand. Each `group` you get back acts like a
small list of just that genre's songs (with `.Key` telling you which genre
it is), so you can `foreach`, `Count()`, or run any other LINQ method on it
too. Try it yourself before peeking at
[`code/Program.cs`](../code/Program.cs).

## What you learned

- Lambda expressions (`s => ...`), an inline, unnamed function
- `Where` (filter), `Select` (reshape), `OrderBy`/`OrderByDescending`
  (sort by a key, no `IComparable<T>` needed), `Sum` (and its aggregation
  siblings)
- Chaining LINQ methods to read like a description of what you want,
  instead of a loop describing how to get it
- `GroupBy`, for splitting a collection into named sub-groups

## Next

Course 11 stands on its own, needing only Courses 1 and 2. For the rest of
this repository's roadmap, see
[PROJECT-IDEAS.md](../../../PROJECT-IDEAS.md).
