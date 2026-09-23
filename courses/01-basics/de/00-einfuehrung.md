🇩🇪 Deutsch | 🇬🇧 [English](../en/00-introduction.md)

[← Zurück zur Kursübersicht](../../../README.de.md) · Weiter: [Kapitel 1 – C#-Grundlagen](01-csharp-grundlagen.md) →

# Kapitel 0 – Einführung & Werkzeuge

## Was ist C#, und warum braucht es eine IDE?

C# (gesprochen "C Sharp") ist eine Allzweck-Programmiersprache von
Microsoft. Sie läuft auf **.NET**, einer kostenlosen, plattformübergreifenden
Laufzeitumgebung für Windows, macOS und Linux. C# steckt unter anderem in
Desktop-Anwendungen, Web-Backends und Spielen, die mit der Unity-Engine
gebaut werden. Mehr dazu bei
[Microsoft Learn – "Einführung in C#"](https://learn.microsoft.com/de-de/dotnet/csharp/tour-of-csharp/).

Anders als JavaScript im Browser läuft C# nicht einfach so. Es muss zuerst
**kompiliert** werden: übersetzt aus dem Code, den du schreibst, in etwas,
das dein Computer ausführen kann, wobei der Compiler deinen Code dabei auf
Fehler prüft. Genau dieser Kompilierschritt ist der Grund, warum dieser Kurs
mit der Installation einer **IDE** (Integrated Development Environment)
beginnt, statt einfach einen Browser-Tab zu öffnen: ein Werkzeug, mit dem du
Code schreibst, kompilierst und ausführst, das dich auf Fehler hinweist,
bevor du überhaupt auf "Ausführen" klickst.

## Visual Studio Community installieren

Wir benutzen **Visual Studio Community**, nicht zu verwechseln mit dem
ähnlich benannten [Visual Studio Code](https://code.visualstudio.com/), einem
anderen, viel schlankeren Editor. Visual Studio Community ist die
vollständige IDE, kostenlos für Einzelpersonen, Studierende und
Open-Source-Arbeit, und die Screenshots und Menüs in diesem Kurs gehen davon
aus.

1. Lade es von [visualstudio.microsoft.com](https://visualstudio.microsoft.com/de/vs/community/)
   herunter.
2. Starte das Installationsprogramm. Wenn es nach dem **Workload** fragt,
   hak **".NET-Desktopentwicklung"** an. Das ist der Workload, der dir die
   C#-Konsolenanwendungs-Vorlagen, den Debugger und alles Weitere gibt, was
   dieser Kurs braucht. Den Rest kannst du erstmal ungewählt lassen; du
   kannst jederzeit später über dasselbe Installationsprogramm mehr
   hinzufügen.
3. Download und Installation dauern eine Weile (ein paar Gigabyte). Ein
   guter Moment für einen Kaffee.

Die offizielle Schritt-für-Schritt-Anleitung mit aktuellen Screenshots (der
genaue Dialogaufbau ändert sich zwischen Visual-Studio-Versionen, deshalb
lohnt sich hier Microsofts eigene, aktuelle Anleitung mehr als ein
statischer Screenshot in diesem Repo):
[Visual Studio installieren](https://learn.microsoft.com/de-de/visualstudio/install/install-visual-studio).

## Dein erstes Projekt anlegen

1. Öffne Visual Studio und wähle **"Neues Projekt erstellen"**.
2. Tipp im Suchfeld **"Konsolen-App"** ein. Du siehst wahrscheinlich zwei
   ähnliche Einträge. Wähl die schlichte **"Konsolen-App"** mit **C#**-Tag,
   die das aktuelle .NET als Ziel hat (nicht **"Konsolen-App
   (.NET Framework)"**, eine ältere, nur unter Windows lauffähige
   Technologie, die dieser Kurs nicht benutzt). Falls du unsicher bist, halt
   die Maus über die Einträge: Die Beschreibung nennt das jeweilige
   Framework.
3. Gib einen Namen ein (z. B. `MyFirstProject`) und einen Speicherort, dann
   klick auf **Erstellen**.

Visual Studio hat gerade eine **Solution** angelegt (eine `.sln`-Datei: ein
Container, der ein oder mehrere Projekte enthalten kann) mit einem
**Projekt** darin (eine `.csproj`-Datei: das, was tatsächlich kompiliert
wird). Für diesen ganzen Kurs sind Solution und Projekt praktisch immer ein
1-zu-1-Paar; den Unterschied merkst du erst, sobald eine Solution mehr als
ein Projekt enthält.

Öffne `Program.cs` (die einzige Datei, die die Vorlage angelegt hat). Sie
enthält bereits eine Zeile:

```csharp
Console.WriteLine("Hello, World!");
```

Das ist ein vollständiges, lauffähiges C#-Programm. Keine `class`, kein
`Main`, das drumherum liegt. Modernes C# erlaubt es dir, dieses
Grundgerüst vorerst wegzulassen, mit einem Feature namens **Top-Level
Statements**: Der Inhalt der Datei läuft einfach von oben nach unten, wie
ein Skript. (Falls dir mal ein *älteres* C#-Beispiel mit
`class Program { static void Main(string[] args) { ... } }` drumherum
begegnet, ist das dasselbe, nur ausgeschrieben; diese Form lernst du in
Kurs 2 kennen, sobald Klassen wirklich das Thema sind.)

## Dein Programm ausführen

Zwei gleichwertige Wege, beide lohnt es sich zu kennen:

- **In Visual Studio:** klick auf den grünen ▶ **Start**-Button, oder drück
  <kbd>F5</kbd>. Ein Konsolenfenster öffnet sich und führt dein Programm aus.
- **Im Terminal:** öffne eines, wechsle mit `cd` in den Ordner deines
  Projekts (den mit der `.csproj`-Datei) und führ aus:
  ```
  dotnet run
  ```
  Das funktioniert identisch außerhalb von Visual Studio. Gut zu wissen,
  denn genauso führen CI-Server, andere Editoren und die automatischen
  Prüfungen dieses Kurses C#-Code aus.

In beiden Fällen solltest du `Hello, World!` sehen. Das ist dein erstes
laufendes C#-Programm.

Ein schneller Check für dein Terminal, später jederzeit nützlich, wenn
irgendwas nicht passt: `dotnet --version` zeigt die installierte
.NET-SDK-Version an.

## Compiler-Fehler sind ein Feature, kein Monster

Bau absichtlich einen Fehler ein: Lösch das schließende `"` bei
`"Hello, World!"` und versuch, das Programm auszuführen. Visual Studio
unterstreicht das Problem rot, *bevor* du überhaupt auf "Ausführen"
klickst, und weigert sich zu bauen, bis du es behebst.

Das ist der Kompilierschritt von oben, der gerade für dich arbeitet: C#
fängt eine ganze Kategorie von Fehlern (Tippfehler, falsche Typen, fehlende
Teile) ab, bevor dein Programm überhaupt läuft, statt wie manch andere
Sprache erst mittendrin abzustürzen. Die Fehlermeldung zu lesen (sie nennt
Datei, Zeile und was erwartet wurde) ist selbst eine Fähigkeit, die du dir
schnell aneignest. Setz das Anführungszeichen wieder ein, bevor es
weitergeht.

## Wie dieser Kurs aufgebaut ist

Jedes Kapitel folgt demselben Muster:

- 🟢 **Kern** — der Pflichtteil. Den schließt du ab, bevor es zum nächsten
  Kapitel geht.
- 🟡 **Optional, mehr Übung** — vertieft dieselben Ideen des Kapitels noch
  etwas. Gut zu machen, aber nicht Pflicht.
- 🔴 **Optional, echte Herausforderung** — ein anspruchsvolleres Zusatzziel,
  das manchmal ein Konzept vorzieht, das eigentlich erst später drankäme. Es
  ist völlig in Ordnung, wenn es beim ersten Versuch nicht klappt; du kannst
  jederzeit nach einem späteren Kapitel zurückkommen.

Die Kapitel bauen absichtlich aufeinander auf, genauso wie die Kurse. Kurs 1
und Kurs 2 bilden zusammen die geteilte Grundlage, die jeder spätere Kurs
voraussetzt. Anders als bei einem Kurs, der nur "irgendwas in C#" braucht,
ist OOP selbst kumulativ aufgebaut. Deshalb bleiben genau diese beiden Kurse
Pflicht, während alles danach in beliebiger Reihenfolge geht.

Noch etwas zur Sprache: Dieser Kurs liegt als getrennte, parallele Dateien
auf Deutsch und Englisch vor. Lies, was dir leichter fällt. **Der Code
selbst und seine Kommentare sind immer auf Englisch**, so wie es in echten
Software-Teams unabhängig von der gesprochenen Sprache üblich ist. Links zum
Weiterlesen (meistens zu [Microsoft Learn](https://learn.microsoft.com/de-de/dotnet/csharp/),
der offiziellen C#-Dokumentation) stehen direkt im Text, dort wo ein neuer
Begriff zum ersten Mal auftaucht, statt am Ende in einem Glossar gesammelt
zu werden.

## Bereit?

Weiter zu [Kapitel 1 – C#-Grundlagen](01-csharp-grundlagen.md).
