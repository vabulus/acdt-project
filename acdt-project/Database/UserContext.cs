using acdt_project.Classes;
using Microsoft.EntityFrameworkCore;

namespace acdt_project.Database;

public class UserContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=localhost,1433;Database=users;User=root;Password=password;";
        var serverVersion = new MySqlServerVersion(new Version(0, 0, 0));

        optionsBuilder.UseMySql(connectionString, serverVersion)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
}