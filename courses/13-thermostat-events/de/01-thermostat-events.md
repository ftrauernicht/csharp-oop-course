🇩🇪 Deutsch | 🇬🇧 [English](../en/01-thermostat-events.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1](../../01-basics/de/01-csharp-grundlagen.md) und [Kurs 2](../../02-cat-roster/de/01-katzenkartei.md)

# Kurs 13 – Thermostat-Events

**Ziel:** ein `Thermostat`, der seine eigenen Temperaturänderungen an alle
meldet, die es wissen wollen (ein `Display`, ein `Logger`, vielleicht
später mehr), ohne dass `Thermostat` je wissen muss, dass einer von ihnen
überhaupt existiert. Am Ende weißt du, was ein C#-`event` wirklich ist,
und warum sich diese Art der Entkopplung den zusätzlichen Aufwand lohnt.

Wie in Kurs 3, 4, 6, 8, 9, 10, 11 und 12 steht die Kernübung unten in
Schritten beschrieben, die du selbst schreibst. Die fertige Version,
inklusive der optionalen Teile und der Herausforderung, liegt in
[`code/`](../code/).

## 🟢 Kern — Die eng gekoppelte Version

```csharp
public class TightlyCoupledThermostat
{
    private readonly Display _display;
    private int _temperature;

    public TightlyCoupledThermostat(Display display)
    {
        _display = display;
    }

    public int Temperature
    {
        get { return _temperature; }
        set
        {
            _temperature = value;
            _display.Show(_temperature); // Thermostat muss Display kennen
        }
    }
}
```

Das funktioniert, aber `Thermostat` hat jetzt eine feste Abhängigkeit
speziell zu `Display`. Soll auch ein `Logger` reagieren? `Thermostat`
nochmal bearbeiten, ein weiteres Feld, ein weiterer Aufruf. Jede neue Art
von Abonnent bedeutet, zurückzugehen und die Klasse zu ändern, die
abonniert wird. Das ist genau umgekehrt zu Kurs 5s Polymorphie-Lektion, wo
ein neuer `Machine`-Typ null Änderungen am Code brauchte, der ihn benutzt.

## 🟢 Kern — Ein Event deklarieren

```csharp
public class TemperatureChangedEventArgs : EventArgs
{
    public int NewTemperature { get; }

    public TemperatureChangedEventArgs(int newTemperature)
    {
        NewTemperature = newTemperature;
    }
}
```

```csharp
public class Thermostat
{
    public event EventHandler<TemperatureChangedEventArgs>? TemperatureChanged;

    // ...
}
```

`TemperatureChangedEventArgs` bündelt alles, was über die Änderung wissenswert
ist (hier nur die neue Temperatur), dieselbe Idee wie Kurs 9s eigene
Exceptions, die ihre eigenen Daten tragen, nur für einen anderen Zweck.
`EventHandler<TEventArgs>` ist ein in .NET eingebauter **Delegate**: ein
Typ, der "eine Methode mit dieser bestimmten Form" repräsentiert (hier:
nimmt einen Sender und eine `TemperatureChangedEventArgs`, gibt nichts
zurück). Das Schlüsselwort `event` macht `TemperatureChanged` zu etwas,
das andere Klassen abonnieren (`+=`) oder abbestellen können (`-=`), aber
nie direkt aufrufen oder komplett ersetzen. Das kann nur `Thermostat`
selbst. Das `?` macht es nullable: Hat noch niemand abonniert, ist es
`null`. Mehr:
[Microsoft Learn – Events](https://learn.microsoft.com/de-de/dotnet/csharp/events-overview).

## 🟢 Kernübung — Das Event selbst auslösen

```csharp
public int Temperature
{
    get { return _temperature; }
    set
    {
        _temperature = value;
        // dein Code hier: löse TemperatureChanged aus, mit `this` als
        // Sender und einer neuen TemperatureChangedEventArgs mit dem
        // neuen Wert -- aber nur, wenn tatsächlich jemand abonniert hat
    }
}
```

`TemperatureChanged?.Invoke(sender, args)` ist das Muster: `?.` (Kurs 6s
Null-conditional-verwandter Operator, hier auf einem Delegate) bedeutet
"ruf `Invoke` nur auf, falls `TemperatureChanged` nicht `null` ist." Das
überspringt es sicher, wenn niemand abonniert hat, statt eine
`NullReferenceException` zu werfen. Schreib den ganzen `set`-Block. Falls
du feststeckst, hat [`code/Thermostat.cs`](../code/Thermostat.cs) eine
funktionierende Version.

## 🟢 Kern — Abonnieren

```csharp
public class Display
{
    public void ShowTemperature(object? sender, TemperatureChangedEventArgs e)
    {
        Console.WriteLine($"Display: it's now {e.NewTemperature} degrees.");
    }
}
```

```csharp
var thermostat = new Thermostat();
var display = new Display();

thermostat.TemperatureChanged += display.ShowTemperature;

thermostat.Temperature = 20; // Display: it's now 20 degrees.
```

`ShowTemperature`s Signatur, `(object? sender, TemperatureChangedEventArgs e)`,
muss exakt zu `EventHandler<TemperatureChangedEventArgs>` passen, genau
das lässt `+=` sie überhaupt akzeptieren. `Thermostat` hat nie `Display`
importiert, nie `ShowTemperature` beim Namen aufgerufen und hatte keine
Ahnung, dass je ein `Display` existieren würde. `Display` hat die ganze
Arbeit erledigt, sich selbst zu verbinden.

Füg einen zweiten, unabhängigen Abonnenten hinzu, ohne `Thermostat` zu
ändern:

```csharp
var logger = new Logger();
thermostat.TemperatureChanged += logger.LogChange;

thermostat.Temperature = 25; // Display und Logger reagieren beide
```

## 🟡 Optional — Abbestellen

```csharp
thermostat.TemperatureChanged -= logger.LogChange;

thermostat.Temperature = 30; // nur Display reagiert jetzt noch
```

`-=` entfernt genau die Methode, die vorher mit `+=` hinzugefügt wurde.
Das ist nützlich für etwas, das nur vorübergehend zuhören soll (ein
Bildschirm, der gerade sichtbar ist, eine Verbindung, die sich schließen
könnte).

## 🔴 Optional, echte Herausforderung — Ein dritter Abonnent

Schreib eine `Alarm`-Klasse, die nur etwas ausgibt, wenn die Temperatur
über einen Schwellenwert steigt, den sie in ihrem Konstruktor bekommt:

```csharp
public class Alarm
{
    // dein Code hier: ein Konstruktor, der einen Schwellenwert nimmt, und
    // eine Methode, die zur Form von
    // EventHandler<TemperatureChangedEventArgs> passt und nur eine
    // Warnung ausgibt, wenn e.NewTemperature über dem Schwellenwert liegt
}
```

Abonnier sie genauso wie `Display` und `Logger`, und bestätige, dass
`Thermostat.cs` **null** Änderungen gebraucht hat, um eine dritte, völlig
andere Art von Abonnent zu unterstützen. [`code/Alarm.cs`](../code/Alarm.cs)
hat eine funktionierende Version.

## Was du gelernt hast

- Warum eine Klasse, die ihre Abonnenten direkt beim Namen aufruft, eine
  feste Abhängigkeit erzeugt, die mit jedem neuen Abonnenten wächst
- `event`, `EventHandler<TEventArgs>`, und eine eigene `EventArgs`-
  Subklasse, um Daten über die Änderung zu tragen
- Ein Event sicher mit `?.Invoke(...)` auslösen, es überspringen, wenn
  niemand abonniert hat
- Abonnieren (`+=`) und Abbestellen (`-=`), und dass die Methode eines
  Abonnenten exakt zur Delegate-Signatur des Events passen muss
- Dass ein neuer Abonnenten-Typ null Änderungen an der Klasse braucht, die
  das Event auslöst: dieselbe "erweitern ohne zu ändern"-Auszahlung, die
  Kurs 5 für Polymorphie gezeigt hat, hier für entkoppelte Kommunikation

## Weiter

Kurs 13 steht für sich allein und braucht nur Kurs 1 und 2. Für den Rest
der Roadmap dieses Repositories siehe
[PROJECT-IDEAS.de.md](../../../PROJECT-IDEAS.de.md).
