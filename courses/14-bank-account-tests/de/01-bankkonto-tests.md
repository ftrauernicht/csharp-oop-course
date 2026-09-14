🇩🇪 Deutsch | 🇬🇧 [English](../en/01-bank-account-tests.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Setzt voraus: [Kurs 1](../../01-basics/de/01-csharp-grundlagen.md) und [Kurs 2](../../02-cat-roster/de/01-katzenkartei.md)

# Kurs 14 – Bankkonto-Tests

**Ziel:** beweisen, dass die Validierungs- und Exception-Logik einer
Klasse tatsächlich funktioniert — automatisch, jedes Mal, ohne dass du
`Console.WriteLine`-Ausgaben selbst mit den Augen nachprüfst. Am Ende
weißt du, wie ein Testprojekt aufgebaut ist, anders als jeder Kurs vorher,
und wie man echte xUnit-Tests schreibt und ausführt.

Dieser Kurs baut absichtlich eine kleine Version von Kurs 9s `BankAccount`
eigenständig neu auf — dieselbe Teilungs-Vereinbarung, die Kurs 4 und 5
schon nutzen, hier nur angewandt auf "das, was getestet wird" statt "die
Domäne, die erweitert wird". Wie in Kurs 3, 4, 6, 8, 9, 10, 11, 12 und 13
steht die Kernübung unten in Schritten beschrieben, die du selbst
schreibst. Die fertige Version, inklusive der optionalen Teile und der
Herausforderung, liegt in [`code/`](../code/).

## 🟢 Kern — Warum dieser Kurs zwei Projekte hat

Jeder Kurs vor diesem war eine einzelne Konsolen-App, die du mit
`dotnet run` ausführen konntest. Tests sind anders: Sie müssen *gegen*
deinen Code laufen, nicht selbstständig laufen und Dinge ausgeben, die du
mit dem Auge prüfst. Der [`code/`](../code/)-Ordner dieses Kurses enthält
**zwei** Projekte statt eines:

- `BankAccount/` — eine **Class Library** (`dotnet new classlib`), nur
  `BankAccount.cs` und `InsufficientFundsException.cs`, gar kein
  `Program.cs` — eine Class Library hat keinen Einstiegspunkt, weil nichts
  sie direkt ausführt.
- `BankAccount.Tests/` — ein **xUnit-Testprojekt** (`dotnet new xunit`),
  das auf die Class Library verweist
  (`dotnet add reference ../BankAccount/BankAccount.csproj`), damit seine
  Tests `BankAccount` und `InsufficientFundsException` überhaupt sehen
  können.

Du musst dir diese beiden `dotnet new`/`dotnet add`-Befehle jetzt nicht
merken — sie laufen nur einmal, wenn ein Testprojekt zuerst aufgesetzt
wird; von hier an passiert alles innerhalb der Dateien, die sie angelegt
haben.

## 🟢 Kern — Dein erster Test

```csharp
public class BankAccountTests
{
    [Fact]
    public void Deposit_IncreasesBalance()
    {
        // Arrange
        var account = new BankAccount(100);

        // Act
        account.Deposit(50);

        // Assert
        Assert.Equal(150, account.Balance);
    }
}
```

[`[Fact]`](https://learn.microsoft.com/de-de/dotnet/core/testing/unit-testing-with-dotnet-test)
markiert eine Methode als Test, den xUnit tatsächlich ausführt — ohne das
wäre das nur eine gewöhnliche, nie aufgerufene Methode.
[`Assert.Equal(expected, actual)`](https://learn.microsoft.com/de-de/dotnet/api/xunit.assert.equal)
lässt den Test fehlschlagen (mit einer klaren Nachricht, die beide Werte
zeigt), falls sie nicht übereinstimmen. Die drei Kommentare — **Arrange**
(aufsetzen, was du brauchst), **Act** (die eine Sache tun, die du testest),
**Assert** (das Ergebnis prüfen) — sind eine Konvention, die sich lohnt
beizubehalten, selbst wenn du irgendwann aufhörst, die Kommentare selbst zu
schreiben; sie verhindert, dass die drei getrennten Aufgaben eines Tests
ineinander verschwimmen.

Führ alle Tests eines Projekts aus einem Terminal aus, innerhalb von
`BankAccount.Tests/`:

```
dotnet test
```

## 🟢 Kernübung — Testen, dass eine Exception tatsächlich geworfen wird

```csharp
[Fact]
public void Withdraw_ThrowsInsufficientFundsException_WhenBalanceTooLow()
{
    // dein Code hier: Arrange ein BankAccount mit kleinem Kontostand, dann
    // Assert, dass Withdraw mit zu viel InsufficientFundsException wirft
}
```

[`Assert.Throws<TException>(() => ...)`](https://learn.microsoft.com/de-de/dotnet/api/xunit.assert.throws)
nimmt eine Lambda (Kurs 11s `=>`-Syntax, hier für etwas anderes als LINQ
benutzt) mit der einen Zeile, die werfen sollte, und lässt den Test
fehlschlagen, falls sie es *nicht* tut — das Gegenteil von `try`/`catch`
aus Kurs 9, das auf eine Exception reagiert. Ein Test macht stattdessen das
Werfen der Exception selbst zu dem, was geprüft wird. Führ `dotnet test`
aus und bestätige, dass er besteht. Falls du feststeckst, hat
[`code/BankAccount.Tests/BankAccountTests.cs`](../code/BankAccount.Tests/BankAccountTests.cs)
eine funktionierende Version.

## 🟡 Optional — Mehr testen als nur "hat es geworfen"

```csharp
[Fact]
public void Withdraw_LeavesBalanceUnchanged_WhenItThrows()
{
    var account = new BankAccount(20);

    try
    {
        account.Withdraw(50);
    }
    catch (InsufficientFundsException)
    {
    }

    Assert.Equal(20, account.Balance);
}
```

Zu bestätigen, dass eine Exception geworfen wird, ist nicht die ganze
Geschichte — Kurs 9s `Withdraw` ist nur korrekt, wenn `Balance` *auch*
noch exakt das ist, was es vorher war, nicht teilweise geändert. Dieser
Test fängt die Exception absichtlich (ein leeres `catch` ist normalerweise
ein Warnsignal, aber hier ist es Absicht: Der ganze Sinn dieses Tests ist,
was *nach* dem Wurf passiert) und prüft dann den Zustand direkt.

## 🔴 Optional, echte Herausforderung — Ein Test, mehrere Eingaben

```csharp
[Theory]
[InlineData(-1)]
[InlineData(-100)]
public void Deposit_ThrowsArgumentException_ForNegativeAmounts(int amount)
{
    var account = new BankAccount(100);

    Assert.Throws<ArgumentException>(() => account.Deposit(amount));
}
```

[`[Theory]`](https://learn.microsoft.com/de-de/dotnet/core/testing/unit-testing-with-dotnet-test)
mit einem oder mehreren `[InlineData(...)]` führt *dieselbe* Testmethode
einmal pro Datenzeile aus, wobei `amount` reihum an jeden Wert gebunden
wird — zwei tatsächliche Testläufe aus einer Methode, statt den ganzen
Test für `-1` und `-100` zweimal zu kopieren. Füg ein drittes eigenes
`[InlineData(...)]` hinzu und bestätige, dass `dotnet test` jetzt einen
bestandenen Test mehr meldet als vorher.
[`code/BankAccount.Tests/BankAccountTests.cs`](../code/BankAccount.Tests/BankAccountTests.cs)
hat eine funktionierende Version des ganzen Kapitels.

## Was du gelernt hast

- Warum ein Testprojekt anders aufgebaut ist: eine Class Library ohne
  Einstiegspunkt, plus ein getrenntes Testprojekt, das darauf verweist
- `[Fact]`, `Assert.Equal`, und die Arrange-Act-Assert-Form eines Tests
- `Assert.Throws<TException>(() => ...)`, um "das hier wirft" selbst zu
  dem zu machen, was getestet wird
- Zustand nach einer Exception testen, nicht nur, ob eine geworfen wurde
- `[Theory]` + `[InlineData]`, um eine Testmethode gegen mehrere Eingaben
  laufen zu lassen, statt sie zu duplizieren

## Weiter

Kurs 14 steht für sich allein und braucht nur Kurs 1 und 2. Für den Rest
der Roadmap dieses Repositories siehe
[PROJECT-IDEAS.de.md](../../../PROJECT-IDEAS.de.md).
