🇬🇧 English | 🇩🇪 [Deutsch](CONTRIBUTING.de.md)

# Contributing to C# OOP Course

This is a personal, solo-maintained project.

- Issues and pull requests are welcome.
- Keep pull requests small and focused -- easier to review, easier to revert.
- No contributor licence agreement is required.
- Response times vary; this isn't maintained on a fixed schedule.

## Adding a new course

This repository is meant to grow. If you're adding a new course -- by
hand, or with AI assistance -- the existing courses follow conventions
that aren't obvious from any single file; they only show up once you
compare several courses side by side. They're written down here so a new
contributor, or a fresh AI session with no memory of earlier work, can
follow them without reverse-engineering them first.

**Structure**
- Each course is a new folder `courses/NN-name/`, numbered in the order
  it was written. Update the "Courses" table in `README.md`/`README.de.md`,
  and the roadmap in `PROJECT-IDEAS.md`/`PROJECT-IDEAS.de.md` (mark the
  entry as built), whenever you add one.
- **Courses 1 and 2 are the shared, required foundation.** Unlike a course
  about "some C# skill", object-oriented ideas build directly on each
  other, so every course from Course 3 onward assumes **both** Courses 1
  and 2 -- never another sibling course beyond those two. If a course needs
  a concept Courses 1-2 don't teach, explain it inline, briefly, with a
  note like "(if you've done Course 4, skip ahead)" for readers who already
  know it -- don't assume it's already known.
- Chapters live in parallel `en/` and `de/` folders with identical
  content and structure, not one bilingual file. Code and code comments
  are always English. Cross-language links point to the matching
  [Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/) locale
  (`/en-us/` vs `/de-de/`).
- Every chapter uses the same three-level system, introduced once in
  Course 1 Chapter 0 and not re-explained elsewhere: 🟢 Core (required),
  🟡 Optional (more practice), 🔴 Optional (a genuine challenge).
- A course gets a `code/` folder: a finished, buildable console project
  (a `.csproj` and its `.cs` files) that serves as the answer key --
  runnable as-is, and what a learner compares their own attempt against.
- **Screenshots are the exception here, not the rule.** Almost every course
  is a console app, so there's usually nothing to screenshot -- show a
  fenced "example output" code block in the chapter text instead. Only add
  an `assets/` folder for a course that genuinely has a UI.

**Before publishing a new chapter, check for these recurring mistakes**
(the same checklist the sibling JavaScript course uses, plus two C#-specific
ones):
- Cross-references to "Course N" or "Chapter N" go stale silently when
  content is renumbered or split -- grep for both the singular *and*
  plural form ("Course 3", "Courses 3 and 4"), in both languages.
- A promise to explain something "later" must actually be delivered, or
  reworded to be honest about not covering it.
- A course claiming to need only Courses 1 and 2 must be checked against
  their *actual* contents, not the intended ones -- re-verify after
  Course 1 or 2 themselves change.
- **Every `code/` project must actually `dotnet build` clean, with zero
  warnings** -- a warning left in a reference solution reads as "this is
  fine, ignore it" to a beginner who doesn't yet know which warnings matter.
- **Run every snippet before publishing**, not just the finished
  `code/` project -- copy each inline example into a scratch project and
  build it, especially anything in an optional section. A snippet that
  merely looks plausible next to working code is easy to let slide, and C#
  will not compile a typo the way a dynamically-typed language might still
  run it.
- Restate "where does this code go" at every point that could be
  ambiguous, not just once at the top of a chapter.

**How much code to show**: Courses 1 and 2 hand you complete, working code
to type in -- the point there is learning the shape of the language and of
a class at all. Starting with Course 3, chapters give you the new
building block (an OOP concept) fully explained, but leave the core
logic of the exercise -- the part that's actually the point of the
chapter -- described in steps for you to assemble yourself, with the
finished version only in `code/` as an answer key. This starts two courses
earlier than the sibling JavaScript course's equivalent rule, on purpose:
the actual subject here is OOP design decisions, which is exactly the part
that needs to be *thought through*, not copied.

**Commit messages** follow [Conventional Commits](https://www.conventionalcommits.org/)
(`feat: add Course 3 - ...`, `fix: ...`, `docs: ...`). A new course is
typically one `feat:` commit covering the chapter text, code, and the
README/PROJECT-IDEAS updates together.
