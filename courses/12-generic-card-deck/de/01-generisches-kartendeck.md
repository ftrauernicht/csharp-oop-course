🇩🇪 Deutsch | 🇬🇧 [English](../en/01-generic-card-deck.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1](../../01-basics/de/01-csharp-grundlagen.md) und [Kurs 2](../../02-cat-roster/de/01-katzenkartei.md)

# Kurs 12 – Generisches Kartendeck

**Ziel:** ein `Deck<T>`, das du selbst baust und das identisch für ein
Deck Spielkarten, ein Deck Zahlen oder alles andere funktioniert — genauso
wie `List<T>` für alles funktioniert hat, was du ihr seit Kurs 3 gegeben
hast. Am Ende weißt du, wie man eine generische Klasse und eine generische
Methode schreibt, und was ein Type Constraint (`where T : ...`) dir
tatsächlich bringt.

Wie in Kurs 3, 4, 6, 8, 9, 10 und 11 steht die Kernübung unten in Schritten
beschrieben, die du selbst schreibst. Die fertige Version, inklusive der
Herausforderung, liegt in [`code/`](../code/).

## 🟢 Kern — Eine generische Klasse schreiben

```csharp
public class Deck<T>
{
    private readonly List<T> _cards = new List<T>();

    public int Count
    {
        get { return _cards.Count; }
    }

    public void Add(T card)
    {
        _cards.Add(card);
    }

    public T Draw()
    {
        if (_cards.Count == 0)
        {
            throw new InvalidOperationException("The deck is empty.");
        }

        T card = _cards[0];
        _cards.RemoveAt(0);
        return card;
    }

    public void Shuffle()
    {
        var random = new Random();
        for (int i = _cards.Count - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            T temp = _cards[i];
            _cards[i] = _cards[j];
            _cards[j] = temp;
        }
    }
}
```

`<T>` direkt nach dem Klassennamen ist ein **Type Parameter** — ein
Platzhalter für den Typ, mit dem du diese Klasse benutzt, ausgefüllt, wenn
du tatsächlich `Deck<Card>` oder `Deck<int>` schreibst. Jedes `T` im
Klassenkörper bedeutet dann "der Typ, mit dem dieses konkrete Deck erzeugt
wurde." Das ist genau derselbe Mechanismus, den `List<T>` selbst benutzt.
Eine generische Klasse *benutzt* du schon seit Kurs 3; mit `Deck<T>`
*schreibst* du jetzt selbst eine. Mehr:
[Microsoft Learn – Generische Klassen](https://learn.microsoft.com/de-de/dotnet/csharp/fundamentals/types/generics).

```csharp
var deck = new Deck<Card>();
deck.Add(new Card("Ace", "Spades"));
deck.Add(new Card("King", "Hearts"));

var numberDeck = new Deck<int>();
numberDeck.Add(7);
numberDeck.Add(42);
```

Dieselbe Klasse, zwei völlig unverwandte Elementtypen, null Änderungen an
`Deck<T>` selbst nötig für beide.

`Shuffle` nutzt echten Zufall (der Fisher-Yates-Shuffle, falls du das
Memory-Spiel des Geschwister-Kurses in JavaScript gemacht hast — derselbe
Algorithmus). Welche Karte also am Ende zuerst dran ist, unterscheidet sich
wirklich bei jedem Durchlauf. Nur dass `Count` gleich bleibt, ist
garantiert:

```csharp
Console.WriteLine(deck.Count); // 2, vor dem Mischen
deck.Shuffle();
Console.WriteLine(deck.Count); // immer noch 2, Reihenfolge geändert, Count nicht
```

## 🟢 Kernübung — Eine generische Methode mit Constraint

```csharp
var numbers = new List<int> { 3, 7, 2, 9, 4 };
Console.WriteLine(FindHighest(numbers)); // 9
```

Schreib `FindHighest`, eine generische Methode, die das größte Element in
einer beliebigen `List<T>` findet, solange `T` überhaupt vergleichbar
ist:

```csharp
T FindHighest<T>(List<T> items) where T : IComparable<T>
{
    // dein Code hier: lauf durch items, merk dir das bisher größte
    // gesehene Element (mit .CompareTo, aus Kurs 10), und gib es zurück
}
```

`where T : IComparable<T>` ist ein **Type Constraint**: Es schränkt ein,
welche Typen hier überhaupt als `T` erlaubt sind, auf solche, die
[`IComparable<T>`](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/builtin-types/reference-types#the-icomparable-interfaces)
implementieren — genau das Interface, das Kurs 10 `Coin` implementieren
ließ. Ohne das Constraint würde `item.CompareTo(highest)` innerhalb der
Methode gar nicht erst kompilieren: ein unbeschränktes, einfaches `T`
könnte *alles* sein, und der Compiler hat keine Möglichkeit zu wissen,
dass es eine `CompareTo`-Methode hat, solange du nicht verlangst, dass es
eine hat. `int` implementiert bereits `IComparable<int>`, genau deshalb
funktioniert `FindHighest(numbers)` oben einfach so. Falls du feststeckst,
hat [`code/Program.cs`](../code/Program.cs) eine funktionierende Version.

Probier, `FindHighest` stattdessen mit einer `List<Card>` aufzurufen. Es
weigert sich zu kompilieren: `error CS0311: The type 'Card' cannot be
used as type parameter 'T'... There is no implicit reference conversion
from 'Card' to 'System.IComparable<Card>'`. `Card` (aus diesem Kapitel)
hat dieses Interface nie implementiert, das Constraint weist es also
korrekt ab, zur Kompilierzeit, bevor das Programm je läuft.

## 🔴 Optional, echte Herausforderung — Eine andere Art von Constraint

```csharp
T2 CreateDefault<T2>() where T2 : new()
{
    return new T2();
}
```

`where T2 : new()` ist eine andere Art von Constraint: Es verlangt nicht,
ein bestimmtes Interface zu implementieren, sondern nur einen öffentlichen
**parameterlosen Konstruktor** zu haben — sodass `new T2()` innerhalb der
Methode garantiert erlaubt ist, egal was `T2` am Ende ist. Probier
`CreateDefault<List<int>>()` (eine frische, leere Liste). Probier danach
`CreateDefault<Card>()`, das sich weigert zu kompilieren, weil `Card`s
einziger Konstruktor einen `rank` und einen `suit` verlangt:
`error CS0310: 'Card' must be a non-abstract type with a public
parameterless constructor`. [`code/Program.cs`](../code/Program.cs) hat
eine funktionierende Version des ganzen Kapitels.

## Was du gelernt hast

- Eine generische Klasse schreiben (`class Deck<T>`) und sie mit völlig
  unterschiedlichen Typargumenten benutzen, ohne die Klasse selbst zu
  ändern
- Generische Methoden (`T FindHighest<T>(...)`), getrennt von generischen
  Klassen
- Type Constraints: `where T : IComparable<T>` (muss ein Interface
  implementieren) und `where T2 : new()` (muss einen parameterlosen
  Konstruktor haben); ein Verstoß gegen beide ist ein
  Kompilierzeit-Fehler, keine Laufzeit-Überraschung

## Weiter

Kurs 12 steht für sich allein und braucht nur Kurs 1 und 2. Für den Rest
der Roadmap dieses Repositories siehe
[PROJECT-IDEAS.de.md](../../../PROJECT-IDEAS.de.md).
