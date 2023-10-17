using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using acdt_project.Enums;
using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;

namespace acdt_project.Classes;

public class Incident
{
    [Key]
    public int IncidentId { get; set; }
    
    public Severity Severity { get; set; }
    public Status Status { get; set; }
    
    public string Cve { get; set; }
    public Timestamp CreatedAt { get; set; }
    public int Issuer { get; set; }
    public string System { get; set; }
    public string Description { get; set; }


    public void AddIncident()
    {
        throw new NotImplementedException();
    }

    public void EditIncident()
    {
        throw new NotImplementedException();
    }

    public void CloseIncident()
    {
        throw new NotImplementedException();
    }

    public void ShowIncident()
    {
        throw new NotImplementedException();
    }
}