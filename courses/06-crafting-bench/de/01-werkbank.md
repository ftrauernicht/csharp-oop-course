🇩🇪 Deutsch | 🇬🇧 [English](../en/01-crafting-bench.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1](../../01-basics/de/01-csharp-grundlagen.md) und [Kurs 2](../../02-cat-roster/de/01-katzenkartei.md)

# Kurs 6 – Werkbank

**Ziel:** zwei Fähigkeiten modellieren, die manche Items haben und andere
nicht (sammelbar sein, verkaufbar sein), ohne jedes Item in eine starre
Klassenhierarchie zu zwingen. Am Ende weißt du, was ein Interface ist,
warum eine Klasse mehrere davon gleichzeitig implementieren kann (anders
als von mehr als einer Basisklasse zu erben, was C# überhaupt nicht
erlaubt), und wie dir das einen zweiten, flexibleren Weg gibt, die
Polymorphie-Auszahlung aus Kurs 5 wiederzuverwenden.

Wie in Kurs 3-5 steht die Kernübung unten in Schritten beschrieben, die du
selbst schreibst. Die fertige Version, inklusive der optionalen Teile und
der Herausforderung, liegt in [`code/`](../code/).

## 🟢 Kern — Zwei Fähigkeiten, die nicht in eine Hierarchie passen

Ein Kraut kann aufgesammelt, aber nicht verkauft werden. Ein seltener Edel-
stein kann aufgesammelt *und* verkauft werden. Eine Schatzkarte (die
Herausforderung unten) kann verkauft werden, ohne je auf die Art
"aufgesammelt" worden zu sein, wie ein Item in einer Tasche. Das mit einer
Basisklasse zu modellieren — sagen wir, eine `CollectibleBase` mit
`Collect()`, in der Hoffnung, dass verkaufbare Items auch irgendwie davon
erben — läuft direkt gegen eine Wand: **Eine C#-Klasse kann nur von einer
einzigen Basisklasse erben.** Es gibt keinen sauberen Weg, allein durch
Vererbung "eine Art von Collectible *und* eine Art von Sellable" zu sein.

## 🟢 Kern — Interfaces: ein Vertrag, keine Hierarchie

```csharp
public interface ICollectible
{
    string Name { get; }
    void Collect();
}
```

Ein [`interface`](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/keywords/interface)
listet Member auf, die ein Typ haben muss — eine Property, eine Methode,
was auch immer drinsteht — **komplett ohne Implementierung**, nicht mal die
generische `virtual`-Rückfalllösung aus Kurs 4 ist erlaubt. Es ist rein ein
Versprechen: "alles, was `ICollectible` implementiert, hat garantiert einen
`Name` und ein `Collect()`." Das `I`-Präfix (`ICollectible`, `ISellable`)
ist eine Namenskonvention, die dir in echtem C#-Code überall begegnet —
von Anfang an übernehmenswert.

Eine Klasse **implementiert** ein Interface genauso, wie sie von einer
Basisklasse erbt:

```csharp
public class Herb : ICollectible
{
    public string Name { get; }

    public Herb(string name)
    {
        Name = name;
    }

    public void Collect()
    {
        Console.WriteLine($"Picked up {Name}.");
    }
}
```

## 🟢 Kern — Mehr als ein Interface implementieren

```csharp
public interface ISellable
{
    int Price { get; }
    void Sell();
}
```

```csharp
public class RareGem : ICollectible, ISellable
{
    public string Name { get; }
    public int Price { get; }

    public RareGem(string name, int price)
    {
        Name = name;
        Price = price;
    }

    public void Collect()
    {
        Console.WriteLine($"Picked up {Name}, sparkling in the light.");
    }

    public void Sell()
    {
        Console.WriteLine($"Sold {Name} for {Price} coins.");
    }
}
```

`: ICollectible, ISellable` — eine durch Komma getrennte Liste. Genau das
konnte Vererbung nicht: `RareGem` erfüllt jetzt zwei völlig unabhängige
Versprechen gleichzeitig, und du könntest genauso ein drittes oder viertes
Interface hinzufügen. Das ist **Komposition statt Vererbung** in ihrer
einfachsten Form: statt jeden Typ in einen "ist-eine"-Baum zu zwingen,
setzt du einen Typ aus so vielen kleinen, fokussierten Fähigkeiten
zusammen, wie er tatsächlich braucht. Mehr:
[Microsoft Learn – Interfaces](https://learn.microsoft.com/de-de/dotnet/csharp/fundamentals/types/interfaces).

## 🟢 Kern — Interfaces sind auch polymorph

```csharp
var inventory = new List<ICollectible>
{
    new Herb("Wild Mint"),
    new RareGem("Sunstone", 50),
};

foreach (var item in inventory)
{
    item.Collect();
}
```

Das sollte bekannt vorkommen — es ist genau dieselbe Auszahlung wie Kurs 5s
`List<Machine>`, nur über ein Interface statt eine abstrakte Klasse.
`List<ICollectible>` kann *jeden* Typ enthalten, der `ICollectible`
implementiert, egal was dieser Typ sonst noch ist oder tut, und
`item.Collect()` läuft immer noch auf die jeweils eigene Version hinaus.
Abstrakte Klassen und Interfaces sind zwei verschiedene Werkzeuge für
dieselbe zugrunde liegende Idee: gegen einen geteilten Vertrag
programmieren, und die Details der Laufzeit überlassen.

## 🟢 Kernübung — Schreib Firewood selbst

Folg der Form von `RareGem` und schreib eine `Firewood`-Klasse, die
**beide** Interfaces implementiert, `ICollectible` und `ISellable`, mit
Name `"Firewood"` und Preis `5`:

```csharp
public class Firewood : ICollectible, ISellable
{
    // dein Code hier
}
```

Füg `new Firewood("Firewood", 5)` zur `inventory`-Liste oben hinzu und
bestätige, dass es korrekt erscheint, wenn du alles mit `Collect()`
aufsammelst. Falls du feststeckst, hat
[`code/Firewood.cs`](../code/Firewood.cs) eine funktionierende Version.

## 🟡 Optional — Herausfinden, was verkäuflich ist, in einer gemischten Liste

`inventory` ist eine `List<ICollectible>` — soweit es den Typ der Liste
betrifft, ist nichts darin zwangsläufig verkäuflich. Um pro Item
herauszufinden, ob es *zusätzlich* `ISellable` implementiert, nutz `is`
zusammen mit einem Variablennamen, nicht nur mit einem Typ:

```csharp
foreach (var item in inventory)
{
    if (item is ISellable sellable)
    {
        sellable.Sell();
    }
}
```

`item is ISellable sellable` prüft den tatsächlichen Typ des Objekts *und*
gibt dir, falls er passt, `sellable` — bereits als `ISellable` typisiert,
mit `.Price` und `.Sell()` verfügbar — in einem Schritt. Das ruft `Sell()`
nur auf den Items auf, die es tatsächlich unterstützen (`RareGem` und,
sobald hinzugefügt, `Firewood`), überspringt `Herb` komplett — alles aus
einer Liste, die jede Art von Item gleichzeitig enthält.

## 🔴 Optional, echte Herausforderung — Ein Typ, der nur Sellable ist

Beweis, dass die beiden Interfaces unabhängig sind, nicht heimlich
eine Hierarchie: Schreib eine `TreasureMap`-Klasse, die **nur**
`ISellable` implementiert (einen `Price`, ein `Sell()`) — kein
`ICollectible`, kein `Name`, kein `Collect()`.

```csharp
public class TreasureMap : ISellable
{
    // dein Code hier
}
```

Bestätige zwei Dinge: Eine `TreasureMap` funktioniert einwandfrei in einer
eigenen `List<ISellable>`, und `inventory.Add(new TreasureMap(30))` —
der Versuch, sie in die `List<ICollectible>` von oben zu stecken — weigert
sich zu kompilieren: `error CS1503: Argument 1: cannot convert from
'TreasureMap' to 'ICollectible'`, weil eine `TreasureMap` das
nicht ist. Vergleich mit [`code/TreasureMap.cs`](../code/TreasureMap.cs).

## Was du gelernt hast

- Warum manche Kombinationen von Verhalten nicht in einen einzigen
  Vererbungsbaum passen
- `interface`: ein Vertrag ganz ohne Implementierung, per Konvention mit
  `I`-Präfix
- Eine Klasse kann beliebig viele Interfaces implementieren (`:
  ICollectible, ISellable`), anders als von mehr als einer Basisklasse zu
  erben
- Komposition statt Vererbung: einen Typ aus unabhängigen Fähigkeiten
  zusammensetzen, statt ihn in eine Hierarchie zu zwingen
- Interfaces geben dir dieselbe polymorphe `List<T>`-plus-`foreach`-
  Auszahlung wie eine abstrakte Basisklasse
- `is Typ Variablenname`-Pattern-Matching, um innerhalb einer Schleife
  über einen allgemeineren Typ nach einer spezifischeren Fähigkeit zu
  suchen und sie zu benutzen

## Weiter

Kurs 6 steht für sich allein und braucht nur Kurs 1 und 2. Für den Rest der
Roadmap dieses Repositories siehe
[PROJECT-IDEAS.de.md](../../../PROJECT-IDEAS.de.md).
