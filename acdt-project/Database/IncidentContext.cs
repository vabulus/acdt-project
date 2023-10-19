
using acdt_project.Classes;
using Microsoft.EntityFrameworkCore;

namespace acdt_project.Database
{
    public class IncidentContext : DbContext
    {
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost,port=3306;Database=acdt-database;User=incident-user;Password=password;";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

            optionsBuilder.UseMySql(connectionString, serverVersion, builder => 
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Incident>()
                .Property(e => e.Severity)
                .HasConversion<int>();

            modelBuilder.Entity<Incident>()
                .Property(e => e.Status)
                .HasConversion<int>();
        }
    }
}