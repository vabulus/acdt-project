
using acdt_project.Classes;
using Microsoft.EntityFrameworkCore;

namespace acdt_project.Database
{
    public class IncidentContext : DbContext
    {
        public DbSet<Incident> Incidents { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Log> Log { get; set; }  
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Port=3306;Database=acdtDatabase;User=root;Password=root;";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));

            optionsBuilder.UseMySql(connectionString, serverVersion, builder =>
                {
                    builder.EnableRetryOnFailure(
                        maxRetryCount: 5,             
                        maxRetryDelay: TimeSpan.FromSeconds(10), 
                        errorNumbersToAdd: null     
                    );
                })
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