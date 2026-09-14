🇬🇧 English | 🇩🇪 [Deutsch](README.de.md)

# C# OOP Course

A beginner-friendly, project-based course for genuinely *understanding*
object-oriented programming — not just being able to recite the words
"encapsulation" or "polymorphism", but having built small, working things
with them. C# is the language, chosen because its compiler is a patient
teacher: it explains exactly what's wrong, in a full sentence, before your
program even runs. It's built to be handed over as-is: send someone the
link to this repository, and everything they need to begin is right here.
No prior programming experience is assumed on their part. Every chapter
builds on the one before it, and every chapter has an optional "go further"
path for anyone who wants more of a challenge.

This course exists **in German and English side by side**: every chapter is
written as two separate files with the same content, so pick whichever
language you read more comfortably — and switch any time. Code and code
comments, however, are always written in English. That is a deliberate
choice and a real-world convention: professional codebases are written in
English regardless of which language the team speaks, so it is worth
getting used to from the very first line.

Object-oriented programming is introduced gradually and on purpose: Course 1
is plain, procedural C# — no classes yet. New ideas show up one at a time,
each earned through a small project, rather than all at once in a wall of
vocabulary. See [PROJECT-IDEAS.md](PROJECT-IDEAS.md) for the full roadmap of
where this is headed, from your first class through inheritance,
polymorphism, and interfaces.

## Get this course onto your computer

**Option A — no Git required:**

1. Go to <https://github.com/ftrauernicht/csharp-oop-course>.
2. Click the green **Code** button → **Download ZIP**.
3. Extract the downloaded ZIP file anywhere on your computer (right-click it
   → *Extract All* on Windows, or double-click it on Mac).
4. Open the extracted folder. You now have every file this course needs.

**Option B — with [Git](https://git-scm.com/), if you already have it installed:**

```
git clone https://github.com/ftrauernicht/csharp-oop-course.git
```

Either way, unlike a course that runs in a browser, C# needs an IDE
installed before you can run anything — Course 1, Chapter 0 walks you
through installing it step by step. Start reading at
[Course 1 – Basics, Chapter 0](courses/01-basics/en/00-introduction.md).

## What you need before you start

| Tool | Why you need it | Link |
|---|---|---|
| [Visual Studio Community](https://visualstudio.microsoft.com/vs/community/) | The free IDE this course's instructions and menus assume — writes, compiles, and runs your C# | [visualstudio.microsoft.com](https://visualstudio.microsoft.com/vs/community/) |
| The .NET SDK | Installed automatically by the Visual Studio installer once you pick the "**.NET desktop development**" workload | Bundled with Visual Studio |

Step-by-step setup instructions (including which installer options to pick)
are in
[Course 1 – Basics, Chapter 0](courses/01-basics/en/00-introduction.md).

## Courses

This repository is meant to hold more than one course over time. Each one
gets its own number under `courses/`, in the order it was written.

Courses 1 and 2 together are the shared foundation every later course
assumes — object-oriented ideas build on each other more directly than,
say, "how do I fetch data", so this repository keeps those two required
instead of making every single course independent. Course 3 onward are each
independent, standalone projects that only assume Courses 1 and 2 — pick
whichever sounds more interesting, in whatever order you like.

### Course 1 – Basics (`courses/01-basics/`)

Not tied to any one project — plain, procedural C#: no classes yet, on
purpose. The shared vocabulary Course 2 and everything after it assumes.

| # | Chapter | What you'll learn |
|---|---|---|
| 0 | [Introduction & Tools](courses/01-basics/en/00-introduction.md) | Installing Visual Studio, creating and running a console project, why C# needs a compiler |
| 1 | [C# Basics](courses/01-basics/en/01-csharp-basics.md) | Values, static typing, variables, operators, string interpolation, methods, conditionals, loops |

### Course 2 – Cat Roster (`courses/02-cat-roster/`)

Assumes Course 1. Your first real step into OOP — a handful of independent
`Cat` objects that introduce themselves.

| # | Chapter | What you'll learn |
|---|---|---|
| 1 | [Cat Roster](courses/02-cat-roster/en/01-cat-roster.md) | Classes vs. objects, properties, constructors, `this`, instance methods that read or change an object's own state |

### Course 3 – Homestead Inventory (`courses/03-homestead-inventory/`)

Assumes Courses 1 and 2. A small farm tracking any number of crops, each
enforcing its own rules about how much it can be watered.

| # | Chapter | What you'll learn |
|---|---|---|
| 1 | [Homestead Inventory](courses/03-homestead-inventory/en/01-homestead-inventory.md) | `List<T>`, `foreach`, private fields, get-only and privately-settable properties, encapsulation via real validation logic |

### Course 4 – Machine Hunter (`courses/04-machine-hunter/`)

Assumes Courses 1 and 2. A `Machine` base class and a handful of concrete
machine types that each attack in their own way.

| # | Chapter | What you'll learn |
|---|---|---|
| 1 | [Machine Hunter](courses/04-machine-hunter/en/01-machine-hunter.md) | Inheritance, `virtual`/`override`, `base(...)`, `protected` |

### Course 5 – Machine Showdown (`courses/05-machine-showdown/`)

Assumes Courses 1 and 2 (shares a domain with, but doesn't require,
Course 4). Every machine type in a single list, one loop, each still
attacking its own way.

| # | Chapter | What you'll learn |
|---|---|---|
| 1 | [Machine Showdown](courses/05-machine-showdown/en/01-machine-showdown.md) | Abstract classes and methods, polymorphism, the `is` type-testing operator |

### Course 6 – Crafting Bench (`courses/06-crafting-bench/`)

Assumes Courses 1 and 2. Items that can be collected, some of which can
also be sold — two independent capabilities via interfaces.

| # | Chapter | What you'll learn |
|---|---|---|
| 1 | [Crafting Bench](courses/06-crafting-bench/en/01-crafting-bench.md) | Interfaces, composition over inheritance, implementing more than one interface, `is Type variableName` pattern matching |

### Course 7 – Reading List (`courses/07-reading-list/`)

Assumes Courses 1 and 2. A `List<Book>` that survives closing and
reopening the program, saved to a real JSON file on disk.

| # | Chapter | What you'll learn |
|---|---|---|
| 1 | [Reading List](courses/07-reading-list/en/01-reading-list.md) | `System.Text.Json`, serializing and deserializing objects, basic file I/O |

Course 7 completed this repository's originally planned arc, from plain
procedural C# through your first class, encapsulation, inheritance,
polymorphism, interfaces, and persistence. Courses 8 and up extend it with
more advanced, sharply-focused topics — see
[PROJECT-IDEAS.md](PROJECT-IDEAS.md) for the full list.

### Course 8 – Personal Library (`courses/08-personal-library/`)

Assumes Courses 1 and 2. Why handing out a class's internal list directly
breaks encapsulation, and the fix.

| # | Chapter | What you'll learn |
|---|---|---|
| 1 | [Personal Library](courses/08-personal-library/en/01-personal-library.md) | `IReadOnlyList<T>`, `.AsReadOnly()` vs. a plain upcast, encapsulating a collection |

### Course 9 – General Store (`courses/09-general-store/`)

Assumes Courses 1 and 2. Running out of money is a real, named failure,
not a silent bug.

| # | Chapter | What you'll learn |
|---|---|---|
| 1 | [General Store](courses/09-general-store/en/01-general-store.md) | Custom exception classes, `try`/`catch`/`finally`, multiple `catch` blocks |

### Course 10 – Coin Purse (`courses/10-coin-purse/`)

Assumes Courses 1 and 2. Coins that can genuinely be compared, sorted, and
deduplicated, not just compared by which object they happen to be.

| # | Chapter | What you'll learn |
|---|---|---|
| 1 | [Coin Purse](courses/10-coin-purse/en/01-coin-purse.md) | Operator overloading (`==`, `<`, `>`), `Equals`/`GetHashCode`, `IComparable<T>` |

### Course 11 – Playlist Queries (`courses/11-playlist-queries/`)

Assumes Courses 1 and 2. Filtering, sorting, and summarizing a playlist
declaratively, instead of a manual loop for each one.

| # | Chapter | What you'll learn |
|---|---|---|
| 1 | [Playlist Queries](courses/11-playlist-queries/en/01-playlist-queries.md) | LINQ (`Where`, `Select`, `OrderBy`, `Sum`, `GroupBy`), lambda expressions |

### Course 12 – Generic Card Deck (`courses/12-generic-card-deck/`)

Assumes Courses 1 and 2. A `Deck<T>` you build yourself, not just consume.

| # | Chapter | What you'll learn |
|---|---|---|
| 1 | [Generic Card Deck](courses/12-generic-card-deck/en/01-generic-card-deck.md) | Generic classes and methods, type constraints (`where T : IComparable<T>`, `where T : new()`) |

More courses will be added over time; this section grows with them. See
[PROJECT-IDEAS.md](PROJECT-IDEAS.md) for what's coming next.

## Project ideas

[PROJECT-IDEAS.md](PROJECT-IDEAS.md) is this repository's own roadmap, not a
list of side-project suggestions: every entry there is a course this
repository has built, in the order it built them. It lives at the
repository root, not inside a single course, so the whole arc is visible
from one file.

## How to use this course

1. Read a chapter top to bottom.
2. Type the examples yourself instead of copy-pasting them — typing is what
   builds the muscle memory, copy-pasting only builds a scrollbar.
3. Every chapter has a **core section** (🟢, required to move on) and one or
   more **optional sections** (🟡 solid extra practice, 🔴 a genuine
   challenge). Skipping the optional parts is completely fine — come back to
   them later if you like.
4. Within a course, later chapters explicitly reuse code from earlier ones.
   Courses 1 and 2 are required, in order, for everything after them;
   Courses 3 and up build only on those two, independently of each other.
5. Links inside each chapter point to the relevant
   [Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/) page
   (the official C# documentation) right where a new concept shows up,
   instead of collecting them in a glossary at the end. If a term is
   unclear, the nearest link is your glossary.

## Repository layout

```
PROJECT-IDEAS.md / .de.md   this repository's own course roadmap
courses/
  01-basics/                 Course 1 — plain C#, no classes yet
    en/                        chapter text, English
    de/                        chapter text, German (Kapiteltexte, Deutsch)
    code/                      reference solution (Basics.csproj, Program.cs)
  02-cat-roster/             Course 2 — your first class
    en/                        chapter text, English
    de/                        chapter text, German
    code/                      reference solution (CatRoster.csproj, Cat.cs, Program.cs)
  03-homestead-inventory/    Course 3 — collections and real encapsulation
    en/                        chapter text, English
    de/                        chapter text, German
    code/                      reference solution (HomesteadInventory.csproj, Crop.cs, Animal.cs, Program.cs)
  04-machine-hunter/         Course 4 — inheritance
    en/                        chapter text, English
    de/                        chapter text, German
    code/                      reference solution (MachineHunter.csproj, Machine.cs, Watcher.cs, Thunderjaw.cs, Grazer.cs, Strider.cs, Program.cs)
  05-machine-showdown/       Course 5 — abstract classes and polymorphism
    en/                        chapter text, English
    de/                        chapter text, German
    code/                      reference solution (MachineShowdown.csproj, Machine.cs, Watcher.cs, Thunderjaw.cs, Strider.cs, Program.cs)
  06-crafting-bench/         Course 6 — interfaces
    en/                        chapter text, English
    de/                        chapter text, German
    code/                      reference solution (CraftingBench.csproj, ICollectible.cs, ISellable.cs, Herb.cs, RareGem.cs, Firewood.cs, TreasureMap.cs, Program.cs)
  07-reading-list/           Course 7 — persistence (JSON)
    en/                        chapter text, English
    de/                        chapter text, German
    code/                      reference solution (ReadingList.csproj, Book.cs, Program.cs)
  08-personal-library/       Course 8 — encapsulating a collection
    en/                        chapter text, English
    de/                        chapter text, German
    code/                      reference solution (PersonalLibrary.csproj, Book.cs, BadLibrary.cs, Library.cs, Program.cs)
  09-general-store/          Course 9 — exceptions
    en/                        chapter text, English
    de/                        chapter text, German
    code/                      reference solution (GeneralStore.csproj, InsufficientFundsException.cs, BankAccount.cs, Store.cs, Program.cs)
  10-coin-purse/             Course 10 — equality, comparison, and sorting
    en/                        chapter text, English
    de/                        chapter text, German
    code/                      reference solution (CoinPurse.csproj, NaiveCoin.cs, Coin.cs, Program.cs)
  11-playlist-queries/       Course 11 — LINQ
    en/                        chapter text, English
    de/                        chapter text, German
    code/                      reference solution (PlaylistQueries.csproj, Song.cs, Program.cs)
  12-generic-card-deck/      Course 12 — generics
    en/                        chapter text, English
    de/                        chapter text, German
    code/                      reference solution (GenericCardDeck.csproj, Card.cs, Deck.cs, Program.cs)
  13-.../                    future courses, same pattern
```

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md).
