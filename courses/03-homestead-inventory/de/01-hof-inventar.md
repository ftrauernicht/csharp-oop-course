🇩🇪 Deutsch | 🇬🇧 [English](../en/01-homestead-inventory.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1](../../01-basics/de/01-csharp-grundlagen.md) und [Kurs 2](../../02-cat-roster/de/01-katzenkartei.md)

# Kurs 3 – Hof-Inventar

**Ziel:** eine ganze Sammlung von Objekten verwalten statt ein paar einzeln
benannter, und einer Klasse echte Kontrolle über ihre eigenen Daten geben,
statt sie offen für jeden zugänglich zu lassen. Am Ende hast du einen
kleinen Hof, der beliebig viele Pflanzen verfolgt, von denen jede selbst
durchsetzt, wie viel sie gegossen werden kann — egal, wer gerade gießt.

Dieses Kapitel gibt dir merklich weniger fertigen Code als Kurs 1 und 2, mit
Absicht: Die Kernübung unten ist der eigentliche Punkt dieses Kapitels,
deshalb steht sie in Schritten beschrieben, die du selbst schreibst, statt
fertig vorgegeben zu sein. Die fertige Version, inklusive der optionalen
Teile und der Herausforderung, liegt in [`code/`](../code/) —
`Crop.cs`, `Animal.cs` und `Program.cs` — falls du feststeckst oder
vergleichen willst, sobald du fertig bist.

## 🟢 Kern — Eine Variable pro Objekt skaliert nicht

Kurs 2 hat Katzen jeweils in einer einzeln benannten Variable erzeugt —
`var whiskers = new Cat(...)`, `var mochi = new Cat(...)`. Das geht für
zwei oder drei, aber ein Hof hat vielleicht ein Dutzend Pflanzen, und die
genaue Anzahl kennst du vorher nicht. Du brauchst eine Möglichkeit, *viele*
Objekte desselben Typs, in beliebiger Menge, an einem Ort zu halten.

## 🟢 Kern — List\<T\>

```csharp
var crops = new List<Crop>();
crops.Add(new Crop("Carrot"));
crops.Add(new Crop("Potato"));

Console.WriteLine(crops.Count); // 2
```

[`List<T>`](https://learn.microsoft.com/de-de/dotnet/api/system.collections.generic.list-1)
ist eine wachsende, geordnete Sammlung — das `<T>` ist ein Platzhalter für
"den Typ, den diese Liste enthält" (hier `Crop`), also liest sich
`List<Crop>` als "eine Liste von Crops". `.Add(...)` hängt ein Element an,
`.Count` sagt dir, wie viele gerade drin sind. Du kannst eine Liste auch
direkt bei der Erzeugung füllen, meist sauberer als mehrere einzelne
`.Add(...)`-Aufrufe:

```csharp
var crops = new List<Crop>
{
    new Crop("Carrot"),
    new Crop("Potato"),
    new Crop("Pumpkin"),
};
```

## 🟢 Kern — foreach

```csharp
foreach (var crop in crops)
{
    Console.WriteLine(crop.Name);
}
```

[`foreach`](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/statements/iteration-statements#the-foreach-statement)
führt seinen Block einmal für jedes Element einer Sammlung aus, wobei
`crop` bei jedem Durchlauf auf das aktuelle Element zeigt — kein manuelles
Zählen, kein Risiko eines Off-by-one-Fehlers, wie es bei einer
handgeschriebenen indexbasierten Schleife passieren kann. Lies es als "für
jede Pflanze in crops, mach Folgendes." Zu `foreach` greifst du immer dann,
wenn du mit *jedem* Element einer Liste etwas machen willst — das ist die
meiste Zeit.

## 🟢 Kern — Eine erste Version von Crop

Starte mit derselben Form, die Kurs 2 für `Cat` benutzt hat:

```csharp
public class Crop
{
    public string Name { get; set; }
    public int WaterLevel { get; set; }

    public Crop(string name)
    {
        Name = name;
        WaterLevel = 0;
    }
}
```

Verdrahte es und probier es aus:

```csharp
var crops = new List<Crop>
{
    new Crop("Carrot"),
    new Crop("Potato"),
    new Crop("Pumpkin"),
};

foreach (var crop in crops)
{
    crop.WaterLevel += 40;
}

foreach (var crop in crops)
{
    Console.WriteLine($"{crop.Name}: water level {crop.WaterLevel}");
}
```

Das läuft einwandfrei — aber nichts hindert einen Fehler wie
`crop.WaterLevel = -999;` oder `crop.WaterLevel = 250;` daran, irgendwo
sonst im Programm zu passieren, und eine Pflanze in einem Zustand zu
hinterlassen, der eigentlich gar nicht möglich sein sollte. Der
Wasserstand einer Pflanze sollte immer zwischen 0 (staubtrocken) und 100
(vollständig gegossen) bleiben, ganz ohne Ausnahme — und diese Regel sollte
nicht davon abhängen, dass jede einzelne Stelle im Code, die `WaterLevel`
anfasst, daran denkt, das selbst zu prüfen.

## 🟢 Kern — Eine Property, die ihre eigene Regel durchsetzt

Genau das kann eine Property einer Klasse garantieren, wenn du sie mit
echter Logik schreibst statt mit der `{ get; set; }`-Kurzform aus Kurs 2:

```csharp
public class Crop
{
    private int _waterLevel;

    public string Name { get; }

    public int WaterLevel
    {
        get => _waterLevel;
        set
        {
            // dein Code hier: begrenze `value` auf den Bereich 0-100,
            // bevor du es in _waterLevel speicherst
        }
    }

    public Crop(string name)
    {
        Name = name;
        WaterLevel = 0;
    }

    public void Water(int amount)
    {
        WaterLevel += amount;
    }
}
```

Ein paar neue Teile hier:

- `private int _waterLevel;` ist ein **privates Feld** — anders als die
  Properties, die du bisher benutzt hast, bedeutet `private`, dass nur Code
  *innerhalb* dieser Klasse direkt darauf zugreifen kann. Der führende
  Unterstrich ist eine übliche C#-Konvention für ein privates Backing-Feld,
  damit auf einen Blick klar ist, was das Feld und was die Property ist.
- `public string Name { get; }` hat gar kein `set` — eine **nur-lesbare
  Property**. Sie lässt sich nur im Konstruktor zuweisen (dort siehst du
  `Name = name;`) und danach nie wieder. Nicht jede Property muss
  änderbar sein.
- `WaterLevel` ist jetzt eine **vollständige Property**: `get => _waterLevel;`
  gibt zurück, was gerade gespeichert ist, und der `set { ... }`-Block
  läuft *jedes Mal*, wenn jemand `crop.WaterLevel = ...` schreibt —
  auch von innerhalb `Water(...)` aus, das jetzt durch genau dieselbe Regel
  läuft wie alles andere, statt das Feld direkt anzufassen.

Füll den `set`-Block selbst aus: Ist `value` (die eingehende Zahl) unter 0,
speicher stattdessen 0; ist sie über 100, speicher stattdessen 100; sonst
speicher `value` wie übergeben. Test es, indem du an beiden Enden
übertreibst — `crop.Water(1000)` sollte `WaterLevel` bei genau 100
belassen, und `crop.WaterLevel = -20;` sollte es bei genau 0 belassen.
Falls du feststeckst, hat [`code/Crop.cs`](../code/Crop.cs) eine
funktionierende Version.

Für das, was du gerade gebaut hast, gibt es einen Namen: eine Klasse, die
den Zugriff auf ihre eigenen Daten kontrolliert, statt darauf zu vertrauen,
dass jeder, der sie benutzt, immer das Richtige tut, ist **Kapselung** —
dieselbe Idee, die Kurs 2 schon eingeführt hat, jetzt mit einer echten
Regel dahinter statt nur einem Bündel von Feldern. Mehr:
[Microsoft Learn – Properties](https://learn.microsoft.com/de-de/dotnet/csharp/programming-guide/classes-and-structs/properties).

## 🟡 Optional — Eine Property, die nur die Klasse selbst setzen darf

`IsHarvestable` soll `true` werden, sobald eine Pflanze vollständig gegossen
ist, aber das sollte kein Code von außen einfach so setzen können — niemand
sollte `crop.IsHarvestable = true;` schreiben und das Gießen komplett
überspringen können. Ein **privater Setter** macht genau das:

```csharp
public bool IsHarvestable { get; private set; }
```

Jeder kann `crop.IsHarvestable` *lesen*, aber nur Code innerhalb von `Crop`
selbst kann es *schreiben*. Setz es im `set`-Block von `WaterLevel` auf
`true`, sobald `_waterLevel` 100 erreicht, und füg eine `Harvest()`-Methode
hinzu:

```csharp
public void Harvest()
{
    if (IsHarvestable)
    {
        Console.WriteLine($"Harvested {Name}!");
        WaterLevel = 0;
        IsHarvestable = false;
    }
    else
    {
        Console.WriteLine($"{Name} isn't ready to harvest yet.");
    }
}
```

## 🔴 Optional, echte Herausforderung — Tiere

Wend genau dasselbe Muster auf eine zweite, unabhängige Klasse an. Schreib
eine `Animal`-Klasse mit:

- Einer nur-lesbaren `Name`-Property, gesetzt im Konstruktor.
- Einer `Happiness`-Property (`int`), validiert genauso wie `WaterLevel` —
  begrenzt auf den Bereich 0 bis 100.
- Einer `Pet()`-Methode, die `Happiness` um 20 erhöht.

Bau dann eine `List<Animal>`, lauf mit `foreach` darüber und ruf `Pet()`
auf jedem Tier auf, und bestätige, dass die Begrenzung genauso funktioniert
wie bei den Pflanzen: `animal.Happiness = 1000;` sollte es bei genau 100
belassen. Vergleich mit [`code/Animal.cs`](../code/Animal.cs), sobald es
funktioniert.

## Was du gelernt hast

- `List<T>`: eine wachsende Sammlung von Objekten eines Typs, `.Add(...)`,
  `.Count`
- `foreach`, um denselben Code auf jedes Element einer Sammlung anzuwenden
- Private Felder (`private int _waterLevel;`) gegenüber öffentlichen
  Properties
- Nur-lesbare Properties (`{ get; }`), nur im Konstruktor setzbar
- Eine vollständige Property mit echter Validierungslogik im `set`-Block —
  Kapselung, tatsächlich eingesetzt
- Ein privater Setter (`{ get; private set; }`) für eine Property, die nur
  ihre eigene Klasse ändern können soll

## Weiter

Kurs 3 steht für sich allein und braucht nur Kurs 1 und 2. Für den Rest der
Roadmap dieses Repositories siehe
[PROJECT-IDEAS.de.md](../../../PROJECT-IDEAS.de.md).
