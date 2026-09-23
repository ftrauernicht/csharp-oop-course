🇬🇧 English | 🇩🇪 [Deutsch](../de/01-thermostat-events.md)

[← Back to course overview](../../../README.md) · Assumes: [Course 1](../../01-basics/en/01-csharp-basics.md) and [Course 2](../../02-cat-roster/en/01-cat-roster.md)

# Course 13 – Thermostat Events

**Goal:** a `Thermostat` that announces its own temperature changes to
whoever wants to know (a `Display`, a `Logger`, maybe more later), without
`Thermostat` ever having to know any of them exist. By the end, you'll know
what a C# `event` actually is, and why this kind of decoupling is worth
the extra ceremony.

As in Courses 3, 4, 6, 8, 9, 10, 11, and 12, the core exercise below is
described in steps for you to write yourself. The finished version,
including the optional and challenge parts, lives in [`code/`](../code/).

## 🟢 Core — The tightly-coupled version

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
            _display.Show(_temperature); // Thermostat has to know Display exists
        }
    }
}
```

This works, but `Thermostat` now has a hard dependency on `Display`
specifically. Want a `Logger` to react too? Edit `Thermostat` again, add
another field, another call. Every new kind of subscriber means going back
and changing the class being subscribed to. That's exactly backwards from
Course 5's polymorphism lesson, where adding a new `Machine` type needed
zero changes to the code using it.

## 🟢 Core — Declaring an event

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

`TemperatureChangedEventArgs` bundles up everything worth knowing about
the change (here, just the new temperature), the same idea as Course 9's
custom exceptions carrying their own data, applied to a different purpose.
`EventHandler<TEventArgs>` is a **delegate** built into .NET: a type that
represents "a method with this specific shape" (here: takes a sender and a
`TemperatureChangedEventArgs`, returns nothing). The `event` keyword turns
`TemperatureChanged` into something other classes can subscribe to (`+=`)
or unsubscribe from (`-=`), but never call directly or replace outright.
Only `Thermostat` itself can do that. The `?` makes it nullable: if nobody
has subscribed yet, it's `null`. More:
[Microsoft Learn – Events](https://learn.microsoft.com/en-us/dotnet/csharp/events-overview).

## 🟢 Core exercise — Raise the event yourself

```csharp
public int Temperature
{
    get { return _temperature; }
    set
    {
        _temperature = value;
        // your code here: raise TemperatureChanged, passing `this` as the
        // sender and a new TemperatureChangedEventArgs with the new value
        // -- but only if someone's actually subscribed
    }
}
```

`TemperatureChanged?.Invoke(sender, args)` is the pattern: `?.` (Course
6's null-conditional-adjacent operator, here on a delegate) means "only
call `Invoke` if `TemperatureChanged` isn't `null`." That skips it safely
when nobody's subscribed, instead of throwing a
`NullReferenceException`. Write the full `set` block. If you get stuck,
[`code/Thermostat.cs`](../code/Thermostat.cs) has a working version.

## 🟢 Core — Subscribing

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

`ShowTemperature`'s signature, `(object? sender, TemperatureChangedEventArgs e)`,
has to match `EventHandler<TemperatureChangedEventArgs>` exactly, which
is what lets `+=` accept it. `Thermostat` never imported `Display`,
never called `ShowTemperature` by name, and had no idea a `Display` would
ever exist. `Display` did all the work of connecting itself.

Add a second, independent subscriber, with zero changes to `Thermostat`:

```csharp
var logger = new Logger();
thermostat.TemperatureChanged += logger.LogChange;

thermostat.Temperature = 25; // both Display and Logger react
```

## 🟡 Optional — Unsubscribing

```csharp
thermostat.TemperatureChanged -= logger.LogChange;

thermostat.Temperature = 30; // only Display reacts now
```

`-=` removes exactly the method that was added with `+=` earlier. That's
useful for something that should only listen temporarily (a screen that's
currently visible, a connection that might close).

## 🔴 Optional, genuine challenge — A third subscriber

Write an `Alarm` class that only prints something when the temperature
goes above a threshold it's given in its constructor:

```csharp
public class Alarm
{
    // your code here: a constructor taking a threshold, and a method
    // matching EventHandler<TemperatureChangedEventArgs>'s shape that
    // only prints a warning if e.NewTemperature is above the threshold
}
```

Subscribe it the same way as `Display` and `Logger`, and confirm
`Thermostat.cs` needed **zero** changes to support a third, entirely
different kind of subscriber. [`code/Alarm.cs`](../code/Alarm.cs) has a
working version.

## What you learned

- Why a class calling its subscribers by name directly creates a hard
  dependency that grows with every new subscriber
- `event`, `EventHandler<TEventArgs>`, and a custom `EventArgs` subclass
  for carrying data about what changed
- Raising an event safely with `?.Invoke(...)`, skipping it when nobody's
  subscribed
- Subscribing (`+=`) and unsubscribing (`-=`), and that a subscriber's
  method must match the event's delegate signature exactly
- That adding a new subscriber type needs zero changes to the class
  raising the event: the same "extend without modifying" payoff Course 5
  showed for polymorphism, here for decoupled communication instead

## Next

Course 13 stands on its own, needing only Courses 1 and 2. For the rest of
this repository's roadmap, see
[PROJECT-IDEAS.md](../../../PROJECT-IDEAS.md).
