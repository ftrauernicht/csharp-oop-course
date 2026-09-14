🇬🇧 English | 🇩🇪 [Deutsch](../de/00-einfuehrung.md)

[← Back to course overview](../../../README.md) · Next: [Chapter 1 – C# Basics](01-csharp-basics.md) →

# Chapter 0 – Introduction & Tools

## What is C#, and why does it need an IDE?

C# (pronounced "C sharp") is a general-purpose programming language built by
Microsoft. It runs on **.NET**, a free, cross-platform runtime that works on
Windows, macOS, and Linux — you'll find C# behind everything from desktop
apps and web backends to games built with the Unity engine. You can read
more on [Microsoft Learn's "Introduction to C#"](https://learn.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/).

Unlike JavaScript in a browser, C# doesn't run directly — it has to be
**compiled** first: translated from the code you write into something your
computer can execute, with the compiler checking your code for mistakes
along the way. That compile step is why this course starts with installing
an **IDE** (Integrated Development Environment) instead of just opening a
browser tab: one tool that lets you write, compile, and run your code, and
points at your mistakes before you even hit run.

## Installing Visual Studio Community

We'll use **Visual Studio Community** — not to be confused with the
similarly-named [Visual Studio Code](https://code.visualstudio.com/), a
different, much lighter editor. Visual Studio Community is the full IDE,
free for individuals, students, and open-source work, and it's what this
course's screenshots and menus assume.

1. Download it from [visualstudio.microsoft.com](https://visualstudio.microsoft.com/vs/community/).
2. Run the installer. When it asks which **workload** to install, tick
   **".NET desktop development"** — this is the one that gives you C#
   console app templates, the debugger, and everything else this course
   needs. You can leave the rest unchecked for now; you can always add more
   later from the same installer.
3. The download and install takes a while (it's a few gigabytes) — a good
   moment to grab a coffee.

Official step-by-step instructions with current screenshots (the exact
dialog layout changes between Visual Studio versions, so it's worth using
Microsoft's own up-to-date guide over a static screenshot here):
[Install Visual Studio](https://learn.microsoft.com/en-us/visualstudio/install/install-visual-studio).

## Creating your first project

1. Open Visual Studio and choose **"Create a new project"**.
2. In the search box, type **"Console App"**. You'll likely see two similar
   entries — pick the plain **"Console App"** one with a **C#** tag that
   targets the current .NET (not **"Console App (.NET Framework)"**, an
   older, Windows-only technology this course doesn't use). If you're
   unsure which is which, hover each entry — the description names the
   framework.
3. Give it a name (e.g. `MyFirstProject`) and a location on disk, then click
   **Create**.

Visual Studio just created a **Solution** (a `.sln` file — a container that
can hold one or more projects) with one **Project** inside it (a `.csproj`
file — the thing that actually gets compiled). For this whole course,
solution and project are basically a 1-to-1 pair; you'll only notice the
difference once a solution grows to hold more than one project.

Open `Program.cs` — the one file the template created. It already contains
one line:

```csharp
Console.WriteLine("Hello, World!");
```

That's a complete, runnable C# program. No `class`, no `Main` method wrapping
it — modern C# lets you skip that boilerplate for now with a feature called
**top-level statements**: the file's contents just run top to bottom, like a
script. (If you ever see an *older* C# example with
`class Program { static void Main(string[] args) { ... } }` around
everything, that's the same thing, spelled out in full — you'll meet that
shape for real once classes are the point, in Course 2.)

## Running your program

Two equivalent ways, worth knowing both:

- **Inside Visual Studio:** press the green ▶ **Start** button, or hit
  <kbd>F5</kbd>. A console window pops up and runs your program.
- **From a terminal:** open one, `cd` into your project's folder (the one
  containing the `.csproj` file), and run:
  ```
  dotnet run
  ```
  This works identically outside Visual Studio, which is worth knowing —
  it's how CI servers, other editors, and this course's own automated
  checks run C# code too.

Either way, you should see `Hello, World!` printed. That's your first
running C# program.

A quick sanity check for your terminal setup, useful any time something
seems off later: `dotnet --version` prints the .NET SDK version you have
installed.

## Compiler errors are a feature, not a monster

Try breaking your program on purpose — delete the closing `"` from
`"Hello, World!"` and try to run it. Visual Studio underlines the problem in
red *before* you even hit run, and refuses to build until you fix it.

That's the compile step from earlier, working for you: C# catches an entire
category of mistakes — typos, wrong types, missing pieces — before your
program ever runs, rather than failing partway through like some other
languages do. Reading the error message (it names the file, the line, and
what it expected) is a skill in itself, and one you'll get fast at. Put the
quote mark back before moving on.

## How this course is structured

Every chapter follows the same shape:

- 🟢 **Core** — the required part. Finish this before moving to the next
  chapter.
- 🟡 **Optional, more practice** — reinforces the same chapter's ideas with a
  bit more depth. Good to do, not required.
- 🔴 **Optional, genuine challenge** — a harder stretch goal, sometimes
  introducing a concept ahead of schedule. It's fine if this doesn't click
  the first time; you can always come back after a later chapter.

Chapters build on each other on purpose, and so do courses. Courses 1 and 2
together are the shared foundation every later course assumes — unlike a
course that only needs "just C#", learning OOP itself is cumulative, so this
repository keeps the two courses that build that foundation required, with
everything after them free to be done in whatever order you like.

One more thing about language: this course is written in German and English
as separate, parallel files — pick whichever you're more comfortable
reading. The **code itself and its comments are always in English**, which
mirrors how real software teams work regardless of their spoken language.
Links to further reading (mostly [Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/),
the official C# documentation) sit inline in the text, right where a new
term first appears, instead of being collected into a glossary at the end.

## Ready?

Continue to [Chapter 1 – C# Basics](01-csharp-basics.md).
