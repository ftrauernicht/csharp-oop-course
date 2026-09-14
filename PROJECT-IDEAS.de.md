🇩🇪 Deutsch | 🇬🇧 [English](PROJECT-IDEAS.md)

[← Zurück zur Repository-Übersicht](README.de.md) · Baut auf: [Kurs 1 – Basics](courses/01-basics/de/01-csharp-grundlagen.md)

# Die Kurs-Roadmap dieses Repositories

Anders als eine Liste von Nebenprojekt-Vorschlägen für nach einem Kurs ist
jeder Eintrag unten ein Kurs, den dieses Repository plant zu haben. Kurs 1
deckt einfaches, prozedurales C# ab -- noch keine Klassen. Alles danach
handelt davon, sich eine objektorientierte Idee nach der anderen zu
erarbeiten, jede an einem kleinen, vollständigen Projekt, statt vorab eine
Begriffswand aufzubauen, bevor irgendetwas davon Bedeutung hat. Kurs 3 und
alle weiteren hängen nicht voneinander ab, aber jeder einzelne von ihnen
hängt von Kurs 1 und 2 ab -- warum das Abhängigkeitsmodell dieses
Repositories sich leicht von einem Kurs unterscheidet, der nur "irgendein
C#-Können" braucht, steht in [CONTRIBUTING.de.md](CONTRIBUTING.de.md).

| # | Kurs | Neue Fähigkeiten, zusätzlich zu dem, was du schon kannst | Schwierigkeit |
|---|---|---|---|
| 1 | ~~Basics~~ -- Werte, Variablen, Operatoren, Methoden, Bedingungen, Schleifen | ✅ Gebaut -- siehe [Kurs 1 – Basics](courses/01-basics/de/01-csharp-grundlagen.md) | ⭐ |
| 2 | **Deine erste Klasse** -- eine Handvoll `Cat`-Objekte, die sich selbst vorstellen | Klasse vs. Objekt, Felder, Konstruktor, Methoden, `this` -- "Kapselung" wird erst benannt, nachdem du die Form schon gespürt hast | ⭐ |
| 3 | **Ein Hof-Inventar** -- die Pflanzen und Tiere eines kleinen Hofs gießen, ernten und zählen | `List<T>`, `foreach`, private Felder mit validierten öffentlichen Properties -- Kapselung, diesmal benannt und bewusst eingesetzt | ⭐⭐ |
| 4 | **Maschinenjäger, Teil 1** -- eine `Machine`-Basisklasse und ein paar konkrete Maschinentypen, jede mit eigenem Angriff | Vererbung, `virtual`/`override`, `base(...)`, `protected` | ⭐⭐⭐ |
| 5 | **Maschinenjäger, Teil 2** -- dieselben Maschinen, aus einer einzigen Liste heraus einzeln bekämpft | Abstrakte Klassen, Polymorphie -- die Auszahlung für das, was Kurs 4 im Stillen vorbereitet hat | ⭐⭐⭐ |
| 6 | **Eine Werkbank zum Craften** -- Items, die sich sammeln lassen, manche davon auch verkaufen | Interfaces, Komposition vs. Vererbung, mehr als ein Interface auf einer Klasse implementieren | ⭐⭐⭐⭐ |
| 7 | **Spielstand speichern** -- eine Sammlung in eine Datei schreiben und wieder einlesen | `System.Text.Json`, Objekte serialisieren und deserialisieren | ⭐⭐⭐ |

Ein paar Anmerkungen zum Lesen dieser Liste:

- Kurs 1 und 2 sind die eine feste Abhängigkeit, die alles andere hat.
  Darüber hinaus lassen sich Kurs 3 und alle weiteren in beliebiger
  Reihenfolge machen -- wähl, welches Projekt am meisten Spaß macht.
- Kurs 4 und 5 teilen sich absichtlich ein durchgängiges Beispiel -- eine
  kleine Menge an Maschinentypen -- weil sich Polymorphie viel leichter
  greifen lässt, wenn schon eine Vererbungshierarchie existiert, über die
  sie überhaupt polymorph sein kann. Trotzdem bleiben es zwei unabhängige,
  eigenständige Projekte: Kurs 5 baut seine eigene, kleine Version der
  Kurs-4-Hierarchie in ein paar Zeilen selbst wieder auf, bevor es zum
  eigentlichen Punkt kommt -- Kurs 5 ohne je Kurs 4 gemacht zu haben,
  funktioniert also genauso gut.
- Ein paar dieser Ideen greifen bewusst, an bestimmten Stellen statt
  durchgängig, Hobbys der Person auf, für die dieser Kurs zuerst geschrieben
  wurde: Katzen für die erste, kleinste Klasse (Kurs 2); ein
  Cozy-Farming-Sim-Flair — man denke an Stardew Valley, Animal Crossing:
  New Horizons oder Palia — für die Verwaltung einer wachsenden Sammlung
  von Objekten (Kurs 3); und eine Horizon-Zero-Dawn-artige Welt
  mechanischer Kreaturen für Vererbung und Polymorphie (Kurs 4-5), weil
  "verschiedene Maschinentypen, die sich eine Form teilen, sich aber
  unterschiedlich verhalten" wortwörtlich die Prämisse dieses Spiels ist.
  Kurs 1, 6 und 7 bleiben bewusst neutral.
- "Weniger Code, mehr Nachdenken" beginnt hier schon ab Kurs 3 -- zwei Kurse
  früher als der entsprechende Punkt im Geschwister-Repo für JavaScript.
  Sobald Kurs 1-2 die Form der Sprache und einer Klasse überhaupt vermittelt
  haben, ist das, was wirklich Übung braucht, die OOP-Entwurfsentscheidung
  selbst -- nicht das Abtippen von jemand anderes Entscheidung.

Egal was als Nächstes gebaut wird: Dieselben Gewohnheiten aus Kurs 1-2
gelten weiter -- zuerst die Kernversion bauen, sie einfach halten, und erst
dann zur optionalen, schwierigeren Variante eines Features greifen, wenn
die einfache funktioniert.
