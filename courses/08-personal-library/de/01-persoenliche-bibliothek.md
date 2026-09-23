🇩🇪 Deutsch | 🇬🇧 [English](../en/01-personal-library.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1](../../01-basics/de/01-csharp-grundlagen.md) und [Kurs 2](../../02-cat-roster/de/01-katzenkartei.md)

# Kurs 8 – Persönliche Bibliothek

**Ziel:** eine *Sammlung* kapseln, nicht nur einen einzelnen Wert wie Kurs 3s
`WaterLevel`. Am Ende weißt du, warum die interne Liste einer Klasse direkt
herauszugeben ein häufiger, leicht zu übersehender Weg ist, Kapselung
komplett zu brechen, und wie die Standardlösung dafür aussieht.

Wie in Kurs 3-7 steht die Kernübung unten in Schritten beschrieben, die du
selbst schreibst. Die fertige Version, inklusive der Herausforderung, liegt
in [`code/`](../code/).

## 🟢 Kern — Das Problem: die Liste selbst herausgeben

```csharp
public class BadLibrary
{
    public List<Book> Books { get; } = new List<Book>();
}
```

Das sieht harmlos aus. `Books` ist eine nur-lesbare Property, also kann
niemand sie durch eine andere Liste *ersetzen*. Aber `List<T>` selbst ist
veränderbar, und eine Referenz darauf herauszugeben gibt volle Kontrolle
über ihren Inhalt:

```csharp
var badLibrary = new BadLibrary();
badLibrary.Books.Add(new Book("Dune", "Frank Herbert"));
badLibrary.Books.Clear(); // nichts hindert das -- die ganze Bibliothek, weg
```

`Clear()` ist kein Tippfehler oder Sonderfall, sondern eine ganz normale
`List<T>`-Methode, verfügbar für *jeden*, der eine Referenz auf diese Liste
hält, ganz ohne Beteiligung von `BadLibrary` selbst. Eine nur-lesbare
Property schützt die Referenz; sie schützt überhaupt nicht, worauf diese
Referenz zeigt.

## 🟢 Kern — IReadOnlyList\<T\>, und wie viel es wirklich schützt

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

Zwei Änderungen beheben das Problem:

- Die eigentliche Liste ist jetzt ein `private readonly`-Feld, `_books`:
  nichts außerhalb von `Library` kann sie überhaupt direkt erreichen.
- `Books` gibt [`IReadOnlyList<Book>`](https://learn.microsoft.com/de-de/dotnet/api/system.collections.generic.ireadonlylist-1)
  statt `List<Book>` zurück, ein Interface ganz ohne `Add`, `Remove` oder
  `Clear`. `library.Books.Add(...)` verhält sich nicht nur falsch, es
  **kompiliert gar nicht erst**: `error CS1061: 'IReadOnlyList<Book>' does
  not contain a definition for 'Add'`.

Eine Feinheit, die man kennen sollte, weil man sie leicht fast richtig
macht: `return _books;` (ein einfaches Hochcasten zu `IReadOnlyList<Book>`)
blockiert das nur zur *Kompilierzeit*. Das Objekt dahinter ist immer noch
exakt dieselbe `List<Book>`. Entschlossener Code könnte sie zurückcasten
(`(List<Book>)library.Books`) und trotzdem verändern.
[`_books.AsReadOnly()`](https://learn.microsoft.com/de-de/dotnet/api/system.collections.generic.list-1.asreadonly)
ist stärker: Es umhüllt `_books` in einem echten, eigenständigen
`ReadOnlyCollection<T>`-Objekt. *Das* zurück zu `List<Book>` zu casten
verhält sich auch nicht nur falsch. Es wirft zur Laufzeit eine
`InvalidCastException`, weil es darunter wirklich keine `List<Book>` ist.
Bevorzug `AsReadOnly()` genau deswegen.

## 🟢 Kernübung — Schreib RemoveBook selbst

```csharp
public bool RemoveBook(string title)
{
    // dein Code hier: finde das Buch mit diesem Titel in _books, entferne
    // es, gib eine Bestätigung aus und liefere true zurück -- oder, falls
    // kein Buch mit diesem Titel existiert, gib das aus und liefere false
}
```

Alles, was du brauchst, ist schon bekannt: eine indexbasierte `for`-Schleife
über `_books`, `_books[i].Title` mit `title` vergleichen, und
[`List<T>.RemoveAt(index)`](https://learn.microsoft.com/de-de/dotnet/api/system.collections.generic.list-1.removeat),
sobald du es gefunden hast. Test es gegen eine `Library` mit ein paar
hinzugefügten Büchern. Falls du feststeckst, hat
[`code/Library.cs`](../code/Library.cs) eine funktionierende Version.

## Was du gelernt hast

- Warum eine nur-lesbare Property ein *veränderbares* Objekt, auf das sie
  eine Referenz zurückgibt, nicht schützt, sondern nur die Referenz selbst
- `IReadOnlyList<T>`, und der Unterschied zwischen einem einfachen
  Hochcasten (blockiert Missbrauch nur zur Kompilierzeit) und
  `.AsReadOnly()` (blockiert ihn auch zur Laufzeit, indem es die Liste in
  einem echten, eigenständigen Objekt umhüllt)
- Kurs 3s Kapselungs-Lektion auf eine Sammlung statt einen einzelnen Wert
  anwenden

## Weiter

Kurs 8 steht für sich allein und braucht nur Kurs 1 und 2. Für den Rest der
Roadmap dieses Repositories siehe
[PROJECT-IDEAS.de.md](../../../PROJECT-IDEAS.de.md).
