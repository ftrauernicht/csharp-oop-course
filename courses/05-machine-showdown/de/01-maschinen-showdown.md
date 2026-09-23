🇩🇪 Deutsch | 🇬🇧 [English](../en/01-machine-showdown.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1](../../01-basics/de/01-csharp-grundlagen.md) und [Kurs 2](../../02-cat-roster/de/01-katzenkartei.md) — teilt sich eine Domäne mit [Kurs 4](../../04-machine-hunter/de/01-maschinenjaeger.md), setzt ihn aber nicht voraus

# Kurs 5 – Maschinen-Showdown

**Ziel:** jeden verschiedenen Maschinentyp in eine einzige Liste packen und
genau dieselbe Schleife über alle laufen lassen. Jede greift trotzdem auf
ihre eigene Art an, obwohl die Schleife selbst gar nicht weiß, welche
konkrete Maschine sie bei jedem Durchlauf gerade vor sich hat. Das ist
**Polymorphie**: die Auszahlung für alles, was Kurs 4 im Stillen
vorbereitet hat.

*(Falls du [Kurs 4](../../04-machine-hunter/de/01-maschinenjaeger.md)
gemacht hast, werden dir die Klassenformen unten sehr bekannt vorkommen.
Überflieg die Wiederholung und spring zu "Die Auszahlung".)*

## 🟢 Kern — Die Maschinen-Hierarchie neu aufbauen

Dieser Kurs steht für sich allein, deshalb hier eine kurze Version derselben
Basisklasse und zweier Maschinentypen aus Kurs 4:

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

public class Thunderjaw : Machine
{
    public Thunderjaw() : base("Thunderjaw", 200)
    {
    }

    public override void Attack()
    {
        LogAttack("fires a devastating chest cannon", 40);
    }
}
```

## 🟢 Kern — abstract: den Vertrag wasserdicht machen

Kurs 4 hat einen `protected`en Konstruktor benutzt, um eine reine `Machine`
zu *erschweren*, aber nichts hat eine abgeleitete Klasse daran gehindert,
`Attack()` einfach gar nicht zu überschreiben (genau das hat Kurs 4s
`Grazer` absichtlich gemacht, um zu zeigen, dass `virtual` optional ist).
Für eine Basisklasse, die *niemals* für sich allein existieren sollte, und
deren jede abgeleitete Klasse ihr eigenes Verhalten definieren *muss*, hat
C# ein stärkeres Werkzeug: **`abstract`**.

```csharp
public abstract class Machine
{
    // ... Name, MaxHealth, Health, der Konstruktor, LogAttack, TakeDamage,
    // IsDefeated: alles unverändert von oben ...

    public abstract void Attack();
}
```

Zwei Änderungen, beide tragend:

- `abstract class Machine` bedeutet, dass `new Machine(...)` *niemals*
  erlaubt ist, überall, aus keinem Grund, nicht nur erschwert wie bei
  `protected`. Probier es, und der Compiler sagt
  `error CS0144: Cannot create an instance of the abstract type or
  interface 'Machine'`.
- `public abstract void Attack();` hat **überhaupt keinen Rumpf**: nur
  eine Signatur und ein Semikolon. Jede nicht-abstrakte Klasse, die von
  `Machine` erbt, muss jetzt *zwingend* ein `Attack()`-Override liefern,
  sonst kompiliert sie auch nicht. Es gibt keine generische
  Rückfall-Nachricht mehr, und keine Möglichkeit, das versehentlich zu
  überspringen, wie es `Grazer` getan hat.

(Der Konstruktor bleibt `protected`, obwohl `abstract` allein die direkte
Instanziierung schon verhindert; das ist trotzdem die korrekte,
idiomatische Art, einen Konstruktor zu schreiben, der nur über das
`base(...)` einer abgeleiteten Klasse laufen soll.) Mehr:
[Microsoft Learn – Abstrakte Klassen](https://learn.microsoft.com/de-de/dotnet/csharp/fundamentals/object-oriented/inheritance#abstract-base-classes).

## 🟢 Kern — Die Auszahlung: eine Liste, viele Verhaltensweisen

```csharp
var machines = new List<Machine>
{
    new Watcher(),
    new Thunderjaw(),
};

foreach (var machine in machines)
{
    machine.Attack();
}
```

```
Watcher shrieks and lunges for 8 damage!
Thunderjaw fires a devastating chest cannon for 40 damage!
```

Schau dir dieses `foreach` genau an: Es sagt genau einmal
`machine.Attack()`, ohne ein `if`, das prüft, welche Art von Maschine es
gerade vor sich hat. Trotzdem geben `Watcher` und `Thunderjaw` jeweils
etwas völlig anderes aus. Die Liste ist als `List<Machine>` typisiert, zur
Kompilierzeit "weiß" die Schleife also nur, dass jedes Element *irgendeine*
`Machine` ist. Welches `Attack()` tatsächlich läuft, entscheidet sich erst
zur *Laufzeit*, anhand des echten Typs des Objekts. Das ist **Polymorphie**
("viele Formen"): ein Aufruf, `machine.Attack()`, nimmt je nachdem, worauf
er tatsächlich aufgerufen wird, eine andere Form an. Mehr:
[Microsoft Learn – Polymorphie](https://learn.microsoft.com/de-de/dotnet/csharp/fundamentals/object-oriented/polymorphism).

Auf Teile davon hast du dich technisch schon seit Kurs 4 verlassen:
`watcher.Attack()` lief immer `Watcher`s Version, nie `Machine`s
generische. Neu hier ist die praktische Superkraft: Die Schleife, und
jeder Code wie sie, muss sich nie ändern, wenn ein neuer Maschinentyp
dazukommt.

## 🟢 Kernübung — Eine dritte Maschine hinzufügen, sonst nichts ändern

Schreib eine `Strider`-Klasse (dieselben Werte wie in Kurs 4: Name
`"Strider"`, maximale Gesundheit `80`, `Attack()` loggt `"kicks"` mit `12`
Schaden):

```csharp
public class Strider : Machine
{
    // dein Code hier
}
```

Füg `new Strider()` zur `machines`-Liste hinzu. Führ es erneut aus. Die
`foreach`-Schleife oben braucht **null Änderungen**, um es korrekt zu
handhaben. Das ist der eigentliche Punkt dieser Übung: überprüf es selbst,
statt es einfach zu glauben. Falls du feststeckst, hat
[`code/Strider.cs`](../code/Strider.cs) eine funktionierende Version.

## 🟡 Optional — Ein kurzer Kampf

Nutz dieselbe Liste für etwas Spielhafteres:

```csharp
foreach (var machine in machines)
{
    machine.TakeDamage(50);
}

foreach (var machine in machines)
{
    Console.WriteLine($"{machine.Name} defeated: {machine.IsDefeated}");
}
```

`Watcher` (30 maximale Gesundheit) übersteht 50 Schaden nicht; `Thunderjaw`
und `Strider` schon. Dasselbe polymorphe Schleifenmuster, diesmal mit
echtem Einsatz.

## 🔴 Optional, echte Herausforderung — Wenn du den Typ doch kennen musst

Polymorphie handhabt "jede Maschine greift an" elegant, aber manchmal
brauchst du wirklich eine einzelne, bestimmte Art für etwas, das gar nicht
Teil des geteilten Vertrags ist, etwa eine Warnung speziell für die
größte Maschine. Der
[`is`](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/operators/type-testing-and-cast#the-is-operator)-Operator
prüft den tatsächlichen Laufzeit-Typ eines Objekts:

```csharp
foreach (var machine in machines)
{
    if (machine is Thunderjaw)
    {
        Console.WriteLine("Massive machine incoming!");
    }

    machine.Attack();
}
```

Füg das zu deiner Schleife hinzu und bestätige, dass die Warnung
ausschließlich für den `Thunderjaw` erscheint. Sparsam eingesetzt ist `is`
ein vertretbares Schlupfloch; für *alles* eingesetzt (`if (machine is
Watcher) ... else if (machine is Thunderjaw) ... else if ...`) hebelt es
den ganzen Sinn von Polymorphie aus. Diese Grenze lohnt sich zu bemerken.

## Was du gelernt hast

- `abstract class` und `abstract`e Methoden: eine Basisklasse, die nie
  instanziiert werden kann, mit Membern, die gar keine Standard-
  Implementierung haben, sodass jede konkrete abgeleitete Klasse ihre
  eigene liefern muss
- Polymorphie: dieselbe Methode über einen geteilten Basistyp aufrufen und
  den *tatsächlichen* Laufzeit-Typ entscheiden lassen, was wirklich läuft
- Warum eine `List<Machine>` (oder jede Sammlung eines geteilten
  Basis-/abstrakten Typs) mit neuen abgeleiteten Klassen mitwächst, ohne
  dass sich der Code, der die Liste nutzt, je ändern muss
- Der `is`-Operator für den seltenen Fall, dass du den konkreten Typ eines
  Objekts wirklich kennen musst, und warum, sich überall darauf zu
  verlassen, den Sinn von Polymorphie zunichtemacht

## Weiter

Kurs 5 steht für sich allein und braucht nur Kurs 1 und 2. Für den Rest der
Roadmap dieses Repositories siehe
[PROJECT-IDEAS.de.md](../../../PROJECT-IDEAS.de.md).
