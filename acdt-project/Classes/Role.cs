using System.ComponentModel.DataAnnotations;

namespace acdt_project.Classes;

public class Role
{
    [Key]
    public int roleID { get; set; }
    public string Name { get; set; }
    
    public int userID { get; set; }
    
    
}