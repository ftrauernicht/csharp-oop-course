🇩🇪 Deutsch | 🇬🇧 [English](CONTRIBUTING.md)

# Mitwirken bei C# OOP Course

Dies ist ein privates Projekt, das von einer Person gepflegt wird.

- Issues und Pull Requests sind willkommen.
- Pull Requests bitte klein und fokussiert halten -- das macht sie leichter
  zu prüfen und leichter rückgängig zu machen.
- Es wird keine Contributor License Agreement benötigt.
- Antwortzeiten variieren; dieses Repository wird nicht nach einem festen
  Zeitplan betreut.

## Einen neuen Kurs hinzufügen

Dieses Repository ist darauf angelegt, zu wachsen. Wer einen neuen Kurs
hinzufügt -- von Hand oder mit KI-Unterstützung -- sollte wissen, dass die
bestehenden Kurse Konventionen folgen, die aus keiner einzelnen Datei
ersichtlich sind; sie zeigen sich erst, wenn man mehrere Kurse
nebeneinander vergleicht. Sie stehen deshalb hier, damit ein neuer
Mitwirkender oder eine frische KI-Sitzung ohne Erinnerung an frühere
Arbeit sie befolgen kann, ohne sie erst mühsam zu rekonstruieren.

**Struktur**
- Jeder Kurs ist ein neuer Ordner `courses/NN-name/`, nummeriert in der
  Reihenfolge, in der er entstanden ist. Bei jedem neuen Kurs die
  Kurs-Tabelle in `README.md`/`README.de.md` aktualisieren, sowie die
  Roadmap in `PROJECT-IDEAS.md`/`PROJECT-IDEAS.de.md` (den Eintrag als
  gebaut markieren).
- **Kurs 1 und Kurs 2 sind die geteilte, verpflichtende Grundlage.** Anders
  als bei einem Kurs über "irgendein C#-Können" bauen objektorientierte
  Ideen direkt aufeinander auf, deshalb setzt jeder Kurs ab Kurs 3 **beide**
  voraus, Kurs 1 und 2 -- nie einen anderen Geschwister-Kurs über diese
  beiden hinaus. Braucht ein Kurs ein Konzept, das Kurs 1-2 nicht
  vermitteln, wird es kurz inline erklärt, mit einem Hinweis wie "(falls du
  Kurs 4 gemacht hast, spring weiter)" für Leser, die es schon kennen -- es
  wird nicht als bekannt vorausgesetzt.
- Kapitel liegen in parallelen `en/`- und `de/`-Ordnern mit identischem
  Inhalt und identischer Struktur, nicht als eine zweisprachige Datei.
  Code und Code-Kommentare sind immer auf Englisch. Sprachübergreifende
  Links zeigen auf die passende
  [Microsoft-Learn](https://learn.microsoft.com/de-de/dotnet/csharp/)-Sprachversion
  (`/en-us/` bzw. `/de-de/`).
- Jedes Kapitel benutzt dasselbe Drei-Stufen-System, einmal in Kurs 1
  Kapitel 0 eingeführt und sonst nirgends erneut erklärt: 🟢 Kern
  (notwendig), 🟡 Optional (mehr Übung), 🔴 Optional (eine echte
  Herausforderung).
- Ein Kurs bekommt einen `code/`-Ordner: ein fertiges, baubares
  Konsolenprojekt (eine `.csproj` mit ihren `.cs`-Dateien) als Musterlösung
  -- unverändert lauffähig, und das, womit Lernende ihren eigenen Versuch
  vergleichen.
- **Screenshots sind hier die Ausnahme, nicht die Regel.** Fast jeder Kurs
  ist eine Konsolenanwendung, es gibt also meist nichts zu screenshotten --
  zeig stattdessen einen Codeblock mit "Beispielausgabe" im Kapiteltext. Nur
  ein Kurs mit echter UI bekommt einen `assets/`-Ordner.

**Vor der Veröffentlichung eines neuen Kapitels auf diese wiederkehrenden
Fehler prüfen** (dieselbe Liste wie im Geschwister-Repo für JavaScript,
plus zwei C#-spezifische):
- Querverweise auf "Kurs N" oder "Kapitel N" veralten still, wenn Inhalt
  umnummeriert oder aufgeteilt wird -- sowohl nach der Einzahl- als auch
  der Mehrzahlform suchen ("Kurs 3", "Kurs 3 und 4"), in beiden Sprachen.
- Ein Versprechen, etwas "später" zu erklären, muss tatsächlich eingelöst
  werden, oder ehrlich umformuliert werden, falls es nicht behandelt wird.
- Ein Kurs, der behauptet, nur Kurs 1 und 2 vorauszusetzen, muss gegen
  deren *tatsächlichen* Inhalt geprüft werden, nicht den beabsichtigten --
  erneut prüfen, sobald sich Kurs 1 oder 2 selbst ändern.
- **Jedes `code/`-Projekt muss sauber mit `dotnet build` bauen, ohne
  Warnungen** -- eine stehen gelassene Warnung in einer Musterlösung liest
  sich für Anfänger als "das ist okay, ignorieren", obwohl sie noch gar
  nicht wissen können, welche Warnungen wichtig sind.
- **Jedes Snippet vor der Veröffentlichung tatsächlich ausführen**, nicht
  nur das fertige `code/`-Projekt -- jedes Inline-Beispiel in ein
  Scratch-Projekt kopieren und bauen, besonders alles in einem optionalen
  Abschnitt. Ein Snippet, das neben lauffähigem Code nur plausibel aussieht,
  rutscht leicht durch, und C# kompiliert einen Tippfehler nicht einfach
  mit, wie es eine dynamisch typisierte Sprache manchmal noch täte.
- "Wohin gehört dieser Code" an jeder Stelle wiederholen, an der das
  mehrdeutig sein könnte, nicht nur einmal am Kapitelanfang.

**Wie viel Code man zeigt**: Kurs 1 und 2 geben fertigen, funktionierenden
Code zum Abtippen vor -- dort geht es darum, überhaupt die Form der Sprache
und einer Klasse kennenzulernen. Ab Kurs 3 bekommen die Kapitel den neuen
Baustein (ein OOP-Konzept) vollständig erklärt, überlassen aber die
Kernlogik der Übung -- den Teil, der eigentlich der Sinn des Kapitels ist --
als in Schritten beschriebene Aufgabe zum Selbst-Zusammensetzen, mit der
fertigen Version nur in `code/` als Musterlösung. Das beginnt zwei Kurse
früher als die entsprechende Regel im Geschwister-Repo für JavaScript, ganz
bewusst: Das eigentliche Thema hier sind OOP-Entwurfsentscheidungen -- genau
der Teil, der *durchdacht*, nicht abgetippt werden muss.

**Commit-Nachrichten** folgen [Conventional Commits](https://www.conventionalcommits.org/)
(`feat: add Course 3 - ...`, `fix: ...`, `docs: ...`). Ein neuer Kurs ist
typischerweise ein `feat:`-Commit, der Kapiteltext, Code und die
README-/PROJECT-IDEAS-Aktualisierungen zusammen abdeckt.
