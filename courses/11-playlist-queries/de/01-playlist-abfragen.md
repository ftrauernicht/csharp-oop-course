🇩🇪 Deutsch | 🇬🇧 [English](../en/01-playlist-queries.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1](../../01-basics/de/01-csharp-grundlagen.md) und [Kurs 2](../../02-cat-roster/de/01-katzenkartei.md)

# Kurs 11 – Playlist-Abfragen

**Ziel:** eine `List<Song>` filtern, sortieren und zusammenfassen, ohne
eine einzige manuelle `foreach` + `if`-Kombination zu schreiben. Am Ende
liest und schreibst du LINQ, eines der häufigsten Dinge, die dir in
echtem C#-Code begegnen, und weißt, wann es Code klarer macht als eine
Schleife und wann eine Schleife immer noch die ehrlichere Wahl ist.

Wie in Kurs 3, 4, 6, 8, 9 und 10 steht die Kernübung unten in Schritten
beschrieben, die du selbst schreibst. Die fertige Version, inklusive der
Herausforderung, liegt in [`code/`](../code/).

## 🟢 Kern — Der manuelle Weg, zur Erinnerung

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

Filtern, sammeln, wiederholen: genau diese Form hast du seit Kurs 3
geschrieben. Es funktioniert, aber die eigentliche *Absicht* ("gib mir die
Rock-Songs") steckt versteckt in vier Zeilen *Mechanik* (eine neue Liste,
eine Schleife, ein `if`, ein `Add`).

## 🟢 Kern — Where, und deine erste Lambda

```csharp
var rockSongs = songs.Where(s => s.Genre == "Rock").ToList();
```

Eine Zeile, dasselbe Ergebnis.
[`Where`](https://learn.microsoft.com/de-de/dotnet/csharp/linq/standard-query-operators/filtering-data)
ist eine Methode, die [`List<T>`](https://learn.microsoft.com/de-de/dotnet/api/system.collections.generic.list-1)
gratis dazubekommt, sobald [LINQ](https://learn.microsoft.com/de-de/dotnet/csharp/linq/)
im Spiel ist. Dank der `ImplicitUsings` dieses Projekts steht sie sogar
schon ohne zusätzliches `using` zur Verfügung. `s => s.Genre == "Rock"`
ist ein **Lambda-Ausdruck**: eine kleine, unbenannte Funktion, inline
geschrieben. Lies `s => ...` als "gegeben ein Song, hier `s` genannt,
...". `s` ist der Parameter (sein Typ, `Song`, wird aus dem Kontext
hergeleitet), und alles nach `=>` wird für jeden ausgewertet. `Where`
ruft diese Lambda einmal pro Element auf und behält nur die, für die
sie `true` zurückgibt. `.ToList()` am Ende verwandelt das Ergebnis
zurück in eine echte `List<Song>`, die du genau wie jede andere
benutzen kannst.

## 🟢 Kern — Select: umformen, nicht nur filtern

```csharp
var titles = songs.Select(s => s.Title).ToList();
```

[`Select`](https://learn.microsoft.com/de-de/dotnet/csharp/linq/standard-query-operators/projection-operations)
führt seine Lambda ebenfalls einmal pro Element aus, behält aber *das, was
die Lambda zurückgibt*, statt der ursprünglichen Elemente: hier eine
`List<string>` (nur Titel), keine `List<Song>` mehr. `Where` wählt eine
Teilmenge von dem aus, was du hast; `Select` verwandelt jedes Element in
etwas komplett anderes.

## 🟢 Kern — OrderBy

```csharp
var byDuration = songs.OrderBy(s => s.DurationSeconds).ToList();
```

[`OrderBy`](https://learn.microsoft.com/de-de/dotnet/csharp/linq/standard-query-operators/sorting-data)
sortiert nach dem, was die Lambda für jedes Element zurückgibt. Anders als
bei Kurs 10s `List<T>.Sort()` ist dafür kein `IComparable<T>` nötig. Das
ist der Tausch: `Sort()` braucht, dass der Typ selbst weiß, wie er seine
eigenen Instanzen vergleicht; `OrderBy` braucht nur, dass du genau dort, wo
du es benutzt, auf die Property zeigst, nach der sortiert werden soll.
[`OrderByDescending`](https://learn.microsoft.com/de-de/dotnet/api/system.linq.enumerable.orderbydescending)
ist dieselbe Idee, umgekehrt.

## 🟢 Kern — Aggregieren: Sum, Count und Verwandte

```csharp
int totalDuration = songs.Sum(s => s.DurationSeconds);
```

[`Sum`](https://learn.microsoft.com/de-de/dotnet/csharp/linq/standard-query-operators/aggregation-operations)
addiert das, was die Lambda für jedes Element zurückgibt. `Count()`,
`Average()`, `Max()` und `Min()` folgen alle derselben Form: keine
Schleife, keine Laufsumme-Variable, die du selbst verwalten musst.

## 🟢 Kernübung — Where und OrderByDescending verketten

Finde jeden Song, der länger als 200 Sekunden ist, längster zuerst:

```csharp
var longSongsDescending = songs
    // dein Code hier: .Where(...) für Songs über 200 Sekunden,
    // dann .OrderByDescending(...) nach Dauer, dann .ToList()
```

LINQ-Methoden verketten sich auf natürliche Weise. Jede gibt etwas
zurück, auf dem du die nächste Methode aufrufen kannst, also liest sich
`songs.Where(...).OrderByDescending(...).ToList()` fast wie ein Satz:
"nimm die Songs, behalt die langen, sortier sie nach Dauer absteigend, und
gib mir eine Liste." Falls du feststeckst, hat
[`code/Program.cs`](../code/Program.cs) eine funktionierende Version.

## 🔴 Optional, echte Herausforderung — GroupBy

Gruppier die ganze Playlist nach Genre und gib aus, wie viele Songs in
jedem stecken:

```csharp
var byGenre = songs.GroupBy(s => s.Genre);

foreach (var group in byGenre)
{
    Console.WriteLine($"{group.Key}: {group.Count()} song(s)");
}
```

[`GroupBy`](https://learn.microsoft.com/de-de/dotnet/csharp/linq/standard-query-operators/grouping-data)
ist die einzige LINQ-Methode hier, die kein einfaches manuelles
Schleifen-Äquivalent hat, das sich lohnt von Hand zu schreiben. Jede
`group`, die du zurückbekommst, verhält sich wie eine kleine Liste nur der
Songs dieses Genres (mit `.Key`, das dir sagt, welches Genre es ist); du
kannst also `foreach`, `Count()` oder jede andere LINQ-Methode auch darauf
anwenden. Probier es selbst, bevor du in
[`code/Program.cs`](../code/Program.cs) reinschaust.

## Was du gelernt hast

- Lambda-Ausdrücke (`s => ...`), eine inline geschriebene, unbenannte
  Funktion
- `Where` (filtern), `Select` (umformen), `OrderBy`/`OrderByDescending`
  (nach einer Property sortieren, kein `IComparable<T>` nötig), `Sum` (und
  seine Aggregations-Geschwister)
- LINQ-Methoden verketten, sodass sich der Code liest wie eine Beschreibung
  dessen, was du willst, statt wie eine Schleife, die beschreibt, wie man
  es bekommt
- `GroupBy`, um eine Sammlung in benannte Untergruppen aufzuteilen

## Weiter

Kurs 11 steht für sich allein und braucht nur Kurs 1 und 2. Für den Rest
der Roadmap dieses Repositories siehe
[PROJECT-IDEAS.de.md](../../../PROJECT-IDEAS.de.md).
