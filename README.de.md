🇩🇪 Deutsch | 🇬🇧 [English](README.md)

# C# OOP Course

Ein anfängerfreundlicher, projektbasierter Kurs, um objektorientierte
Programmierung wirklich zu *verstehen* — nicht nur die Wörter
"Kapselung" oder "Polymorphie" aufsagen zu können, sondern damit schon
kleine, funktionierende Dinge gebaut zu haben. C# ist die Sprache, gewählt
wegen ihres Compilers: ein geduldiger Lehrer, der dir in einem vollständigen
Satz genau sagt, was falsch ist, bevor dein Programm überhaupt läuft. Der
Kurs ist so gebaut, dass er sich direkt weitergeben lässt: Schick jemandem
den Link zu diesem Repository, und alles, was diese Person zum Start
braucht, liegt bereits hier. Es wird keinerlei Programmiererfahrung
vorausgesetzt. Jedes Kapitel baut auf dem vorherigen auf, und jedes Kapitel
hat einen optionalen "geh weiter"-Pfad für alle, die mehr Herausforderung
wollen.

Dieser Kurs existiert **auf Deutsch und Englisch, nebeneinander**: Jedes
Kapitel liegt als zwei getrennte Dateien mit demselben Inhalt vor — lies,
was dir leichter fällt, und wechsle jederzeit. Code und Code-Kommentare sind
allerdings immer auf Englisch. Das ist eine bewusste Entscheidung und eine
reale Konvention: professionelle Codebasen werden auf Englisch geschrieben,
unabhängig davon, welche Sprache das Team spricht — das gewöhnt man sich am
besten gleich von der ersten Zeile an.

Objektorientierte Programmierung wird bewusst schrittweise eingeführt: Kurs
1 ist einfaches, prozedurales C# — noch keine Klassen. Neue Ideen kommen
eine nach der anderen, jede an einem kleinen Projekt erarbeitet, statt alle
auf einmal als Begriffswand. Die vollständige Roadmap, wohin das führt — vom
ersten Klasse über Vererbung, Polymorphie bis zu Interfaces — steht in
[PROJECT-IDEAS.de.md](PROJECT-IDEAS.de.md).

## Diesen Kurs auf deinen Computer bekommen

**Option A — ohne Git:**

1. Geh zu <https://github.com/ftrauernicht/csharp-oop-course>.
2. Klick auf den grünen **Code**-Button → **Download ZIP**.
3. Entpack die heruntergeladene ZIP-Datei irgendwo auf deinem Computer
   (Rechtsklick → *Alle extrahieren* unter Windows, oder Doppelklick unter
   Mac).
4. Öffne den entpackten Ordner. Du hast jetzt jede Datei, die dieser Kurs
   braucht.

**Option B — mit [Git](https://git-scm.com/), falls schon installiert:**

```
git clone https://github.com/ftrauernicht/csharp-oop-course.git
```

So oder so: Anders als bei einem Kurs, der im Browser läuft, braucht C# eine
installierte IDE, bevor überhaupt etwas läuft — Kurs 1, Kapitel 0 führt dich
Schritt für Schritt durch die Installation. Starte beim Lesen mit
[Kurs 1 – Basics, Kapitel 0](courses/01-basics/de/00-einfuehrung.md).

## Was du vor dem Start brauchst

| Werkzeug | Warum du es brauchst | Link |
|---|---|---|
| [Visual Studio Community](https://visualstudio.microsoft.com/de/vs/community/) | Die kostenlose IDE, von der die Anleitungen und Menüs in diesem Kurs ausgehen — schreibt, kompiliert und führt dein C# aus | [visualstudio.microsoft.com](https://visualstudio.microsoft.com/de/vs/community/) |
| Das .NET SDK | Wird vom Visual-Studio-Installationsprogramm automatisch mitinstalliert, sobald du den Workload "**.NET-Desktopentwicklung**" auswählst | Liegt Visual Studio bei |

Die Schritt-für-Schritt-Anleitung zur Installation (inklusive der
richtigen Installer-Optionen) steht in
[Kurs 1 – Basics, Kapitel 0](courses/01-basics/de/00-einfuehrung.md).

## Kurse

Dieses Repository ist darauf angelegt, mit der Zeit mehr als einen Kurs zu
enthalten. Jeder bekommt seine eigene Nummer unter `courses/`, in der
Reihenfolge, in der er entstanden ist.

Kurs 1 und Kurs 2 bilden zusammen die geteilte Grundlage, die jeder spätere
Kurs voraussetzt — objektorientierte Ideen bauen direkter aufeinander auf
als etwa "wie hole ich Daten von einer API", deshalb bleiben genau diese
beiden Kurse Pflicht, statt jeden einzelnen Kurs komplett unabhängig zu
machen. Kurs 3 und alle weiteren sind dann jeweils unabhängige,
eigenständige Projekte, die nur Kurs 1 und 2 voraussetzen — wähl, was dich
mehr interessiert, in beliebiger Reihenfolge.

### Kurs 1 – Basics (`courses/01-basics/`)

An kein bestimmtes Projekt gebunden — einfaches, prozedurales C#: noch keine
Klassen, ganz bewusst. Das gemeinsame Vokabular, das Kurs 2 und alles
danach voraussetzt.

| # | Kapitel | Was du lernst |
|---|---|---|
| 0 | [Einführung & Werkzeuge](courses/01-basics/de/00-einfuehrung.md) | Visual Studio installieren, ein Konsolenprojekt anlegen und ausführen, warum C# einen Compiler braucht |
| 1 | [C#-Grundlagen](courses/01-basics/de/01-csharp-grundlagen.md) | Werte, statische Typisierung, Variablen, Operatoren, String-Interpolation, Methoden, Bedingungen, Schleifen |

### Kurs 2 – Katzenkartei (`courses/02-cat-roster/`)

Setzt Kurs 1 voraus. Dein erster echter Schritt in die OOP — eine Handvoll
unabhängiger `Cat`-Objekte, die sich selbst vorstellen.

| # | Kapitel | Was du lernst |
|---|---|---|
| 1 | [Katzenkartei](courses/02-cat-roster/de/01-katzenkartei.md) | Klasse vs. Objekt, Properties, Konstruktoren, `this`, Instanzmethoden, die den Zustand eines Objekts lesen oder ändern |

### Kurs 3 – Hof-Inventar (`courses/03-homestead-inventory/`)

Setzt Kurs 1 und 2 voraus. Ein kleiner Hof, der beliebig viele Pflanzen
verfolgt, von denen jede selbst durchsetzt, wie viel sie gegossen werden
kann.

| # | Kapitel | Was du lernst |
|---|---|---|
| 1 | [Hof-Inventar](courses/03-homestead-inventory/de/01-hof-inventar.md) | `List<T>`, `foreach`, private Felder, nur-lesbare und privat setzbare Properties, Kapselung über echte Validierungslogik |

### Kurs 4 – Maschinenjäger (`courses/04-machine-hunter/`)

Setzt Kurs 1 und 2 voraus. Eine `Machine`-Basisklasse und eine Handvoll
konkreter Maschinentypen, die jeweils auf ihre eigene Art angreifen.

| # | Kapitel | Was du lernst |
|---|---|---|
| 1 | [Maschinenjäger](courses/04-machine-hunter/de/01-maschinenjaeger.md) | Vererbung, `virtual`/`override`, `base(...)`, `protected` |

Mit der Zeit kommen weitere Kurse dazu; dieser Abschnitt wächst mit ihnen.
Was als Nächstes kommt, steht in
[PROJECT-IDEAS.de.md](PROJECT-IDEAS.de.md).

## Projektideen

[PROJECT-IDEAS.de.md](PROJECT-IDEAS.de.md) ist die eigene Roadmap dieses
Repositories, keine Liste von Nebenprojekt-Vorschlägen: Jeder Eintrag dort
ist ein Kurs, den dieses Repository plant zu bauen, in der geplanten
Reihenfolge, beginnend mit Kurs 5. Die Datei liegt im Repository-Root und
nicht in einem einzelnen Kurs, damit der ganze Bogen aus einer Datei
sichtbar ist.

## Wie du diesen Kurs benutzt

1. Lies ein Kapitel von oben nach unten.
2. Tipp die Beispiele selbst ab, statt sie zu kopieren — Abtippen baut das
   Muskelgedächtnis auf, Kopieren baut nur einen Scrollbalken.
3. Jedes Kapitel hat einen **Kernteil** (🟢, Pflicht, um weiterzumachen) und
   einen oder mehrere **optionale Teile** (🟡 solide Zusatzübung, 🔴 eine
   echte Herausforderung). Die optionalen Teile zu überspringen ist völlig
   in Ordnung — komm später darauf zurück, wenn du magst.
4. Innerhalb eines Kurses bauen spätere Kapitel ausdrücklich auf früheren
   auf. Kurs 1 und 2 sind Pflicht, in dieser Reihenfolge, für alles danach;
   Kurs 3 und alle weiteren bauen nur auf diesen beiden auf, unabhängig
   voneinander.
5. Links in jedem Kapitel zeigen auf die passende
   [Microsoft-Learn](https://learn.microsoft.com/de-de/dotnet/csharp/)-Seite
   (die offizielle C#-Dokumentation), genau dort, wo ein neuer Begriff zum
   ersten Mal auftaucht — statt am Ende in einem Glossar gesammelt zu
   werden. Ist ein Begriff unklar, ist der nächste Link dein Glossar.

## Repository-Aufbau

```
PROJECT-IDEAS.md / .de.md   die eigene Kurs-Roadmap dieses Repositories
courses/
  01-basics/                 Kurs 1 — einfaches C#, noch keine Klassen
    en/                        Kapiteltexte, Englisch
    de/                        Kapiteltexte, Deutsch
    code/                      Musterlösung (Basics.csproj, Program.cs)
  02-cat-roster/             Kurs 2 — deine erste Klasse
    en/                        Kapiteltexte, Englisch
    de/                        Kapiteltexte, Deutsch
    code/                      Musterlösung (CatRoster.csproj, Cat.cs, Program.cs)
  03-homestead-inventory/    Kurs 3 — Sammlungen und echte Kapselung
    en/                        Kapiteltexte, Englisch
    de/                        Kapiteltexte, Deutsch
    code/                      Musterlösung (HomesteadInventory.csproj, Crop.cs, Animal.cs, Program.cs)
  04-machine-hunter/         Kurs 4 — Vererbung
    en/                        Kapiteltexte, Englisch
    de/                        Kapiteltexte, Deutsch
    code/                      Musterlösung (MachineHunter.csproj, Machine.cs, Watcher.cs, Thunderjaw.cs, Grazer.cs, Strider.cs, Program.cs)
  05-.../                    zukünftige Kurse, gleiches Muster
```

## Mitwirken

Siehe [CONTRIBUTING.de.md](CONTRIBUTING.de.md).
