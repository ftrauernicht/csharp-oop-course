🇩🇪 Deutsch | 🇬🇧 [English](../en/01-csharp-basics.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Zurück: [Kapitel 0 – Einführung & Werkzeuge](00-einfuehrung.md)

# Kapitel 1 – C#-Grundlagen

**Ziel:** die kleine Menge an Grundbausteinen, aus denen jedes C#-Programm
besteht — Werte, Variablen, Operatoren, Methoden, Entscheidungen und
Wiederholung. Kapitel 0 hat gezeigt, *wie* du ein Projekt anlegst und
ausführst; in diesem Kapitel geht es darum, *was* du in `Program.cs`
überhaupt schreiben darfst. Nichts hiervon ist Wegwerfwissen: Kurs 2 und
alles danach benutzt jeden einzelnen dieser Bausteine.

Lass das Projekt aus [Kapitel 0](00-einfuehrung.md) geöffnet und tipp jedes
Beispiel direkt in `Program.cs` ein, während du liest. Ersetz die
`Hello, World!`-Zeile und füg einfach darunter weiter hinzu. Eine fertige
Version dieses ganzen Kapitels, inklusive der Herausforderung am Ende, liegt
in [`code/Program.cs`](../code/Program.cs), falls du vergleichen willst.

## 🟢 Kern — Mit der Konsole sprechen

Zwei Methoden, die du ständig benutzen wirst:

```csharp
Console.WriteLine("What's your name?");
var name = Console.ReadLine();

Console.WriteLine($"Hello, {name}!");
```

[`Console.WriteLine`](https://learn.microsoft.com/de-de/dotnet/api/system.console.writeline)
gibt eine Textzeile aus.
[`Console.ReadLine`](https://learn.microsoft.com/de-de/dotnet/api/system.console.readline)
pausiert dein Programm und wartet, bis jemand etwas eintippt und Enter
drückt, und gibt dir zurück, was eingetippt wurde.

Dieses `$"Hello, {name}!"` ist **String-Interpolation**: Ein `$` vor den
Anführungszeichen erlaubt es dir, eine Variable direkt in den Text
einzusetzen, per `{}`, statt Textstücke von Hand zusammenzukleben. Das
benutzt du ab jetzt in fast jeder Ausgabezeile. Mehr dazu:
[Microsoft Learn – String-Interpolation](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/tokens/interpolated).

Führ das aus (<kbd>F5</kbd>, oder `dotnet run` im Terminal) und tipp
tatsächlich einen Namen ein, wenn danach gefragt wird. Dieser Kurs klickt
erst richtig, wenn du deinen eigenen Code ausführst, nicht nur liest.

(Vielleicht unterstreicht Visual Studio `name` mit einer Warnung zu einer
"möglichen Null-Referenz": `Console.ReadLine()` kann theoretisch auch gar
nichts zurückgeben, falls der Eingabestrom unerwartet schließt. Hier
harmlos, darüber musst du dir noch keine Gedanken machen.)

## 🟢 Kern — Werte und ihre Typen

Ein **Wert** ist ein einzelnes Stück Daten. Vier Arten, die in fast jedem
Programm auftauchen:

```csharp
int age = 16;          // eine Ganzzahl
double price = 4.5;    // eine Zahl mit Nachkommastellen
bool isStudent = true;  // ein Wahrheitswert — nur true oder false
string city = "Berlin"; // Text
```

Hier liegt der größte Unterschied zu einer Sprache wie JavaScript: C# ist
**statisch typisiert**. Sobald `age` als `int` deklariert ist, kann es nur
noch eine Ganzzahl enthalten: `age = "sixteen";` läuft nicht etwa mit
einem seltsamen Ergebnis, sondern lässt sich schlicht nicht kompilieren.
Das ist der Compiler aus Kapitel 0, der hier wieder am Werk ist: eine ganze
Klasse von Fehlern (die Verwechslung, welche Art von Daten man gerade in
der Hand hat) wird abgefangen, bevor dein Programm überhaupt startet. Mehr:
[Microsoft Learn – Eingebaute Typen](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/builtin-types/built-in-types).

`var` kennst du schon aus dem Konsolen-Beispiel oben: Es bedeutet nicht
"irgendein Typ", sondern "leite den Typ aus dem her, was ich gerade
zuweise, und leg ihn fest". `var name = Console.ReadLine();` ist genauso
statisch typisiert wie den Typ selbst auszuschreiben; es ist nur kürzer zu
tippen, wenn der Typ aus der rechten Seite offensichtlich ist. Beide Stile
sind in diesem Kurs in Ordnung; jetzt weißt du, warum sie gleichwertig sind.
Mehr: [Microsoft Learn – Implizit typisierte lokale Variablen](https://learn.microsoft.com/de-de/dotnet/csharp/programming-guide/classes-and-structs/implicitly-typed-local-variables).

## 🟢 Kern — Operatoren

Die arithmetischen kennst du schon
(`+` `-` `*` `/` `%`, siehe
[Microsoft Learn – Arithmetische Operatoren](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/operators/arithmetic-operators)).
Drei weitere Familien sind genauso wichtig:

**Zuweisung**: `=` speichert einen Wert; die anderen sind Kurzform für
"nimm den aktuellen Wert, mach etwas damit, speicher ihn zurück":

```csharp
int score = 10;
score += 5; // dasselbe wie: score = score + 5;  → 15
score *= 2; // dasselbe wie: score = score * 2;  → 30
```

**Vergleich**: Eine Ja/Nein-Frage über zwei Werte stellen:

```csharp
5 == 5   // true
5 != 3   // true (ungleich)
5 < 10   // true
5 >= 5   // true
```

Anders als in manchen Sprachen gibt es hier keine Falle, vor der man
gewarnt werden müsste: Weil C# statisch typisiert ist, kompiliert
`5 == "5"` gar nicht erst. Eine Zahl und ein String können nie versehentlich
als gleich behandelt werden. Ein einziger Gleichheitsoperator, und der ist
immer sicher. Mehr:
[Microsoft Learn – Gleichheitsoperatoren](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/operators/equality-operators).

**Logisch**: Wahrheitswerte verknüpfen oder umkehren:

```csharp
true && false // false ("und" — beide Seiten müssen true sein)
true || false // true  ("oder" — mindestens eine Seite muss true sein)
!true         // false ("nicht" — kehrt es um)
```

**Ternär (bedingt)**: Ein kompaktes, einzeiliges if/else, das statt eines
Blocks einen *Wert* liefert:

```csharp
int age = 16;
string label = age >= 18 ? "adult" : "minor";
// label ist "adult", wenn age >= 18, sonst "minor"
```

Lies `Bedingung ? WertWennWahr : WertWennFalsch` von links nach rechts. Am
nützlichsten für kurze Entweder-oder-Entscheidungen wie diese; für alles
Längere liest sich ein vollständiges `if`/`else` (unten) klarer. Mehr:
[Microsoft Learn – Bedingter Operator](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/operators/conditional-operator).

## 🟢 Kern — Runde und geschweifte Klammern

**Runde Klammern `()`** haben zwei Aufgaben. Gruppierung, um die
Auswertungsreihenfolge wie in der Mathematik zu steuern:

```csharp
(2 + 3) * 4 // 20, nicht 14
```

...und eine Methode aufrufen, oder auflisten, was sie entgegennimmt:

```csharp
Console.WriteLine("hi");              // WriteLine mit einem Wert aufrufen
int Add(int a, int b) { ... }         // a und b stehen in runden Klammern
```

**Geschweifte Klammern `{}`** markieren einen **Block**: Eine Gruppe von
Anweisungen, die als eine Einheit zusammengefasst laufen. Du siehst sie um
den Körper einer Methode, eines `if` und einer Schleife (alle noch in
diesem Kapitel). Eckige Klammern `[]` braucht dieser Kurs noch nicht. Die
tauchen auf, sobald Sammlungen von Werten nützlich werden, in einem späteren
Kurs.

## 🟢 Kern — Methoden: einem Verhalten einen Namen geben

```csharp
int Add(int a, int b)
{
    return a + b;
}

Console.WriteLine(Add(3, 4)); // 7
```

`a` und `b` sind **Parameter**: Platzhalter für die Werte, die beim Aufruf
der Methode übergeben werden, jeweils mit eigenem deklarierten Typ. Das
`int` vor `Add` ist der **Rückgabetyp**: der Typ des Werts, den `return` an
den Aufrufer zurückgibt. Eine Methode, die nur etwas ausgeben soll und
nichts zurückliefert, benutzt stattdessen `void`:

```csharp
void Greet(string personName)
{
    Console.WriteLine($"Hi, {personName}!");
}

Greet("Alex"); // gibt aus: Hi, Alex!
```

Richtig aufbauen wirst du solche Methoden, als Teil einer echten Klasse, ab
Kurs 2. Für jetzt reicht es, nur die Form zu kennen, damit später nichts
unbekannt aussieht. Mehr:
[Microsoft Learn – Methoden](https://learn.microsoft.com/de-de/dotnet/csharp/methods).

## 🟢 Kern — Entscheidungen treffen: if / else

```csharp
int temperature = 8;

if (temperature < 10)
{
    Console.WriteLine("Wear a jacket.");
}
else if (temperature < 20)
{
    Console.WriteLine("A light sweater will do.");
}
else
{
    Console.WriteLine("Shorts weather!");
}
```

C# prüft die Bedingungen von oben nach unten und führt den Block der ersten
`true`-Bedingung aus; der Rest wird komplett übersprungen. `else` (und
`else if`) sind beide optional. Mehr:
[Microsoft Learn – if-Anweisung](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/statements/selection-statements#the-if-statement).

## 🟢 Kern — Sich wiederholen: Schleifen

Eine **Schleife** führt denselben Codeblock mehrmals aus. Die
[`for`](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/statements/iteration-statements#the-for-statement)-Schleife
ist das Arbeitspferd, wenn du ungefähr weißt, wie oft du etwas wiederholen
willst:

```csharp
for (int i = 1; i <= 5; i++)
{
    Console.WriteLine(i);
}
// 1
// 2
// 3
// 4
// 5
```

Ihre drei Teile, getrennt durch Semikolons: **Start** (`int i = 1`: läuft
einmal, bevor irgendetwas anderes passiert), **Bedingung** (`i <= 5`: wird
vor jedem Durchlauf geprüft; die Schleife stoppt, sobald das `false` ist)
und **Schritt** (`i++`: läuft nach jedem Durchlauf). `i++` ist Kurzform für
`i = i + 1`.

[`while`](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/statements/iteration-statements#the-while-statement)
ist die einfachere Schwester, für den Fall, dass du die genaue Anzahl vorher
nicht kennst; sie läuft einfach weiter, solange ihre Bedingung wahr bleibt:

```csharp
int count = 0;
while (count < 3)
{
    Console.WriteLine("Hello!");
    count++;
}
```

**Probier es selbst:** Schreib eine `for`-Schleife, die die Zahlen 1 bis 10
ausgibt. Schreib dann eine, die nur die geraden Zahlen ausgibt, mit `%`, um
zu prüfen "ist diese Zahl durch 2 teilbar?".

## 🟡 Optional — Kommentare

```csharp
// ein einzeiliger Kommentar
/* ein
   mehrzeiliger
   Kommentar */
```

Der Compiler ignoriert Kommentare komplett: Sie sind Notizen für Menschen,
die den Code lesen, inklusive dein zukünftiges Ich. Guter Stil (und die
eigene Regel dieses Kurses) ist, das *Warum* zu kommentieren, nicht das
*Was*: Wenn der Code schon sagt, was er tut, fügt ein Kommentar, der das
wiederholt, nur Rauschen hinzu.

## 🟡 Optional — const, und wann man danach greift

```csharp
const double TaxRate = 0.19;
```

[`const`](https://learn.microsoft.com/de-de/dotnet/csharp/language-reference/keywords/const)
deklariert einen Wert, der zur Kompilierzeit feststeht und nie neu
zugewiesen werden kann, nützlich für Werte, die für die gesamte
Programmlaufzeit wirklich konstant sind (ein Steuersatz, die Anzahl der Tage
einer Woche), im Gegensatz zu einer gewöhnlichen Variable, die *in diesem
einen Durchlauf* nur zufällig nicht neu zugewiesen wird.

## 🔴 Optional, echte Herausforderung — FizzBuzz

Eine kleine, berühmte Übung, die Schleifen, Bedingungen und den
Modulo-Operator (`%`) gemeinsam einsetzt. Ein guter Abschluss, bevor es
weitergeht:

> Durchlauf die Zahlen 1 bis 15. Gib für jede `"Fizz"` aus, wenn sie durch 3
> teilbar ist, `"Buzz"`, wenn sie durch 5 teilbar ist, `"FizzBuzz"`, wenn sie
> durch beides teilbar ist, sonst die Zahl selbst.

Probier es selbst, bevor du in [`code/Program.cs`](../code/Program.cs)
reinschaust: Du hast alles, was du aus diesem Kapitel brauchst (eine
`for`-Schleife, `if`/`else if`/`else` und `%`). Ein Tipp, falls du ihn
willst: prüf "durch beides teilbar" *bevor* du eines der beiden einzeln
prüfst, sonst kommt der speziellere Fall nie zum Zug.

## Was du gelernt hast

- Von der Konsole lesen und in die Konsole schreiben (`Console.WriteLine`,
  `Console.ReadLine`), und String-Interpolation (`$"..."`)
- Werte und ihre Typen (`int`, `double`, `bool`, `string`), statische
  Typisierung, und `var`
- Operatoren: arithmetisch, Zuweisung, Vergleich, logisch, ternär
- Runde Klammern `()` zum Gruppieren/Aufrufen, geschweifte Klammern `{}` für
  Blöcke
- Methoden, ihre Form (Parameter, Rückgabetyp, `void`)
- Bedingungen (`if`/`else if`/`else`)
- Schleifen (`for`, `while`)
- *(optional)* Kommentare, `const`

## Weiter

Das war das letzte Kapitel von Kurs 1. Weiter geht's mit
[Kurs 2 – Katzenkartei](../../02-cat-roster/de/01-katzenkartei.md), der
unmittelbar hier anknüpft: Du nimmst genau diese Bausteine und gruppierst
sie zum ersten Mal in eine Klasse, dein erster echter Schritt in die
objektorientierte Programmierung.
