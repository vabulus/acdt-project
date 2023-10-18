// import random library
using System;
using System.Globalization;using System.Runtime.InteropServices.JavaScript;
using acdt_project.Classes;
using acdt_project.Database;
using acdt_project.Enums;


Incident incidentObj = new Incident();

incidentObj.IncidentId = 1;
incidentObj.Severity = Severity.High;
incidentObj.Issuer = 1;
incidentObj.Status = Status.Open;
incidentObj.Cve = "CVE-2021-1234";
incidentObj.System = "Windows 10";
incidentObj.Description = "This is a test incident";
incidentObj.CreatedAt = DateTime.Now;


using (var context = new IncidentContext())
{
    context.Incidents.Add(incidentObj);
    context.SaveChanges();
}   




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
            incidentObj.ShowIncident();
            break;
        case 2:
            incidentObj.AddIncident();
            break;
        case 3:
            incidentObj.EditIncident();
            break;
        case 4:
            incidentObj.CloseIncident();
            break;
        case 5:
            Environment.Exit(0);
            break;
        default:
            break;
    }

} while (Convert.ToInt32(userInput) != 5);
