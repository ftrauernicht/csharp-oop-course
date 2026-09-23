🇩🇪 Deutsch | 🇬🇧 [English](../en/01-coin-purse.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1](../../01-basics/de/01-csharp-grundlagen.md) und [Kurs 2](../../02-cat-roster/de/01-katzenkartei.md)

# Kurs 10 – Geldbeutel

**Ziel:** Zwei `Coin`-Objekte, die dieselbe Münzsorte darstellen, zählen als
gleich, lassen sich korrekt nebeneinander sortieren und erzeugen keine
Duplikate in einem `HashSet`. Am Ende weißt du, warum das für eine selbst
geschriebene Klasse nichts davon automatisch passiert, und das
Standardmuster, das es behebt.

Wie in Kurs 3, 4, 6, 8 und 9 steht die Kernübung unten in Schritten
beschrieben, die du selbst schreibst. Die fertige Version, inklusive der
Herausforderung, liegt in [`code/`](../code/).

## 🟢 Kern — Die Überraschung: `==` vergleicht standardmäßig Referenzen

```csharp
public class NaiveCoin
{
    public int Denomination { get; }

    public NaiveCoin(int denomination)
    {
        Denomination = denomination;
    }
}
```

```csharp
var naive1 = new NaiveCoin(5);
var naive2 = new NaiveCoin(5);
Console.WriteLine(naive1 == naive2); // False
```

`False`, obwohl beide "5 Cent" darstellen. Bei einer einfachen Klasse
vergleichen `==` und das geerbte
[`Equals`](https://learn.microsoft.com/de-de/dotnet/api/system.object.equals)
*Referenzgleichheit*: "ist das buchstäblich dasselbe Objekt im Speicher",
nicht "stellen diese beiden Objekte denselben Wert dar". `int` und `string`
fühlen sich an, als würden sie Werte vergleichen, weil .NET dieses
Verhalten für sie schon überschrieben hat. Eine selbst geschriebene Klasse
bekommt das nicht geschenkt.

## 🟢 Kern — Equals und GetHashCode, zusammen

```csharp
public override bool Equals(object? obj)
{
    if (obj is not Coin other)
    {
        return false;
    }

    return Denomination == other.Denomination;
}

public override int GetHashCode()
{
    return Denomination.GetHashCode();
}
```

`obj is not Coin other` kombiniert Kurs 6s Pattern-Matching-`is` mit `not`
— `false`, falls `obj` gar keine `Coin` ist, sonst ist `other` einsatzbereit,
schon als `Coin` typisiert. Diese beiden Overrides sind ein
zusammengehöriges Paar, per Vertrag: Wenn `Equals` sagt, zwei Objekte
seien gleich, *muss* `GetHashCode` für beide denselben Wert liefern,
sonst verhalten sich Collection-Typen, die auf Hashing beruhen
(`HashSet<T>`, `Dictionary<TKey, TValue>`), auf eine Art fehlerhaft, die
sich wirklich schwer debuggen lässt. Hier hängen beide komplett von
`Denomination` ab, der Vertrag gilt also automatisch. Mehr:
[Microsoft Learn – Equals und GetHashCode](https://learn.microsoft.com/de-de/dotnet/api/system.object.equals#notes-to-inheritors).

## 🟢 Kern — == und != überladen

```csharp
public static bool operator ==(Coin? left, Coin? right)
{
    if (left is null)
    {
        return right is null;
    }

    return left.Equals(right);
}

public static bool operator !=(Coin? left, Coin? right)
{
    return !(left == right);
}
```

`Equals` allein zu überschreiben ändert nicht, was der `==`-*Operator*
tut. Das sind zwei getrennte Dinge, die meistens übereinstimmen. `static
bool operator ==(...)` ist
[**Operator-Overloading**](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/operators/operator-overloading):
festlegen, was `==` für deinen Typ überhaupt bedeutet. C# verlangt, `==`
und `!=` zusammen als Paar zu überladen. Die `left is null`-Prüfung zählt —
ohne sie würde `left.Equals(right)` werfen, falls `left` selbst `null`
wäre, und genau diesen Fall muss `==` sauber abfangen.

```csharp
var coin1 = new Coin(5);
var coin2 = new Coin(5);
Console.WriteLine(coin1 == coin2); // True
```

## 🟢 Kern — Was dir das wirklich bringt: Entdopplung

```csharp
var uniqueCoins = new HashSet<Coin>
{
    new Coin(5),
    new Coin(10),
    new Coin(5), // ein Duplikat
};

Console.WriteLine(uniqueCoins.Count); // 2
```

[`HashSet<T>`](https://learn.microsoft.com/de-de/dotnet/api/system.collections.generic.hashset-1)
behält nur eines von allem, was es als gleich betrachtet — und "als gleich
betrachtet" bedeutet genau `Equals`/`GetHashCode`, das Paar, das du gerade
geschrieben hast. Ohne sie kämen `NaiveCoin(5)` und eine weitere
`NaiveCoin(5)` beide rein, weil keine der beiden per Referenz der anderen
gleicht.

## 🟢 Kernübung — Schreib IComparable\<Coin\> selbst

```csharp
public class Coin : IComparable<Coin>
{
    // ... Equals, GetHashCode, ==, != von oben ...

    public int CompareTo(Coin? other)
    {
        // dein Code hier: gib eine negative Zahl zurück, wenn diese Münze
        // weniger wert ist als `other`, null bei Gleichheit, positiv, wenn
        // sie mehr wert ist -- gib 1 zurück, falls `other` null ist
        // (diese Münze "kommt nach" gar nichts)
    }
}
```

[`IComparable<T>`](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/builtin-types/reference-types#the-icomparable-interfaces)
ist das, was
[`List<T>.Sort()`](https://learn.microsoft.com/de-de/dotnet/api/system.collections.generic.list-1.sort)
intern aufruft, um die Reihenfolge zu entscheiden. Du musst
die Vergleichslogik nicht von Hand schreiben — `int` hat schon
`.CompareTo(...)`, und `Denomination` ist ein `int`, also ist das eine
Zeile, die es weiterreicht. Test es:

```csharp
var purse = new List<Coin> { new Coin(25), new Coin(1), new Coin(10), new Coin(5) };
purse.Sort();
Console.WriteLine(string.Join(", ", purse)); // 1, 5, 10, 25 cents, in Reihenfolge
```

Falls du feststeckst, hat [`code/Coin.cs`](../code/Coin.cs) eine
funktionierende Version.

## 🟡 Optional — ToString, auch überschrieben

`string.Join(", ", purse)` oben liest sich nur deshalb sauber, weil `Coin`
noch eine geerbte Methode überschreibt:

```csharp
public override string ToString()
{
    return $"{Denomination}-cent coin";
}
```

Jede Klasse hat schon ein `ToString()` — das Standard-`ToString()` gibt
einfach den Klassennamen aus, weshalb `Console.WriteLine(irgendeinObjekt)`
bei einer nicht überschriebenen Klasse so etwas Nutzloses wie `Coin`
ausgibt. Es zu überschreiben sorgt dafür, dass `$"{coin}"` und
`Console.WriteLine(coin)` etwas Sinnvolles ausgeben, ohne dass man sich
überall merken muss, stattdessen `coin.Denomination` zu schreiben.

## 🔴 Optional, echte Herausforderung — Vergleichsoperatoren, auf CompareTo aufgebaut

Füg `Coin` `<` und `>` hinzu, definiert über das `CompareTo`, das du schon
geschrieben hast:

```csharp
public static bool operator <(Coin left, Coin right)
{
    // dein Code hier
}

public static bool operator >(Coin left, Coin right)
{
    // dein Code hier
}
```

C# verlangt auch `<`/`>` als Paar (und getrennt davon `<=`/`>=` als eigenes
Paar, das diese Herausforderung nicht verlangt). Bestätige, dass
`new Coin(5) < new Coin(10)` `true` ist und `new Coin(25) > new Coin(10)`
`true` ist. [`code/Coin.cs`](../code/Coin.cs) hat eine funktionierende
Version.

## Was du gelernt hast

- Warum `==`/`Equals` bei einer selbst geschriebenen Klasse standardmäßig
  Referenzen vergleichen, nicht Werte
- `Equals` und `GetHashCode` zusammen überschreiben, und warum sie
  übereinstimmen müssen
- `==`/`!=` als Operatoren überladen, getrennt vom Überschreiben von
  `Equals`
- Warum die Entdopplung von `HashSet<T>` komplett von einem korrekten
  `Equals`/`GetHashCode`-Paar abhängt
- `IComparable<T>` und `CompareTo`, was `List<T>.Sort()` tatsächlich
  aufruft
- `ToString()` überschreiben, und `<`/`>` auf `CompareTo` aufgebaut

## Weiter

Kurs 10 steht für sich allein und braucht nur Kurs 1 und 2. Für den Rest
der Roadmap dieses Repositories siehe
[PROJECT-IDEAS.de.md](../../../PROJECT-IDEAS.de.md).
