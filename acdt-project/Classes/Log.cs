using System.ComponentModel.DataAnnotations;
using acdt_project.Enums;

namespace acdt_project.Classes;

public class Log
{
    [Key]
    public int LogId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Severity LogLevel { get; set; }
    public string Message { get; set; }
    
    
}