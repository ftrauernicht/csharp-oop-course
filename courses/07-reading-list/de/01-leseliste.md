🇩🇪 Deutsch | 🇬🇧 [English](../en/01-reading-list.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1](../../01-basics/de/01-csharp-grundlagen.md) und [Kurs 2](../../02-cat-roster/de/01-katzenkartei.md)

# Kurs 7 – Leseliste

**Ziel:** eine `List<Book>`, die das Schließen und Neustarten des Programms
übersteht: gespeichert in einer echten Datei auf der Festplatte, in einem
Format, das auch jedes andere Programm (oder ein Texteditor) lesen kann.
Am Ende weißt du, wie du eine Liste deiner eigenen Objekte absichtlich in
Text verwandelst und wieder zurück, im Format, das fast jede API und
Konfigurationsdatei auf der Welt schon spricht: JSON.

Wie in Kurs 3, 4 und 6 steht die Kernübung unten in Schritten beschrieben,
die du selbst schreibst. Die fertige Version, inklusive der optionalen
Teile und der Herausforderung, liegt in [`code/`](../code/).

## 🟢 Kern — Das Problem: bisher war alles vorübergehend

Jeder Kurs vor diesem hat Objekte, Listen, ganze kleine Systeme gebaut.
Jedes einzelne verschwand in dem Moment, in dem das Programm aufhörte
zu laufen. Für eine Demo ist das in Ordnung, aber eine echte Leseliste muss
sich merken, was du gestern hinzugefügt hast. **Serialisierung** bedeutet,
ein Objekt in ein Format zu verwandeln, das gespeichert werden kann (eine
Datei, übers Netzwerk verschickt, was auch immer), und **Deserialisierung**
verwandelt es später wieder zurück in ein echtes Objekt. C# hat mehrere
Wege dafür; dieses Kapitel nutzt [`System.Text.Json`](https://learn.microsoft.com/de-de/dotnet/api/system.text.json),
fest in .NET eingebaut, weil dir JSON auch außerhalb dieses Kurses ständig
begegnen wird.

## 🟢 Kern — Eine einfache Klasse zum Speichern

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

Nichts Neues hier: dieselbe Form, die Kurs 2 für `Cat` benutzt hat.

## 🟢 Kern — Objekte in Text verwandeln

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

[`JsonSerializer.Serialize(...)`](https://learn.microsoft.com/de-de/dotnet/api/system.text.json.jsonserializer.serialize)
schaut sich ein Objekt an (oder hier eine ganze `List<Book>`) und erzeugt
einen String: Jede öffentliche Property wird zu einem `"Name": Wert`-Paar,
Arrays werden zu `[...]`, Objekte zu `{...}`: erkennbar, selbst wenn du nie
C# gesehen hast. Genau deshalb ist JSON überall.

## 🟢 Kern — Eine Datei schreiben und lesen

```csharp
File.WriteAllText("books.json", json);
```

```csharp
string savedJson = File.ReadAllText("books.json");
```

[`File.WriteAllText(path, contents)`](https://learn.microsoft.com/de-de/dotnet/api/system.io.file.writealltext)
legt die Datei an, falls sie noch nicht existiert, oder ersetzt sie
komplett, falls doch. [`File.ReadAllText(path)`](https://learn.microsoft.com/de-de/dotnet/api/system.io.file.readalltext)
liest das Ganze als einen String zurück. Beide leben in
[`System.IO`](https://learn.microsoft.com/de-de/dotnet/api/system.io),
automatisch verfügbar über die `ImplicitUsings` dieses Projekts.

## 🟢 Kern — Text zurück in Objekte verwandeln

```csharp
var loadedBooks = JsonSerializer.Deserialize<List<Book>>(savedJson) ?? new List<Book>();
```

[`JsonSerializer.Deserialize<T>(...)`](https://learn.microsoft.com/de-de/dotnet/api/system.text.json.jsonserializer.deserialize)
macht das Gegenteil: liest den JSON-Text und baut daraus echte `Book`-
Objekte, ordnet jede JSON-Property über den Namen einem
Konstruktor-Parameter zu (unabhängig von Groß-/Kleinschreibung: `"Title"`
im JSON findet `title` in `Book`s Konstruktor), und setzt danach jede
übrig gebliebene öffentliche Property (`IsFinished`) aus dem restlichen
JSON. Das sind echte, neu gebaute `Book`-Objekte, nicht die, mit denen du
angefangen hast: der Beweis, dass es wirklich durch Text und zurück ging,
nicht nur eine Referenz auf dieselben Objekte im Speicher.

`Deserialize<T>` kann theoretisch `null` zurückgeben (falls der JSON-Text
buchstäblich `"null"` war), sein Rückgabetyp ist also `List<Book>?`, nicht
`List<Book>`. [`??`](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/operators/null-coalescing-operator)
("Null-Coalescing") liest sich als "nimm die linke Seite, außer sie ist
`null`, dann nimm stattdessen die rechte": hier eine leere Liste statt
`null`, sodass der restliche Code nie vor der Benutzung auf `null` prüfen
muss.

## 🟢 Kernübung — In zwei wiederverwendbare Methoden verpacken

Benutz die vier Teile oben und schreib:

```csharp
void SaveBooks(List<Book> booksToSave, string path)
{
    // dein Code hier: serialisiere booksToSave, dann schreib es nach path
}

List<Book> LoadBooks(string path)
{
    // dein Code hier: lies die Datei bei path, deserialisiere sie, und
    // gib eine leere Liste statt null zurück, falls das fehlschlägt
}
```

Benutz sie dann für einen vollständigen Roundtrip:

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

Führ es aus und schau dann in deinen Projektordner. Da liegt jetzt eine
echte `books.json`-Datei neben deinem Code. Öffne sie in einem Texteditor;
es ist genau der String, den du vorhin ausgegeben gesehen hast. Falls du
feststeckst, hat [`code/Program.cs`](../code/Program.cs) funktionierende
Versionen beider Methoden.

## 🟡 Optional — Lesbares JSON

Die Datei, die du gerade erzeugt hast, ist eine einzige lange Zeile: gut
für ein Programm zu lesen, schwer für einen Menschen zu überfliegen.
[`JsonSerializerOptions`](https://learn.microsoft.com/de-de/dotnet/api/system.text.json.jsonserializeroptions)
lässt dich nach Formatierung fragen:

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

Übergib dasselbe `options` an `Serialize` innerhalb von `SaveBooks`, lösch
deine alte `books.json` und führ es erneut aus, um den Unterschied zu
sehen.

Zur Robustheit: Die Demo dieses Kapitels speichert immer erst, bevor sie
lädt, aber eine echte App lädt typischerweise *als Allererstes* beim
Start, bevor überhaupt schon etwas gespeichert wurde. [`File.Exists(path)`](https://learn.microsoft.com/de-de/dotnet/api/system.io.file.exists)
lässt `LoadBooks` eine leere Liste zurückgeben, statt abzustürzen, wenn es
gar keine Speicherdatei gibt:

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

## 🔴 Optional, echte Herausforderung — Lesen, ändern, speichern, neu laden

Persistenz zählt nur, wenn Änderungen wirklich haften bleiben. Nachdem du
deine Bücher zurückgeladen hast:

1. Markier das erste Buch als fertig gelesen: `loadedBooks[0].IsFinished = true;`
2. Speicher dieselbe Liste erneut, in dieselbe Datei, überschreib sie.
3. Lad sie noch einmal, in eine dritte Variable, und gib den `IsFinished`-
   Wert jedes Buchs aus.

Bestätige, dass die Änderung den vollständigen Roundtrip übersteht:
gespeichert, neu geladen, und immer noch da, nicht nur in der Variable
erhalten geblieben, die du zufällig geändert hast.
[`code/Program.cs`](../code/Program.cs) hat einen Weg, es zu schreiben.

## Was du gelernt hast

- Serialisierung und Deserialisierung: Objekte in ein speicherbares Format
  verwandeln und wieder zurück
- `JsonSerializer.Serialize(...)` und `JsonSerializer.Deserialize<T>(...)`
- `File.WriteAllText(...)` und `File.ReadAllText(...)`
- Warum das Ergebnis von `Deserialize<T>` nullable ist, und `??` als
  idiomatischer Weg, auf einen Standardwert statt `null` zurückzufallen
- `JsonSerializerOptions` für Formatierungsoptionen wie `WriteIndented`
- `File.Exists(...)`, um "es gibt noch keine Speicherdatei" zu behandeln,
  ohne abzustürzen
- Dass echte Persistenz bedeutet, dass eine Änderung einen vollständigen
  Speicher-und-Neuladen-Zyklus übersteht, nicht nur für die Variable gilt,
  die du zufällig geändert hast

## Weiter

Kurs 7 steht für sich allein und braucht nur Kurs 1 und 2. Damit ist die
aktuell geplante Roadmap dieses Repositories komplett. Was ein
zukünftiger Kurs als Nächstes bringen könnte, steht in
[PROJECT-IDEAS.de.md](../../../PROJECT-IDEAS.de.md).
