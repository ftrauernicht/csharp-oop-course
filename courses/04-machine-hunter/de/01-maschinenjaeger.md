🇩🇪 Deutsch | 🇬🇧 [English](../en/01-machine-hunter.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1](../../01-basics/de/01-csharp-grundlagen.md) und [Kurs 2](../../02-cat-roster/de/01-katzenkartei.md)

# Kurs 4 – Maschinenjäger

**Ziel:** mehrere verschiedene Maschinentypen, die sich alle einen Namen,
Gesundheit und die Fähigkeit, Schaden zu nehmen, teilen, aber jeweils
unterschiedlich angreifen, ohne `Name`, `Health` und `TakeDamage` in jede
einzelne hineinzukopieren. Am Ende weißt du, was eine Basisklasse und eine
abgeleitete Klasse wirklich sind, und was `virtual`, `override`, `base` und
`protected` jeweils tun.

Wie in Kurs 3 steht die Kernübung unten in Schritten beschrieben, die du
selbst schreibst. Die fertige Version, inklusive der optionalen Teile und
der Herausforderung, liegt in [`code/`](../code/).

## 🟢 Kern — Das Problem mit kopierten Klassen

Stell dir vor, du schreibst `Watcher` und `Thunderjaw` als zwei völlig
getrennte Klassen, im Kurs-2-Stil:

```csharp
public class Watcher
{
    public string Name { get; }
    public int Health { get; private set; }
    // ... TakeDamage, IsDefeated, alles kopiert ...

    public void Attack()
    {
        Console.WriteLine($"{Name} shrieks and lunges for 8 damage!");
    }
}

public class Thunderjaw
{
    public string Name { get; }
    public int Health { get; private set; }
    // ... genau dasselbe TakeDamage, IsDefeated, nochmal kopiert ...

    public void Attack()
    {
        Console.WriteLine($"{Name} fires a devastating chest cannon for 40 damage!");
    }
}
```

Alles außer `Attack()` ist identisch, und mit einem dritten oder vierten
Maschinentyp wird es nur schlimmer. **Vererbung** lässt dich den geteilten
Teil genau einmal schreiben, in einer **Basisklasse**, und jeder konkrete
Maschinentyp (eine **abgeleitete Klasse**, auch Subklasse genannt) baut
darauf auf und fügt nur hinzu oder ändert, was an ihr tatsächlich anders ist.

## 🟢 Kern — Die Basisklasse

```csharp
public class Machine
{
    public string Name { get; }
    public int MaxHealth { get; }
    public int Health { get; private set; }

    protected Machine(string name, int maxHealth)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }

    public virtual void Attack()
    {
        Console.WriteLine($"{Name} attacks!");
    }

    protected void LogAttack(string flavorText, int damage)
    {
        Console.WriteLine($"{Name} {flavorText} for {damage} damage!");
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0)
        {
            Health = 0;
        }
        Console.WriteLine($"{Name} takes {amount} damage. Health: {Health}/{MaxHealth}");
    }

    public bool IsDefeated
    {
        get { return Health <= 0; }
    }
}
```

Zwei Dinge sind hier neu, und beide zählen viel:

- Der Konstruktor ist `protected`, nicht `public`. **`protected`** bedeutet
  "nur diese Klasse und ihre abgeleiteten Klassen dürfen das benutzen":
  eine Stufe offener als `private` (nur diese Klasse), eine Stufe
  geschlossener als `public` (jeder). Eine reine, typlose `Machine` sollte
  gar nicht für sich allein existieren; nur ein konkreter Maschinentyp
  sollte es. Probier `new Machine("Test", 10)` in `Program.cs`, sobald du
  den Rest dieses Kapitels gebaut hast, und der Compiler weigert sich:
  `error CS0122: 'Machine.Machine(string, int)' is inaccessible due to its
  protection level`. Kurs 5 gibt dieser Idee einen richtigen Namen und ein
  eigenes Schlüsselwort.
- `LogAttack` ist ebenfalls `protected`: ein geteilter Helfer, den
  abgeleitete Klassen aus ihrem eigenen `Attack()` heraus aufrufen können,
  den aber Code von außen (wie `Program.cs`) gar nicht erreicht. `public`-
  Member sind die Schnittstelle deiner Klasse nach außen; `protected`-
  Member sind geteilte Werkzeuge für die *Familie* von Klassen, die darauf
  aufbauen.

## 🟢 Kern — Eine abgeleitete Klasse: `virtual`, `override` und `base`

```csharp
public class Watcher : Machine
{
    public Watcher() : base("Watcher", 30)
    {
    }

    public override void Attack()
    {
        LogAttack("shrieks and lunges", 8);
    }
}
```

`: Machine` hinter dem Klassennamen bedeutet "`Watcher` *ist eine*
`Machine`." Sie bekommt automatisch `Name`, `Health`, `TakeDamage` und
alles andere, was `Machine` definiert, geschenkt. Drei Teile machen das
möglich:

- `: base("Watcher", 30)` am Konstruktor ruft `Machine`s eigenen
  (`protected`en) Konstruktor mit `Watcher`s konkretem Namen und
  Gesundheit auf, *bevor* der eigene Konstruktor-Rumpf von `Watcher` läuft.
  Jeder Konstruktor einer abgeleiteten Klasse muss den Konstruktor der
  Basisklasse irgendwie erreichen. So geht's.
- `public virtual void Attack()` auf `Machine` markiert diese Methode als
  **überschreibbar erlaubt** für eine abgeleitete Klasse. Ohne `virtual`
  würde das `override` unten gar nicht kompilieren.
- `public override void Attack()` auf `Watcher` ersetzt `Machine`s
  generische Version durch `Watcher`s eigene, speziell für `Watcher`-
  Objekte. Mehr:
  [Microsoft Learn – Vererbung](https://learn.microsoft.com/de-de/dotnet/csharp/fundamentals/object-oriented/inheritance).

Probier es aus:

```csharp
var watcher = new Watcher();
watcher.Attack();          // Watcher shrieks and lunges for 8 damage!
watcher.TakeDamage(15);    // Watcher takes 15 damage. Health: 15/30
```

`TakeDamage` wurde auf `Watcher` nirgends geschrieben: es wird
unverändert von `Machine` geerbt und funktioniert einfach.

## 🟢 Kernübung — Schreib Thunderjaw selbst

Benutz `Watcher` oben als Vorlage und schreib eine `Thunderjaw`-Klasse mit:

- Name `"Thunderjaw"`, maximale Gesundheit `200`
- Einem `Attack()`, das `"fires a devastating chest cannon"` mit `40`
  Schaden loggt

```csharp
public class Thunderjaw : Machine
{
    // dein Code hier: Konstruktor mit : base(...), und ein Attack()-Override
}
```

Test es:

```csharp
var thunderjaw = new Thunderjaw();
thunderjaw.Attack();
thunderjaw.TakeDamage(250); // mehr als die maximale Gesundheit -- sollte bei 0 begrenzen
Console.WriteLine($"{thunderjaw.Name} defeated: {thunderjaw.IsDefeated}");
```

Falls du feststeckst, hat [`code/Thunderjaw.cs`](../code/Thunderjaw.cs)
eine funktionierende Version.

## 🟡 Optional — Überschreiben ist optional, nicht Pflicht

`virtual` bedeutet, dass eine Methode überschrieben werden *kann*. Nichts
zwingt jede abgeleitete Klasse dazu, das tatsächlich zu tun:

```csharp
public class Grazer : Machine
{
    public Grazer() : base("Grazer", 50)
    {
    }
}
```

`Grazer` hat gar kein `Attack()`-Override. `grazer.Attack()` aufzurufen
funktioniert trotzdem: es läuft `Machine`s eigene generische Version
unverändert und gibt `"Grazer attacks!"` aus. Ein Override, das das
Verhalten der Basisklasse *erweitern* statt komplett ersetzen will, kann
sie auch explizit als erste Zeile mit `base.Attack();` aufrufen und danach
mehr ergänzen, auch wenn keine der Maschinen in diesem Kapitel es braucht.

## 🔴 Optional, echte Herausforderung — Eine Maschine mit Extra

Eine abgeleitete Klasse ist nicht darauf beschränkt, zu überschreiben, was
sie erbt. Sie kann völlig neue Member hinzufügen, die auf der Basisklasse
gar nicht existieren. Schreib eine `Strider`-Klasse (Name `"Strider"`,
maximale Gesundheit `80`, `Attack()` loggt `"kicks"` mit `12` Schaden) mit
einer weiteren Methode, die nur `Strider` hat:

```csharp
public void Ride()
{
    Console.WriteLine($"You climb onto {Name} and ride across the frontier.");
}
```

`watcher.Ride()` sollte nicht kompilieren. Nur ein `Strider` lässt sich
reiten. Vergleich mit [`code/Strider.cs`](../code/Strider.cs), sobald es
funktioniert.

## Was du gelernt hast

- Warum kopierte, geteilte Member über ähnliche Klassen hinweg ein Zeichen
  dafür sind, dass du eine Basisklasse brauchst
- `: Machine` zum Vererben, `protected` für Member, die nur eine Klasse und
  ihre abgeleiteten Klassen benutzen dürfen
- `virtual` (darf überschrieben werden) und `override` (die konkrete
  Version dieser abgeleiteten Klasse)
- `base(...)`, um den Konstruktor einer Basisklasse zu erreichen;
  `base.Methode()`, um ihre Version einer Methode explizit aufzurufen
- Eine abgeleitete Klasse kann das Überschreiben einer virtuellen Methode
  auch ganz weglassen, oder völlig neue Member hinzufügen, die die
  Basisklasse nie hatte

## Weiter

Kurs 4 steht für sich allein und braucht nur Kurs 1 und 2. Für den Rest der
Roadmap dieses Repositories (inklusive Kurs 5, der alle diese Maschinen in
eine einzige Liste steckt und sie nacheinander bekämpft) siehe
[PROJECT-IDEAS.de.md](../../../PROJECT-IDEAS.de.md).
