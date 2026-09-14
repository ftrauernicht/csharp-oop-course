🇩🇪 Deutsch | 🇬🇧 [English](../en/01-cat-roster.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1 – Basics](../../01-basics/de/01-csharp-grundlagen.md)

# Kurs 2 – Katzenkartei

**Ziel:** dein erster echter Schritt in die objektorientierte Programmierung
— zusammengehörige Daten und das Verhalten, das dazugehört, in einem Ding
bündeln, statt einen Haufen loser Variablen zu jonglieren. Am Ende hast du
eine Handvoll unabhängiger `Cat`-Objekte, die sich selbst vorstellen können,
und weißt genau, was eine Klasse, ein Objekt, ein Konstruktor und eine
Methode wirklich sind.

Eine fertige Version von allem in diesem Kapitel liegt in
[`code/Cat.cs`](../code/Cat.cs) und [`code/Program.cs`](../code/Program.cs),
inklusive der Herausforderung am Ende.

## 🟢 Kern — Das Problem mit losen Variablen

Angenommen, du willst drei Katzen mit dem verfolgen, was Kurs 1 dir gegeben
hat — Variablen und `Console.WriteLine`:

```csharp
string cat1Name = "Whiskers";
int cat1Age = 3;
string cat1FavoriteToy = "crinkly ball";

string cat2Name = "Mochi";
int cat2Age = 1;
string cat2FavoriteToy = "feather wand";

Console.WriteLine($"Hi, I'm {cat1Name}! Age: {cat1Age}. Favorite toy: {cat1FavoriteToy}.");
Console.WriteLine($"Hi, I'm {cat2Name}! Age: {cat2Age}. Favorite toy: {cat2FavoriteToy}.");
```

Das funktioniert, skaliert aber schlecht, und nichts hindert dich daran,
aus Versehen `cat2Name` zu tippen, wo du `cat1Name` meintest — die
Verbindung "diese drei Variablen gehören zur selben Katze" existiert nur in
den Namen, die du zufällig gewählt hast, nicht im Code selbst. Eine
**Klasse** behebt genau das: Sie lässt dich "eine Katze hat einen Namen,
ein Alter und ein Lieblingsspielzeug" *einmal* als Bauplan definieren, und
dann so viele echte Katzen daraus erzeugen, wie du willst.

## 🟢 Kern — Eine Klasse definieren

```csharp
public class Cat
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string FavoriteToy { get; set; }
}
```

`Name`, `Age` und `FavoriteToy` sind **Properties** — das ist die normale,
idiomatische Art, ein Datenfeld auf einer C#-Klasse nach außen anzubieten.
Der Teil `{ get; set; }` sieht aus, als würde er nichts mehr tun als eine
gewöhnliche Variable, und im Moment stimmt das auch: Er ist eine
Kurzschreibweise, die im Hintergrund ein verstecktes Feld anlegt, plus
Standardlogik zum "Lesen" und "Schreiben". Kurs 3 öffnet diese
Kurzschreibweise und packt echte Logik hinein. Für jetzt reicht es, eine
Property wie ein beschriftetes Datenfach der Klasse zu behandeln. Mehr:
[Microsoft Learn – Properties](https://learn.microsoft.com/de-de/dotnet/csharp/programming-guide/classes-and-structs/properties).

Diese `Cat`-Klasse für sich allein steht für keine bestimmte Katze — sie ist
ein Bauplan, genau wie ein Plätzchenausstecher kein Plätzchen ist. Gebacken
wurde noch nichts.

## 🟢 Kern — Objekte mit `new` erzeugen

```csharp
var whiskers = new Cat();
whiskers.Name = "Whiskers";
whiskers.Age = 3;
whiskers.FavoriteToy = "crinkly ball";
```

`new Cat()` nutzt den Bauplan, um ein echtes **Objekt** (auch **Instanz**
genannt) im Speicher zu erzeugen, und `whiskers` ist eine Variable, die
darauf zeigt. Du kannst beliebig viele aus derselben Klasse erzeugen:

```csharp
var mochi = new Cat();
mochi.Name = "Mochi";
mochi.Age = 1;
mochi.FavoriteToy = "feather wand";
```

`whiskers` und `mochi` sind beide `Cat`-Objekte, aus genau demselben Bauplan
gebaut, mit völlig unabhängigen Daten — `mochi.Age` zu ändern rührt
`whiskers.Age` niemals an. Dieses zeilenweise Aufsetzen funktioniert, ist
aber umständlich, und schlimmer: nichts zwingt dich dazu, wirklich jede
Property zu setzen, bevor du das Objekt benutzt. Die Lösung ist ein
**Konstruktor**.

## 🟢 Kern — Der Konstruktor

Ein Konstruktor ist eine besondere Methode, die automatisch in dem Moment
läuft, in dem `new` ein Objekt baut — so kannst du ihm alles mitgeben, was
das Objekt direkt bei der Erzeugung braucht:

```csharp
public class Cat
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string FavoriteToy { get; set; }

    public Cat(string name, int age, string favoriteToy)
    {
        Name = name;
        Age = age;
        FavoriteToy = favoriteToy;
    }
}
```

Ein Konstruktor heißt exakt wie seine Klasse, hat keinen Rückgabetyp (nicht
mal `void`), und seine Aufgabe ist es, das neue Objekt in einem sinnvollen
Ausgangszustand zu hinterlassen. Eine fertig aufgesetzte Katze zu erzeugen
ist jetzt eine Zeile:

```csharp
var whiskers = new Cat("Whiskers", 3, "crinkly ball");
var mochi = new Cat("Mochi", 1, "feather wand");
```

**Eine Anmerkung zu `this`:** Du siehst Konstruktoren oft mit
`this.Name = name;` geschrieben, statt einfach `Name = name;`. Hier tun
beide exakt dasselbe, weil der Parameter (`name`, klein geschrieben) und die
Property (`Name`, groß geschrieben) unterschiedliche Groß-/Kleinschreibung
haben — genau deshalb folgt echter C#-Code dieser Konvention: Sie vermeidet
die Kollision von vornherein. `this` bedeutet "das Objekt, auf dem dieser
Code gerade läuft", und es wird *notwendig*, nicht nur eine Stilfrage,
sobald der Name eines Parameters wirklich exakt mit dem einer Property
übereinstimmt:

```csharp
public Cat(string Name) // Parameter absichtlich wie die Property benannt
{
    this.Name = Name; // this.Name ist die Property; Name allein ist der Parameter
    // ohne `this.` würde `Name = Name;` den Parameter sich selbst zuweisen,
    // und die Property bliebe stillschweigend leer
}
```

Mehr: [Microsoft Learn – Konstruktoren](https://learn.microsoft.com/de-de/dotnet/csharp/programming-guide/classes-and-structs/constructors).

## 🟢 Kern — Instanzmethoden

Eine Property hält Daten; eine **Methode** auf einer Klasse gibt ihr
Verhalten:

```csharp
public class Cat
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string FavoriteToy { get; set; }

    public Cat(string name, int age, string favoriteToy)
    {
        Name = name;
        Age = age;
        FavoriteToy = favoriteToy;
    }

    public void Introduce()
    {
        Console.WriteLine($"Hi, I'm {Name}! Age: {Age}. Favorite toy: {FavoriteToy}.");
    }
}
```

`Introduce` sieht aus wie die Methoden aus Kurs 1, mit einem Unterschied,
der viel ausmacht: Sie nimmt `Name`, `Age` oder `FavoriteToy` nicht als
Parameter entgegen — sie benutzt sie direkt, weil sie beim Aufruf auf einer
bestimmten Katze schon weiß, auf welcher sie gerade läuft:

```csharp
whiskers.Introduce(); // Hi, I'm Whiskers! Age: 3. Favorite toy: crinkly ball.
mochi.Introduce();    // Hi, I'm Mochi! Age: 1. Favorite toy: feather wand.
```

Dieselbe Methode, derselbe Code, zwei völlig unterschiedliche Ausgaben —
weil `whiskers` und `mochi` unterschiedliche Objekte sind, jedes mit
eigenen Werten für diese Properties.

**Probier es selbst:** Füg `Cat` eine vierte Property hinzu, `FavoriteFood`,
übergib sie im Konstruktor, und füg sie zur Ausgabe von `Introduce` hinzu.

## 🟡 Optional — Eine Methode, die den Zustand ändert

Methoden sind nicht darauf beschränkt, Properties zu lesen und auszugeben —
sie können den Zustand eines Objekts auch ändern:

```csharp
public void HaveBirthday()
{
    Age++;
    Console.WriteLine($"{Name} just turned {Age}!");
}
```

```csharp
mochi.HaveBirthday(); // Mochi just turned 2!
```

`mochi.Age` ist jetzt dauerhaft `2` — die Änderung bleibt genau wie bei
jeder anderen Property-Änderung bestehen, weil eine Methode vollen Zugriff
auf das Objekt hat, zu dem sie gehört (und es verändern kann).

## 🔴 Optional, echte Herausforderung — Zwei Katzen vergleichen

Füg `Cat` eine Methode hinzu, die eine andere `Cat` als Parameter nimmt und
zurückgibt, ob diese Katze älter ist:

```csharp
public bool IsOlderThan(Cat other)
{
    // dein Code hier
}
```

Probier es selbst, bevor du in [`code/Cat.cs`](../code/Cat.cs) reinschaust.
Sobald es funktioniert:

```csharp
if (tom.IsOlderThan(whiskers))
{
    Console.WriteLine($"{tom.Name} is older than {whiskers.Name}.");
}
```

Ein Tipp, falls du ihn willst: Innerhalb der Methode bezieht sich `Age` auf
das Alter *dieser* Katze (der, auf der die Methode aufgerufen wird), und
`other.Age` greift auf die `Cat` zu, die übergeben wurde — dieselbe
Punkt-Syntax, die du überall sonst benutzt hast, nur diesmal auf einem
Parameter statt direkt auf `whiskers` oder `mochi`.

## Was du gelernt hast

- Warum zusammengehörige Daten in einer Klasse besser sind als ein Haufen
  loser Variablen
- Eine Klasse mit Properties definieren (`public string Name { get; set; }`)
- Objekte mit `new` erzeugen, und dass jedes eine unabhängige Instanz ist
- Konstruktoren, und was `this` bedeutet (und wann es wirklich nötig ist)
- Instanzmethoden, die den Zustand eines Objekts lesen (`Introduce`) oder
  ändern (`HaveBirthday`)
- Eine Methode, die eine andere Instanz derselben Klasse als Parameter
  nimmt (`IsOlderThan`)

Für das, was du gerade gebaut hast, gibt es einen Namen: Die Daten eines
Objekts zusammen mit dem Verhalten zu bündeln, das darauf arbeitet, sodass
man für die Benutzung einer `Cat` nur wissen muss, was sie kann
(`Introduce()`, `HaveBirthday()`), nicht wie ihr Innenleben funktioniert,
nennt man **Kapselung**. Mehr:
[Microsoft Learn – Objektorientierte Programmierung (C#)](https://learn.microsoft.com/de-de/dotnet/csharp/fundamentals/object-oriented/).
Du begegnest ihr in Kurs 3 wieder, ganz bewusst — diesmal mit Properties,
die tatsächlich Regeln über die Daten durchsetzen, die sie halten.

## Weiter

Das war der zweite und letzte der beiden verpflichtenden Grundlagenkurse
dieses Repositories. Kurs 3 und alle weiteren lassen sich in beliebiger
Reihenfolge machen, solange Kurs 1 und 2 zuerst gemacht wurden. Kurs 3 ist
noch nicht veröffentlicht; schau in der
[Repository-Übersicht](../../../README.de.md) nach, was aktuell verfügbar
ist.
