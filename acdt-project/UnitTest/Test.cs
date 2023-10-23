using System;
using Xunit;
using acdt_project.Classes;
using Microsoft.EntityFrameworkCore;
using acdt_project.Enums;
using acdt_project.Database;


namespace acdt_project.UnitTest
{
    public class Test
    {

/*
Der Test prüft also, ob die ToString-Methode der Incident-Klasse die Attribute der Instanz 
(wie z.B. Severity, IncidentStatus, Cve, usw.) korrekt in einer Zeichenfolge darstellt.
*/

        [Fact]
        public void IncidentTest()
        {
            var incident = new Incident(Enums.Severity.High,
                Enums.IncidentStatus.Open, 
                "CVE-2023-12345", 
                DateTime.Now, 1,
                "SystemA",
                "This is a test incident"
            );

            string incidentString = incident.ToString();

           
             Assert.Equal("IncidentId: 0\nSeverity: High\nIncidentStatus: Open\nCve: CVE-2023-12345\nCreatedAt: " + DateTime.Now.ToString() + "\nIssuer: 1\nSystem: SystemA\nDescription: This is a test incident\n", incidentString);

             //Assert.Equal("Dies ist ein anderer Text", incidentString);
        }

//Dieser Test überprüft, ob die AddIncident-Methode in der Incident-Klasse ordnungsgemäß funktioniert.

        [Fact]
        public void AddsIncidentToDatabe()
        {
            Incident incident = new Incident(Enums.Severity.Medium,
               Enums.IncidentStatus.Open,
               "CVE-2023-9999",
               DateTime.Now,
               2,
               "SystemX",
               "Test Incident"
            );

            Incident.AddIncident(incident);
            var retrievedIncident = Incident.GetIncident(incident.IncidentId);

            Assert.NotNull(retrievedIncident);
            Assert.Equal(incident.IncidentId, retrievedIncident.IncidentId);

        }

//Dieser Test überprüft, ob die GetIncident-Methode in der Incident-Klasse einen Vorfall anhand seiner ID erfolgreich abruft.
        [Fact]
        public void GetIncident_ReturnsIncidentByID()
        {
            Incident incident = new Incident(Enums.Severity.Critical,
                Enums.IncidentStatus.Open,
                "CVE-2023-54321",
                DateTime.Now,
                3,
                "SystemB",
                "brutal kritisch"  
            );

            Incident.AddIncident(incident);
            var retrievedIncident = Incident.GetIncident(incident.IncidentId);

            Assert.NotNull(retrievedIncident);
            Assert.Equal(incident.IncidentId, retrievedIncident.IncidentId);
        }


//Dieser Test überprüft, ob die AddUser-Methode in der User-Klasse einen Benutzer erfolgreich hinzufügt und in nacher wieder löscht.
        [Fact]
        public void AddUserToDataBase()
        {
            // Arrange
            User newUser = new User(
                "newUse",
                12,
                "1231287890",
                "Test@test.at"
            );

            // Act
            User.AddUser(newUser);
            var retrievedUser = User.GetUser(newUser.Username);

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.Equal(newUser.UserId, retrievedUser.UserId);
            Assert.Equal(newUser.Username, retrievedUser.Username);
            Assert.Equal(newUser.RoleId, retrievedUser.RoleId);
            Assert.Equal(newUser.PhoneNumber, retrievedUser.PhoneNumber);
            Assert.Equal(newUser.EMail, retrievedUser.EMail);

            // Cleanup
            User.DeleteUser(retrievedUser.UserId);
        }


/*
        public void CloseIncident()
        {
            Incident incident = new Incident(
                Enums.Severity.Low,
                IncidentStatus.Open,
                "CVW-2023-3214",
                DateTime.Now,
                6,
                "systemX",
                "noch ein Incident" 
            );

            Incident.CloseIncident(incident);
            var closedIncident = Incident.GetIncident(incident.IncidentId);

            Assert.NotNull(closedIncident);
            Assert.Equal(IncidentStatus.Closed, closedIncident.Status);
        }
        */
    }
}
    

            
