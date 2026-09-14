🇬🇧 English | 🇩🇪 [Deutsch](PROJECT-IDEAS.de.md)

[← Back to repository overview](README.md) · Builds on: [Course 2 – Cat Roster](courses/02-cat-roster/en/01-cat-roster.md)

# This repository's course roadmap

Unlike a list of side-project suggestions for after you've finished a
course, every entry below is a course this repository plans to have. Courses
1 and 2 cover plain, procedural C# and your first class -- the shared
foundation. Everything from here on is about earning one object-oriented
idea at a time, each through a small, complete project, instead of
front-loading a wall of vocabulary before any of it means anything. Courses
3 and up don't depend on each other, but every single one of them depends
on Courses 1 and 2 -- see [CONTRIBUTING.md](CONTRIBUTING.md) for why this
repository's dependency model differs slightly from a course that only
needs "some C#".

| # | Course | New skills, on top of what you already have | Difficulty |
|---|---|---|---|
| 1 | ~~Basics~~ -- values, variables, operators, methods, conditionals, loops | ✅ Built -- see [Course 1 – Basics](courses/01-basics/en/01-csharp-basics.md) | ⭐ |
| 2 | ~~Your first class~~ -- a handful of `Cat` objects that introduce themselves | ✅ Built -- see [Course 2 – Cat Roster](courses/02-cat-roster/en/01-cat-roster.md) | ⭐ |
| 3 | **A homestead inventory** -- water, harvest, and count a small farm's crops and animals | `List<T>`, `foreach`, private fields with validated public properties -- encapsulation, this time named and used on purpose | ⭐⭐ |
| 4 | **Machine hunter, part 1** -- a `Machine` base class and a few concrete machine types, each with their own attack | Inheritance, `virtual`/`override`, `base(...)`, `protected` | ⭐⭐⭐ |
| 5 | **Machine hunter, part 2** -- the same machines, fought one by one from a single list | Abstract classes, polymorphism -- the payoff for what Course 4 quietly set up | ⭐⭐⭐ |
| 6 | **A crafting bench** -- items that can be collected, and some of those can also be sold | Interfaces, composition vs. inheritance, implementing more than one interface on a class | ⭐⭐⭐⭐ |
| 7 | **Save your progress** -- write a collection to a file and read it back | `System.Text.Json`, serializing and deserializing objects | ⭐⭐⭐ |

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
  works just fine.
- A few of these lean on hobbies of the person this course was first
  written for, on purpose, in specific spots rather than throughout: cats
  for the first, smallest class (Course 2); a cozy farming-sim flavor —
  think Stardew Valley, Animal Crossing: New Horizons, or Palia — for
  managing a growing collection of objects (Course 3); and a
  Horizon-Zero-Dawn-style world of mechanical creatures for inheritance and
  polymorphism (Courses 4-5), because "different machine types that share
  a shape but behave differently" is quite literally that game's own
  premise. Courses 1, 6, and 7 stay deliberately neutral.
- "Less code, more thinking" starts at Course 3 here -- two courses earlier
  than the equivalent point in the sibling JavaScript course. Once Courses
  1-2 have taught the shape of the language and of a class at all, the
  thing actually worth practicing is the OOP design decision itself, not
  retyping someone else's.

Whichever gets built next, the same habits from Courses 1-2 keep applying:
build the core version first, keep it simple, and only reach for the
optional, harder variant of a feature once the simple one works.
