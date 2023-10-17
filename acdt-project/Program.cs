// import random library
using System;
using System.Globalization;
using acdt_project.Classes;
using acdt_project.Enums;
using Google.Protobuf.WellKnownTypes;


Incident incident = new Incident();

incident.IncidentId = 1;
incident.Severity = Severity.High;
incident.Issuer = 1;
incident.Status = Status.Open;
incident.Cve = "CVE-2021-1234";
incident.System = "Windows 10";
incident.Description = "This is a test incident";
incident.CreatedAt = new Timestamp();

incident.AddIncident();
incident.EditIncident();
incident.CloseIncident();



string defaultText = "Options:\n" +
                      "1 – Show Incidents\n" +
                      "2 – Create Incidents\n" +
                      "3 – Edit Incidents\n" +
                      "4 – Close Incidents\n" +
                      "5 – Exit\n";


string userInput = ""; 
do
{
    Console.WriteLine(defaultText);
    Console.Write("Enter your choice: ");
    userInput = Console.ReadLine() ?? "";
    
    switch (Convert.ToInt32(userInput))
    {
        case 1:
            incident.ShowIncident();
            break;
        case 2:
            incident.AddIncident();
            break;
        case 3:
            incident.EditIncident();
            break;
        case 4:
            incident.CloseIncident();
            break;
        case 5:
            Environment.Exit(0);
            break;
        default:
            break;
    }

} while (Convert.ToInt32(userInput) != 5);
