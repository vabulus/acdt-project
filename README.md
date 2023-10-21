# SIMS - Security Incident Management System

![.NET Version](https://img.shields.io/badge/.NET-7.0-brightgreen)
![License](https://img.shields.io/badge/License-MIT-blue)

**Version:** 1.0

## Inhaltsverzeichnis
1. [Beschreibung](#beschreibung)
2. [Systemanforderungen](#systemanforderungen)
3. [Installation](#installation)
4. [Verwendung](#verwendung)
5. [Datenbank](#datenbank)
6. [Docker](#docker)
7. [Beispiel](#beispiel)
8. [Roadmap](#roadmap)
9. [Lizenz](#lizenz)
10. [Hinweis zu SAST](#hinweis-zu-sast)
11. [ER-Diagramm](#er-diagramm)
12. [Klassendiagramm](#klassendiagramm)
13. [Git-Repository](#git-repository)

## Beschreibung
Das "Security Incident Management System" (SIMS) ist ein Tool zur Protokollierung von IT-Sicherheitsvorfällen. Es ermöglicht Benutzern das Erfassen, Eskalieren und Benachrichtigen von sicherheitsrelevanten Vorfällen.

**Features:**
- Manuelle Erfassung von sicherheitsrelevanten Vorfällen (Bearbeiter, Melder, Schweregrad, Status, CVE, System, Beschreibung, Zeitstempel, etc.).
- Eskalation an den nächsten Benutzer.
- Notifizierung über verschiedene Kanäle (SMS, Signal, etc.).
- Benutzerverwaltung mit Rollen (Administratoren, Benutzer, etc.).
- Protokollierung von Vorfällen in einer Log-Tabelle.
- Relationale Datenbank zur Speicherung von Vorfällen, Benutzern und Log-Daten.

## Systemanforderungen
- Betriebssystem: Unterstützte Betriebssysteme
- .NET-Runtime: .NET 7.0
- Docker: Erforderlich, um die Anwendung und die Datenbank in Containern bereitzustellen
- Docker-Container starten:
    - docker-compose up -d ausführen

## Installation
1. Klonen Sie das Git-Repository: [Link zum Repository](https://github.com/IhrBenutzername/sims)
2. Führen Sie die Docker-Compose-Datei aus, um die Anwendung und die Datenbank zu starten.

## Verwendung
1. Starten Sie die Anwendung.
2. Wählen Sie aus den angebotenen Optionen, um Vorfälle anzuzeigen, hinzuzufügen, zu bearbeiten, zu schließen oder das Programm zu beenden.

## Datenbank
Die Anwendung verwendet eine MySQL-Datenbank zur Speicherung von Vorfällen und Benutzerinformationen. Das Datenbankschema sind in den Diagrammen dokumentiert.

## Docker
Die Anwendung und die Datenbank wird mithilfe von Docker-Containern bereitgestellt. Die Docker-compose Datei wird benötigt um Docker zu starten.

## Lizenz
Dieses Projekt steht unter der [MIT-Lizenz](https://opensource.org/licenses/MIT).

## ER-Diagramm

## Klassendiagramm

## Semgrep Results
| Key            | Value                                                                                                                                                                                                                                                                                                                   |
| -------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| errors         | []                                                                                                                                                                                                                                                                                                                       |
| paths (scanned)| [".vs", "Classes/Escalation.cs", "Classes/Incident.cs", "Classes/Log.cs", "Classes/MailNotification.cs", "Classes/Role.cs", "Classes/SignalNotification.cs", "Classes/SmsNotification.cs", "Classes/User.cs", "Database/IncidentContext.cs", "Dockerfile", "Enums/IncidentStatus.cs", "Enums/MenuOption.cs", "Enums/Severity.cs", "Interfaces/INotification.cs", "Migrations/20231019151822_InitialCreate.Designer.cs", "Migrations/20231019151822_InitialCreate.cs", "Migrations/IncidentContextModelSnapshot.cs", "Program.cs", "acdt-project.csproj", "docker-semgrep.sh", "scan_results.json", "scan_results.txt", "trigger.sql"] |
| results        | []                                                                                                                                                                                                                                                                                                                       |
| skipped_rules  | []                                                                                                                                                                                                                                                                                                                       |
| version        | "1.45.0"                                                                                                                                                                                                                                                                                                                 |
