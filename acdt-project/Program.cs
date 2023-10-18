// import random library
using acdt_project.Classes;
using acdt_project.Enums;


string defaultText = "Options:\n" +
                      "1 – Show Incidents\n" +
                      "2 – Add Incidents\n" +
                      "3 – Edit Incidents\n" +
                      "4 – Close Incidents\n" +
                      "5 – Exit\n";


string userInput = ""; 
do
{
    User userObj = UserAuthentication();
    
    Console.WriteLine(defaultText);
    Console.Write("Enter your choice: ");
    userInput = Console.ReadLine() ?? "";
    
    switch (Convert.ToInt32(userInput))
    {
        case 1:
            foreach (var incident in Incident.ShowIncident())
            {
                Console.WriteLine(incident.IncidentId);
                Console.WriteLine(incident.Severity);
                Console.WriteLine(incident.Status);
                Console.WriteLine(incident.Cve);
                Console.WriteLine(incident.CreatedAt);
                Console.WriteLine(incident.Issuer);
                Console.WriteLine(incident.System);
                Console.WriteLine(incident.Description);
            }
            break;
        case 2:
            Severity severityUser = GetSeverityInput("Enter the severity of the incident (1-4, default = 1): ");
            string cve = GetInput("Enter the CVE of the incident: ");
            string system = GetInput("Enter the system of the incident: ");
            string description = GetInput("Enter the description of the incident: ");
            
            Incident incidentObj = new Incident(
                severityUser,
                Status.Open,
                cve,
                DateTime.Now,
                userObj.UserId,
                system,
                description
            );
            
            Incident.AddIncident(incidentObj);
            
            break;
        case 3:
            List<Incident> incidentList = Incident.ShowIncident();

            if (incidentList.Count > 0)
            {
                Incident? incidentToEdit = null; 
                while (incidentToEdit == null)
                {
                    foreach (var incident in incidentList)
                    {
                        Console.WriteLine(incident.IncidentId + " – " + incident.Description);
                    }

                    string incidentId = GetInput("Enter the ID of the incident you want to edit: ");
                    
                    if (int.TryParse(incidentId, out int parsedId))
                    {
                        
                        incidentToEdit = Incident.GetIncident(parsedId);
                        if (incidentToEdit != null)
                        {
                            incidentToEdit.Status = GetStatusInput($"Status (default = {incidentToEdit.Status.ToString()}):", true);
                            incidentToEdit.Severity = GetSeverityInput($"Severity (default = {incidentToEdit.Severity.ToString()}): ", true);
                            incidentToEdit.Cve = GetInput($"CVE (default = {incidentToEdit.Cve}): ", true);
                            incidentToEdit.Description = GetInput($"Description (default = {incidentToEdit.Description}): ", true);
                            
                            Incident.UpdateIncident(incidentToEdit);                        
                        }
                        
                        else
                        {
                            Console.WriteLine("No incident found with the given ID!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid ID! Please enter a valid incident ID.");
                    }
                }
            }
            else
            {
                Console.WriteLine("No incidents found!");
            }
            
            break;
        case 4:
            // todo: copy only the status from case 3 and change the status to closed
            
            Incident.CloseIncident();
            break;
        case 5:
            Environment.Exit(0);
            break;
        default:
            break;
    }

} while (Convert.ToInt32(userInput) != 5);


static User UserAuthentication()
{
    // dummy user authentication
    Console.WriteLine("Welcome to the ACDT Incident Management System");
    Console.Write("Please enter your username: ");
    string username = Console.ReadLine() ?? "";
    Console.Write("Please enter your password: ");
    string password = Console.ReadLine() ?? "";
    
    // now we get the user from the database, however we just use a dummy user for now
    User user = new User("admin", 1, "123456789", "test@fkasdf.com", 1);

    return user;
}


static Severity GetSeverityInput(string prompt, bool defaultAllowed = false)
{
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine() ?? "";

        if (string.IsNullOrEmpty(input))
        {
            if (defaultAllowed)
                return Severity.Low;
            else
                continue;
        }

        if (int.TryParse(input, out int severity) && severity >= 1 && severity <= 4)
        {
            return (Severity)severity;
        }

        Console.WriteLine("Invalid input! Please enter a value between 1-4" + (defaultAllowed ? " or press Enter for default value." : "."));
    }
}

static string GetInput(string prompt, bool defaultAllowed = false)
{
    string input;
    do
    {
        Console.Write(prompt);
        input = Console.ReadLine() ?? "";
    } while (!defaultAllowed && string.IsNullOrEmpty(input)); 

    return input;
}

static Status GetStatusInput(string prompt, bool defaultAllowed = false)
{
    string input;
    Status result;
    do
    {
        Console.Write(prompt);
        input = Console.ReadLine() ?? "";

        if (defaultAllowed && string.IsNullOrEmpty(input))
            return default;

        if (Enum.TryParse<Status>(input, true, out result))
            return result;

        Console.WriteLine("Invalid status. Please enter again.");

    } while (true);
}


