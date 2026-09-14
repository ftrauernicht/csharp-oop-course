🇬🇧 English | 🇩🇪 [Deutsch](PROJECT-IDEAS.de.md)

[← Back to repository overview](README.md) · Builds on: [Course 14 – Bank Account Tests](courses/14-bank-account-tests/en/01-bank-account-tests.md)

# This repository's course roadmap

Unlike a list of side-project suggestions for after you've finished a
course, every entry below is a course this repository has built (or, for a
future addition, plans to). Courses 1 and 2 cover plain, procedural C# and
your first class -- the shared foundation. Everything from here on is
about earning one object-oriented idea at a time, each through a small,
complete project, instead of front-loading a wall of vocabulary before any
of it means anything. Courses 3 and up don't depend on each other, but
every single one of them depends on Courses 1 and 2 -- see
[CONTRIBUTING.md](CONTRIBUTING.md) for why this repository's dependency
model differs slightly from a course that only needs "some C#".

Courses 1-7 are this repository's original arc. Courses 8-15 are a second,
more advanced tier -- each one sharply focused on a single topic real C#
codebases lean on constantly, ordered from smallest conceptual jump to
biggest.

| # | Course | New skills, on top of what you already have | Difficulty |
|---|---|---|---|
| 1 | ~~Basics~~ -- values, variables, operators, methods, conditionals, loops | ✅ Built -- see [Course 1 – Basics](courses/01-basics/en/01-csharp-basics.md) | ⭐ |
| 2 | ~~Your first class~~ -- a handful of `Cat` objects that introduce themselves | ✅ Built -- see [Course 2 – Cat Roster](courses/02-cat-roster/en/01-cat-roster.md) | ⭐ |
| 3 | ~~A homestead inventory~~ -- water, harvest, and count a small farm's crops and animals | ✅ Built -- see [Course 3 – Homestead Inventory](courses/03-homestead-inventory/en/01-homestead-inventory.md) | ⭐⭐ |
| 4 | ~~Machine hunter, part 1~~ -- a `Machine` base class and a few concrete machine types, each with their own attack | ✅ Built -- see [Course 4 – Machine Hunter](courses/04-machine-hunter/en/01-machine-hunter.md) | ⭐⭐⭐ |
| 5 | ~~Machine hunter, part 2~~ -- the same machines, fought one by one from a single list | ✅ Built -- see [Course 5 – Machine Showdown](courses/05-machine-showdown/en/01-machine-showdown.md) | ⭐⭐⭐ |
| 6 | ~~A crafting bench~~ -- items that can be collected, and some of those can also be sold | ✅ Built -- see [Course 6 – Crafting Bench](courses/06-crafting-bench/en/01-crafting-bench.md) | ⭐⭐⭐⭐ |
| 7 | ~~Save your progress~~ -- write a collection to a file and read it back | ✅ Built -- see [Course 7 – Reading List](courses/07-reading-list/en/01-reading-list.md) | ⭐⭐⭐ |
| 8 | ~~A personal library~~ -- why handing out a class's internal list breaks encapsulation, and the fix | ✅ Built -- see [Course 8 – Personal Library](courses/08-personal-library/en/01-personal-library.md) | ⭐⭐ |
| 9 | ~~A general store~~ -- a shop simulation where running out of money is a real, named failure, not a silent bug | ✅ Built -- see [Course 9 – General Store](courses/09-general-store/en/01-general-store.md) | ⭐⭐ |
| 10 | ~~A coin purse~~ -- coins and amounts that can genuinely be compared, sorted, and deduplicated | ✅ Built -- see [Course 10 – Coin Purse](courses/10-coin-purse/en/01-coin-purse.md) | ⭐⭐⭐ |
| 11 | ~~Playlist queries~~ -- filter, sort, and summarize a playlist without writing a single manual loop | ✅ Built -- see [Course 11 – Playlist Queries](courses/11-playlist-queries/en/01-playlist-queries.md) | ⭐⭐⭐ |
| 12 | ~~A generic card deck~~ -- a `Deck<T>` you build yourself, not just consume | ✅ Built -- see [Course 12 – Generic Card Deck](courses/12-generic-card-deck/en/01-generic-card-deck.md) | ⭐⭐⭐⭐ |
| 13 | ~~A thermostat that tells on itself~~ -- a device that announces its own state changes to whoever's listening, without knowing who that is | ✅ Built -- see [Course 13 – Thermostat Events](courses/13-thermostat-events/en/01-thermostat-events.md) | ⭐⭐⭐⭐ |
| 14 | ~~Proving the bank account works~~ -- automated tests for a class's validation and exception-throwing behavior, not just eyeballing the output | ✅ Built -- see [Course 14 – Bank Account Tests](courses/14-bank-account-tests/en/01-bank-account-tests.md) | ⭐⭐⭐⭐ |
| 15 | **Refactoring a pricing mess** -- turning a tangle of `if`/`else if` into swappable, testable strategies | The Strategy and Factory patterns, naming what Courses 4-13 already did unknowingly | ⭐⭐⭐⭐⭐ |

A few notes on how to read this list:

- Courses 1 and 2 are the one hard dependency everything else has. Beyond
  that, Course 3 and up can be done in any order -- pick whichever project
  sounds most fun.
- Courses 4 and 5 deliberately share one running example -- a small set of
  machine types -- because polymorphism is much easier to feel once an
  inheritance hierarchy already exists to be polymorphic *over*. They're
  still two independent, standalone projects: Course 5 rebuilds its own
  small version of the Course 4 hierarchy in a few lines before it gets to
  the actual point, so doing Course 5 without ever having done Course 4
  works just fine. Courses 9 and 14 share a domain the same way -- Course 14
  writes tests against its own small rebuild of Course 9's `BankAccount`.
- A few of these lean on hobbies of the person this course was first
  written for, on purpose, in specific spots rather than throughout: cats
  for the first, smallest class (Course 2); a cozy farming-sim flavor —
  think Stardew Valley, Animal Crossing: New Horizons, or Palia — for
  managing a growing collection of objects (Course 3); and a
  Horizon-Zero-Dawn-style world of mechanical creatures for inheritance and
  polymorphism (Courses 4-5), because "different machine types that share
  a shape but behave differently" is quite literally that game's own
  premise. Courses 1, 6, 7, and the whole 8-15 tier stay deliberately
  neutral.
- "Less code, more thinking" starts at Course 3 here -- two courses earlier
  than the equivalent point in the sibling JavaScript course. Once Courses
  1-2 have taught the shape of the language and of a class at all, the
  thing actually worth practicing is the OOP design decision itself, not
  retyping someone else's. Courses 8-15 keep holding back the core
  exercise the same way.
- Course 15 is deliberately last: it doesn't teach new syntax so much as
  give a name to a habit Courses 4 (swappable `Attack()` behavior), 6
  (composing capabilities), and 13 (decoupled communication) already built,
  on purpose, without calling it a "pattern" yet.

Courses 1-15 are this repository's currently built and planned roadmap.
Whatever comes after follows the same habits: build the core version
first, keep it simple, hold back the actual design decision as an exercise
rather than handing it over, and only reach for the optional, harder
variant of a feature once the simple one works. See
[CONTRIBUTING.md](CONTRIBUTING.md) for the full set of conventions a new
course needs to follow.
