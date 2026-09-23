🇩🇪 Deutsch | 🇬🇧 [English](../en/01-general-store.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1](../../01-basics/de/01-csharp-grundlagen.md) und [Kurs 2](../../02-cat-roster/de/01-katzenkartei.md)

# Kurs 9 – Gemischtwarenladen

**Ziel:** "du hast nicht genug Geld" zu einem echten, unmöglich zu
übersehenden Fehlerfall machen, statt zu einem Bug, der nur darauf
wartet zu passieren. Ein Kauf, der nicht klappen kann, stoppt entweder
das Programm mit nützlicher Information, oder wird absichtlich behandelt,
aber macht nie einfach still gar nichts. Am Ende weißt du, wie du deinen
eigenen Exception-Typ definierst und wann das besser ist, als einen zu
nehmen, den .NET schon mitbringt.

Wie in Kurs 3, 4, 6 und 8 steht die Kernübung unten in Schritten
beschrieben, die du selbst schreibst. Die fertige Version, inklusive der
optionalen Teile und der Herausforderung, liegt in [`code/`](../code/).

## 🟢 Kern — Was passiert, wenn eine Aktion einfach nicht klappen kann?

```csharp
public void Withdraw(int amount)
{
    Balance -= amount;
}
```

Nichts hindert `Balance` hier daran, negativ zu werden. Du könntest ein
`if` hinzufügen, das einfach... nichts tut, wenn nicht genug Geld da ist.
Aber dann hat aufrufender Code keine Möglichkeit zu wissen, dass die
Abhebung still fehlgeschlagen ist, und könnte selbstbewusst "Kauf
abgeschlossen" melden, obwohl nichts passiert ist. Stattdessen eine
**Exception** zu [`throw`](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/statements/exception-handling-statements#the-throw-statement)en
macht den Fehler unmöglich versehentlich zu ignorieren: Solange nichts sie
explizit fängt, stoppt das Programm genau dort, mit einer Nachricht, die
genau zeigt, was schiefgegangen ist.

## 🟢 Kern — Eine eigene Exception definieren

```csharp
public class InsufficientFundsException : Exception
{
    public int Requested { get; }
    public int Available { get; }

    public InsufficientFundsException(int requested, int available)
        : base($"Tried to spend {requested} coins, but only {available} are available.")
    {
        Requested = requested;
        Available = available;
    }
}
```

Auch `: Exception` ist wieder Vererbung, genau wie bei Kurs 4s
`Machine`-Subklassen, nur diesmal von einer Klasse geerbt, die .NET
mitbringt, statt von einer, die du selbst geschrieben hast. `: base(message)`
ruft [`Exception`](https://learn.microsoft.com/de-de/dotnet/api/system.exception)s
eigenen Konstruktor mit einer menschenlesbaren Nachricht auf (danach
verfügbar über `.Message`), und `Requested`/`Available` sind gewöhnliche
Properties, genau wie bei jeder Klasse, die du seit Kurs 2 geschrieben
hast. Nichts an `Exception` hindert dich daran, eigene Daten dranzuhängen.

## 🟢 Kern — Werfen und fangen

```csharp
try
{
    store.Purchase(account, "Sword", 50);
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine($"Purchase failed: {ex.Message}");
}
```

[`try`](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/statements/exception-handling-statements#the-try-statement)
umschließt Code, der eine Exception werfen könnte; `catch (SomeException ex)`
läuft nur, wenn genau dieser Exception-Typ (oder einer seiner abgeleiteten
Typen; auch Exceptions können Hierarchien bilden) innerhalb des
`try`-Blocks geworfen wird, wobei `ex` dir Zugriff auf alles darauf gibt,
inklusive deiner eigenen Properties. Code nach dem `catch`-Block läuft
normal weiter. Die Exception hat das Programm nicht zum Absturz gebracht,
sie wurde behandelt.

## 🟢 Kernübung — Schreib Withdraw selbst

Gegeben ist `Deposit`, das schon vor einem unsinnigen negativen Betrag
schützt:

```csharp
public void Deposit(int amount)
{
    if (amount < 0)
    {
        throw new ArgumentException("Amount cannot be negative.");
    }

    Balance += amount;
}
```

Schreib `Withdraw` mit **zwei** Prüfungen: derselbe Schutz vor einem
negativen Betrag wie bei `Deposit`, und eine Prüfung, ob `amount` das
aktuelle `Balance` übersteigt, wirf in dem Fall
`InsufficientFundsException`.

```csharp
public void Withdraw(int amount)
{
    // dein Code hier
}
```

Test es: Mehr abzuheben als der Kontostand sollte werfen und `Balance`
komplett unverändert lassen (die Subtraktion sollte nie laufen, wenn die
Prüfung zuerst wirft). Falls du feststeckst, hat
[`code/BankAccount.cs`](../code/BankAccount.cs) eine funktionierende
Version.

## 🟡 Optional — finally: Code, der immer läuft

```csharp
try
{
    store.Purchase(account, "Potion", 12);
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine($"Purchase failed: {ex.Message}");
}
finally
{
    Console.WriteLine("Transaction attempt finished.");
}
```

[`finally`](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/statements/exception-handling-statements#the-try-finally-statement)
läuft immer — egal ob der `try`-Block erfolgreich war, eine Exception
geworfen hat, die gefangen wurde, oder (seltener, aber gut zu wissen) eine,
die es nicht wurde. Das ist der richtige Ort für Aufräumarbeiten, die so
oder so passieren müssen.

## 🔴 Optional, echte Herausforderung — Mehr als einen Exception-Typ fangen

`ArgumentException` ist eine **eingebaute** .NET-Exception. Nicht jeder
Fehlerfall braucht eine brandneue eigene Exception-Klasse; greif zu einer,
die .NET schon mitbringt, wenn sie wirklich passt. Versuch, beide Arten
von Fehlschlag auszulösen und sie getrennt zu fangen:

```csharp
try
{
    account.Deposit(-10);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Deposit rejected: {ex.Message}");
}
catch (InsufficientFundsException ex)
{
    Console.WriteLine($"Purchase failed: {ex.Message}");
}
```

Mehrere `catch`-Blöcke werden von oben nach unten geprüft, genau wie
`if`/`else if`: der erste, dessen Typ zur geworfenen Exception passt (oder
ein Basistyp davon ist), läuft, der Rest wird übersprungen. Bestätige, dass
ein Vertauschen der beiden `catch`-Blöcke hier immer noch funktioniert
(diese beiden Exception-Typen sind nicht miteinander verwandt, die
Reihenfolge spielt also keine Rolle). Schau dann nach, was passieren
würde, wenn du stattdessen zuerst `catch (Exception ex)` hättest, vor den
spezifischeren Typen. [`code/Program.cs`](../code/Program.cs) hat eine
funktionierende Version des ganzen Kapitels.

## Was du gelernt hast

- Warum still zu scheitern (oder einen magischen "hat nicht geklappt"-Wert
  zurückzugeben) schlechter ist als eine Exception für einen echten
  Fehlerfall
- Eine eigene Exception definieren, indem man von `Exception` erbt, eigene
  Properties hinzufügt und eine Nachricht über `base(...)` baut
- `try`/`catch (SomeException ex)`, und dass Code nach einer behandelten
  Exception normal weiterläuft
- `finally`, für Code, der laufen muss, egal ob der `try`-Block erfolgreich
  war oder fehlgeschlagen ist
- Mehrere `catch`-Blöcke, von oben nach unten geprüft wie `if`/`else if`,
  und zu einem eingebauten Exception-Typ zu greifen statt immer einen
  eigenen zu erfinden

## Weiter

Kurs 9 steht für sich allein und braucht nur Kurs 1 und 2. Für den Rest der
Roadmap dieses Repositories siehe
[PROJECT-IDEAS.de.md](../../../PROJECT-IDEAS.de.md).
