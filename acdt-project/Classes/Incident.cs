using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using acdt_project.Database;
using acdt_project.Enums;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace acdt_project.Classes;

public class Incident
{
    
    // if writeline a incident obj i want that all fields will get printed
    public override string ToString()
    {
        return $"IncidentId: {IncidentId}\n" +
               $"Severity: {Severity}\n" +
               $"IncidentStatus: {Status}\n" +
               $"Cve: {Cve}\n" +
               $"CreatedAt: {CreatedAt}\n" +
               $"Issuer: {Issuer}\n" +
               $"System: {System}\n" +
               $"Description: {Description}\n";
    }

    
    // make a constructor
    public Incident(Severity severity, IncidentStatus status, string cve, DateTime createdAt, int issuer, string system, string description)
    {
        Severity = severity;
        Status = status;
        Cve = cve;
        CreatedAt = createdAt;
        Issuer = issuer;
        System = system;
        Description = description;
    }
    
    [Key]
    public int IncidentId { get; set; }
    
    public Severity Severity { get; set; }
    public IncidentStatus Status { get; set; }
    
    public string Cve { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Issuer { get; set; }
    public string System { get; set; }
    public string Description { get; set; }


    public static void AddIncident(Incident incidentObj)
    {
        using (var context = new IncidentContext())
        {
            context.Incidents.Add(incidentObj);
            context.SaveChanges();
        }   
    }

    public static Incident? GetIncident(int incidentId)
    {
        using (var context = new IncidentContext())
        {
            return context.Incidents.SingleOrDefault(i => i.IncidentId == incidentId);
        }   
    }

    public static void UpdateIncident(Incident incidentToUpdate)
    {
        using (var context = new IncidentContext())
        {
            context.Incidents.Attach(incidentToUpdate);
            context.Entry(incidentToUpdate).State = EntityState.Modified;
            context.SaveChanges();
        }   
    }
    

    public static List<Incident> FetchIncidents()
    {
        using (var context = new IncidentContext())
        {
            // load the incidents from the database
            var incidents = context.Incidents.ToListAsync().Result;
            return incidents;
        }  
    }
}