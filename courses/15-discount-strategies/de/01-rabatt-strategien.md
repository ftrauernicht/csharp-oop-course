🇩🇪 Deutsch | 🇬🇧 [English](../en/01-discount-strategies.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1](../../01-basics/de/01-csharp-grundlagen.md) und [Kurs 2](../../02-cat-roster/de/01-katzenkartei.md)

# Kurs 15 – Rabatt-Strategien

**Ziel:** ein wachsendes Gestrüpp aus `if`/`else if` in kleine,
austauschbare Objekte verwandeln — und dabei Gewohnheiten einen Namen
geben, die die Kurse dieses Repositories schon seit Kurs 4 aufgebaut
haben, ohne sie je "Pattern" zu nennen. Das ist der letzte Kurs der
aktuell geplanten Roadmap dieses Repositories, und er handelt bewusst
weniger von neuer Syntax als davon, zu erkennen, was du schon kannst.

Wie in Kurs 3, 4, 6, 8, 9, 10, 11, 12, 13 und 14 steht die Kernübung unten
in Schritten beschrieben, die du selbst schreibst. Die fertige Version,
inklusive der Herausforderung, liegt in [`code/`](../code/).

## 🟢 Kern — Das Gestrüpp

```csharp
public class MessyPriceCalculator
{
    public int CalculatePrice(int basePrice, string customerType)
    {
        if (customerType == "regular")
        {
            return basePrice;
        }
        else if (customerType == "member")
        {
            return basePrice - (basePrice * 10 / 100);
        }
        else if (customerType == "vip")
        {
            return basePrice - 50;
        }
        else
        {
            throw new ArgumentException($"Unknown customer type: {customerType}");
        }
    }
}
```

Das funktioniert heute, aber jeder neue Kundentyp bedeutet, diese Methode
zu öffnen und einen weiteren Zweig hinzuzufügen — und zu testen, "funktioniert
die Member-Rabattrechnung" (im Kurs-14-Stil), bedeutet, durch eine Methode
zu gehen, die zufällig auch VIP- und Normalpreis enthält, egal ob der Test
sich dafür interessiert oder nicht.

## 🟢 Kern — Das Strategy-Pattern

```csharp
public interface IDiscountStrategy
{
    int Apply(int basePrice);
}

public class NoDiscount : IDiscountStrategy
{
    public int Apply(int basePrice)
    {
        return basePrice;
    }
}

public class PercentageDiscount : IDiscountStrategy
{
    private readonly int _percent;

    public PercentageDiscount(int percent)
    {
        _percent = percent;
    }

    public int Apply(int basePrice)
    {
        return basePrice - (basePrice * _percent / 100);
    }
}
```

```csharp
public class PriceCalculator
{
    public int CalculatePrice(int basePrice, IDiscountStrategy discount)
    {
        return discount.Apply(basePrice);
    }
}
```

`PriceCalculator` hat jetzt kein einziges `if` mehr. Jede Rabattregel ist
ihre eigene kleine Klasse, komplett für sich allein testbar — genau Kurs
14s `Assert.Equal`, gezielt auf `new PercentageDiscount(10).Apply(100)`,
ganz ohne `PriceCalculator` beteiligt. Das ist das **Strategy-Pattern**:
ein Interface für "eine Art, diese eine Sache zu tun", und getrennte
Klassen für jede tatsächliche Art, sie zu tun, ausgewählt von dem, der es
benutzt, statt in eine große Methode gebacken zu sein. Das machst du schon
seit Kurs 4 — `Watcher` und `Thunderjaw` sind Strategien für "wie eine
Maschine angreift", du hattest nur noch keinen Namen dafür. Mehr:
[Microsoft Learn – Interfaces](https://learn.microsoft.com/de-de/dotnet/csharp/fundamentals/types/interfaces)
(dasselbe Feature, Kurs 6s Kapitel geht tiefer auf die Mechanik ein).

## 🟢 Kernübung — Schreib FixedAmountDiscount selbst

Folg der Form von `PercentageDiscount` und schreib eine Strategie, die
immer einen festen Betrag abzieht:

```csharp
public class FixedAmountDiscount : IDiscountStrategy
{
    // dein Code hier: ein Konstruktor, der den abzuziehenden Betrag
    // nimmt, und ein Apply(basePrice), das ihn abzieht
}
```

Test es: `new PriceCalculator().CalculatePrice(100, new FixedAmountDiscount(50))`
sollte `50` ergeben. Falls du feststeckst, hat
[`code/FixedAmountDiscount.cs`](../code/FixedAmountDiscount.cs) eine
funktionierende Version.

## 🟡 Optional — Das Factory-Pattern

Irgendetwas muss immer noch einen Kundentyp-String in das richtige
Strategie-Objekt verwandeln. Diese Entscheidung verdient genau einen Ort,
an dem sie lebt:

```csharp
public static class DiscountStrategyFactory
{
    public static IDiscountStrategy Create(string customerType)
    {
        if (customerType == "regular")
        {
            return new NoDiscount();
        }
        else if (customerType == "member")
        {
            return new PercentageDiscount(10);
        }
        else if (customerType == "vip")
        {
            return new FixedAmountDiscount(50);
        }
        else
        {
            throw new ArgumentException($"Unknown customer type: {customerType}");
        }
    }
}
```

Die `if`/`else if`-Kette ist nicht wirklich verschwunden — sie ist
umgezogen. Das ist ehrlich, und diese Ehrlichkeit lohnt sich: Das
**Factory-Pattern** eliminiert keine Bedingungen, es *isoliert* eine, damit
sie der einzige Ort im ganzen Programm ist, der wissen muss, wie man einen
String in ein Objekt verwandelt; die eigentliche Rabattrechnung verzweigt
jetzt überhaupt nicht mehr nach `customerType`. `static class` bedeutet,
dass `DiscountStrategyFactory` selbst nie mit `new` instanziiert wird —
seine eine Methode wird direkt auf dem Klassennamen aufgerufen
(`DiscountStrategyFactory.Create(...)`), passend für etwas, das keinen
eigenen Zustand hält.

## 🔴 Optional, echte Herausforderung — Erweitern, ohne Bestehendes anzufassen

Füg einen vierten Rabatt hinzu — was auch immer dir gefällt (ein
Saisonrabatt, ein Mengenrabatt) — als eigene neue `IDiscountStrategy`-
Klasse, und verdrahte sie in `DiscountStrategyFactory`. Bestätige, dass du
dafür nie `PriceCalculator.cs` öffnen musstest, noch irgendeine der
*bestehenden* Strategie-Klassen — nur neue Dateien, plus ein neuer Zweig
in der Factory. Das ist die eigentliche Auszahlung von alldem, konkret
gemacht: dieselbe "erweitern ohne zu ändern"-Form, die dir Kurs 5s
polymorphe Liste und Kurs 13s Events schon gegeben haben, hier auf ein
echtes Refactoring angewandt statt auf einen Entwurf von Grund auf.
[`code/SeasonalDiscount.cs`](../code/SeasonalDiscount.cs) hat ein
Beispiel.

## Was du gelernt hast

- Eine wachsende `if`/`else if`-Kette als Zeichen erkennen, dass ein
  Strategy-Pattern besser passen könnte — ein Interface plus eine kleine
  Klasse pro tatsächlichem Verhalten
- Dass du diese Form schon seit Kurs 4 benutzt (`Machine`-Subklassen), Kurs
  6 (`ICollectible`/`ISellable`) und Kurs 13 (Event-Abonnenten) — Strategy
  ist ein Name für etwas, das du schon bauen konntest
- Das Factory-Pattern: "welches Objekt brauche ich" an einem Ort isolieren,
  ohne so zu tun, als würden Bedingungen komplett verschwinden
- `static class`, für einen Typ, der keinen Instanzzustand hält und nie
  mit `new` konstruiert wird
- Ein System erweitern, indem man neue Klassen hinzufügt statt bestehende
  zu ändern — die konkrete Bedeutung von "offen für Erweiterung,
  geschlossen für Änderung"

## Was als Nächstes kommt

Kurs 15 schließt die aktuell geplante Roadmap dieses Repositories ab, Kurs
1 bis 15. Ein Kurs 16 ist noch nicht geplant — wie ein zukünftiger
hineinpassen würde, steht in
[PROJECT-IDEAS.de.md](../../../PROJECT-IDEAS.de.md), nach denselben
Konventionen, die in [CONTRIBUTING.de.md](../../../CONTRIBUTING.de.md)
dokumentiert sind.
