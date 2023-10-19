using System.Xml;
using acdt_project.Classes;
using acdt_project.Enums;


string defaultText = "Options:\n" +
                      "1 – Show Incidents\n" +
                      "2 – Add Incident\n" +
                      "3 – Edit Incident\n" +
                      "4 – Close Incident\n" +
                      "5 – Escalate\n" +
                      "--------------------------\n" +
                      "6 - Add User\n" +
                      "7 - Delete User\n";


User userObj = UserAuthentication();
Role roleObj = new Role();

string userInput = ""; 
do
{
    Console.Clear();
    Console.WriteLine(defaultText);
    Console.Write("Enter your choice: ");
    userInput = Console.ReadLine() ?? "";
    
    // we are not lazy loading, because for almost any operation we need the user object. this position is optimal, cuz user has to think about his next actions
    List<Incident> incidentList = Incident.FetchIncidents();
    
    switch (Convert.ToInt32(userInput) - 1)
    {
        case (int)MenuOption.ShowIncidents:
            ShowIncidents(incidentList);
            break;

        case (int)MenuOption.AddIncident:
            AddIncident(userObj);
            break;

        case (int)MenuOption.EditIncident:
            EditIncident(incidentList);
            break;

        case (int)MenuOption.CloseIncident:
            CloseIncident(incidentList);
            break;
        
        case (int)MenuOption.Escalate:
            EscalateIncident(incidentList);
            break;
        
        case (int)MenuOption.AddUser:
            AddUser(roleObj);
            break;
        case (int)MenuOption.DeleteUser:
            ShowUsers();
            DeleteUser(1);
            break;
        case (int)MenuOption.Exit:
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Invalid choice. Please select a valid option.");
            break;
    }

} while (Convert.ToInt32(userInput) != 5);


// Menu: Incident functions
static void ShowIncidents(List<Incident> incidentList)
{
    foreach (var incident in incidentList)
    {
        Console.WriteLine(incident.ToString());
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}

static void AddIncident(User userObj)
{
    Console.Clear();
    
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
    Console.WriteLine("Incident added successfully!\nPress any key to continue...");
    Console.ReadKey();
}

static void EditIncident(List<Incident> incidentList)
{
    Incident? incidentToEdit = SelectExistingIncident(incidentList);
    if (incidentToEdit != null)
    {
        incidentToEdit.Status = GetStatusInput($"Status (default = {incidentToEdit.Status.ToString()}):", true);
        incidentToEdit.Severity = GetSeverityInput($"Severity (default = {incidentToEdit.Severity.ToString()}): ", true);
        incidentToEdit.Cve = GetInput($"CVE (default = {incidentToEdit.Cve}): ", true);
        incidentToEdit.Description = GetInput($"Description (default = {incidentToEdit.Description}): ", true);
                            
        Incident.UpdateIncident(incidentToEdit);  
        Console.WriteLine($"Incident ID {incidentToEdit.IncidentId} edited successfully!\nPress any key to continue...");
        Console.ReadKey();
    }
}

static void CloseIncident(List<Incident> incidentList)
{
    Incident? incidentToEdit = SelectExistingIncident(incidentList);
    if (incidentToEdit != null)
    {
        incidentToEdit.Status = Status.Closed;
        Incident.UpdateIncident(incidentToEdit);     
        Console.WriteLine($"Incident ID {incidentToEdit.IncidentId} closed successfully!\nPress any key to continue...");
        Console.ReadKey();
    }
}

static void EscalateIncident(List<Incident> incidentList)
{
    if (SelectExistingIncident(incidentList) != null)
    {
        Console.WriteLine("Incident escalated successfully!\nPress any key to continue...");
    }
    
    // use the SelectExistingIncident and check if the return type is not null
    if(SelectExistingIncident(incidentList) != null)
    {
        SendMail();
    }
}

// Menu: User functions
static void AddUser(Role roleObj)
{
    Console.Clear();
    string userName = GetInput("Enter your Username: ");
    string phoneNumber = GetInput("Enter your Phonenumber: ");
    string email = GetInput("Enter your email address: ");
    
    User newUserObj = new User(
        userName,
        roleObj.roleID,
        phoneNumber,
        email
        );
            
    User.AddUser(newUserObj);
    Console.WriteLine("User added successfully!\nPress any key to continue...");
    Console.ReadKey();
}

static void ShowUsers()
{
    List<User> userList = User.FetchAllUser();
    foreach (var property in userList)
    {
        Console.WriteLine(property.ToString());
    }

}

static void DeleteUser(int userId)
{
    User.DeleteUser(userId);
}


// Helper functions
static Incident? SelectExistingIncident(List<Incident> incidentList)
{
    if (incidentList.Count == 0)
    {
        Console.WriteLine("No incidents found!");
        return null;
    }

    while (true)
    {
        foreach (var incident in incidentList)
        {
            Console.WriteLine(incident.IncidentId + " – " + incident.Description);
        }

        string incidentId = GetInput("Enter the ID of the incident (or 'exit' to quit): ");
        
        if (incidentId.ToLower() == "exit")
        {
            return null;
        }

        if (int.TryParse(incidentId, out int parsedId))
        {
            var incidentToEdit = Incident.GetIncident(parsedId);
            if (incidentToEdit != null)
            {
                return incidentToEdit;
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"No incident found with ID: {parsedId}. Try again.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID format. Try again.");
        }
    }
}

static void SendMail()
{
    // dummy mail notification for now
    int receiver = Convert.ToInt16(GetInput(("Please enter the UserID of the Receiver: ")));
    INotification mailNotification = new MailNotification();
    //mailNotification.Sender = 1;
    mailNotification.Receiver = receiver;
    mailNotification.Notify();
}

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
