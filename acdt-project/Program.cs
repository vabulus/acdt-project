using System.Xml;
using acdt_project.Classes;
using acdt_project.Enums;
using Spectre.Console;


string[] severityArray = Enum.GetNames(typeof(Severity));
string[] statusArray = Enum.GetNames(typeof(IncidentStatus));


User userObj = UserAuthentication();
Role roleObj = new Role();

do
{
    
    List<Incident> incidentList = Incident.FetchIncidents();
    Console.Clear();
    
    var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title("Options")
        .PageSize(10)
        .MoreChoicesText("[grey](Use Up/Down to view more options)[/]")
        .AddChoices(new[]
        {
            "Show Incidents",
            "Add Incident",
            "Edit Incident",
            "Close Incident",
            "Escalate",
            "List Users",
            "Add User",
            "Delete User",
            "Message User",
            "Exit"
        }));
    
    switch (choice)
    {
        case "Show Incidents":
            ShowIncidents(incidentList);
            break;
        case "Add Incident":
            AddIncident(userObj);
            break;
        case "Edit Incident":
            EditIncident(incidentList);
            break;
        case "Close Incident":
            CloseIncident(incidentList);
            break;
        case "Escalate":
            EscalateIncident(incidentList);
            break;
        case "List Users":
            ShowUsers();
            break;
        case "Add User":
            AddUser(roleObj);
            break;
        case "Delete User":
            ShowUsers();
            DeleteUser(1);
            break;
        case "Message User":
            SendNotification();
            break;
        case "Exit":
            Environment.Exit(0);
            break;
    }
} while (true);


// Menu: Incident functions
static void ShowIncidents(List<Incident> incidentList)
{
    var table = new Table();
    table.AddColumn("ID");
    table.AddColumn("Severity");
    table.AddColumn("CVE");
    table.AddColumn("System");
    table.AddColumn("Description");

    foreach (var incident in incidentList)
    {
        table.AddRow(
            incident.IncidentId.ToString(),
            incident.Severity.ToString(),
            incident.Cve,
            incident.System,
            incident.Description
        );
    }

    AnsiConsole.Write(table);
    AnsiConsole.WriteLine();
    AnsiConsole.Markup("[bold]Press any key to continue...[/]");
    Console.ReadKey();
}

static void AddIncident(User userObj)
{
    Console.Clear();
    Severity severityUser = GetSeverityInput("Enter the severity of the incident (1-4, default = 1): ");
    
    // var favorites = AnsiConsole.Prompt(
    //     new SelectionPrompt<string>()
    //         .PageSize(10)
    //         .Title("What is the status of the incident?")
    //         .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
    //         .AddChoices(statusArray));
    
    
    string cve = GetInput("Enter the CVE of the incident: ");
    string system = GetInput("Enter the system of the incident: ");
    string description = GetInput("Enter the description of the incident: ");
            
    Incident incidentObj = new Incident(
        severityUser,
        IncidentStatus.Open,
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
        incidentToEdit.Status = IncidentStatus.Closed;
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
        SendNotification();
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

    var table = new Table();
    table.AddColumn("Username");
    table.AddColumn("Phone number");
    table.AddColumn("Email");

    foreach (var user in userList)
    {
        table.AddRow(
            user.Username,
            user.PhoneNumber,
            user.EMail
        );
    }
    AnsiConsole.Write(table);
    AnsiConsole.WriteLine();
    AnsiConsole.Markup("[bold]Press any key to continue...[/]");
    Console.ReadKey();
}

static void DeleteUser(int userId)
{
    User.DeleteUser(userId);
}

static void SendNotification()
{
    string receiverName = GetInput(("Please enter the name of the receiver: "));
    User recipient = User.GetUser(receiverName);
    

    // string defaultText = "What messaging channel would you like to use?:\n" +
    //                      "1 – Email\n" +
    //                      "2 – Signal\n" +
    //                      "3 – SMS\n" +
    //                      "4 – Exit\n";
    //
    // Console.WriteLine(defaultText);
    // Console.Write("Enter your choice: ");
    // string userInput = Console.ReadLine() ?? "";
    //
    // switch (Convert.ToInt16(userInput))
    // {
    //     case 1:
    //         INotification mailNotification = new MailNotification();
    //         mailNotification.Receiver = recipient.UserId;
    //         mailNotification.Notify();
    //         break;
    //     case 2:
    //         INotification signalNotification = new SignalNotification();
    //         signalNotification.Receiver = recipient.UserId;
    //         signalNotification.Notify();
    //         break;
    //     case 3:
    //         INotification smsNotification = new SmsNotification();
    //         smsNotification.Receiver = recipient.UserId;
    //         smsNotification.Notify();
    //         break;
    // }
    //
    // List<Incident> incidentList = Incident.FetchIncidents();
    // Console.Clear();
    
    var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
        .Title("What messaging channel would you like to use?:")
        .PageSize(10)
        .MoreChoicesText("[grey](Use Up/Down to view more options)[/]")
        .AddChoices(new[]
        {
            "Email",
            "Signal",
            "SMS",
            "Exit"
        }));
    
    switch (choice)
    {
        case "Email":
            INotification mailNotification = new MailNotification();
            mailNotification.Receiver = recipient.UserId;
            mailNotification.Notify();
            break;
        case "Signal":
            INotification signalNotification = new SignalNotification();
            signalNotification.Receiver = recipient.UserId;
            signalNotification.Notify();
            break;
        case "SMS":
            INotification smsNotification = new SmsNotification();
            smsNotification.Receiver = recipient.UserId;
            smsNotification.Notify();
            break;
        case "Exit":
            Environment.Exit(0);
            break;
    }
}

// Helper functions
static Incident? SelectExistingIncident(List<Incident> incidentList)
{
    if (incidentList.Count == 0)
    {
        AnsiConsole.WriteLine("No incidents found!");
        return null;
    }

    while (true)
    {
        foreach (var incident in incidentList)
        {
            AnsiConsole.WriteLine(incident.IncidentId + " – " + incident.Description);
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
                AnsiConsole.Clear();
                AnsiConsole.WriteLine($"No incident found with ID: {parsedId}. Try again.");
            }
        }
        else
        {
            AnsiConsole.WriteLine("Invalid ID format. Try again.");
        }
    }
}

static User UserAuthentication()
{
    // // dummy user authentication
    // Console.WriteLine("Welcome to the ACDT Incident Management System");
    // Console.Write("Please enter your username: ");
    // string username = Console.ReadLine() ?? "";
    // Console.Write("Please enter your password: ");
    // string password = Console.ReadLine() ?? "";
    //
    // now we get the user from the database, however we just use a dummy user for now
    User user = new User("admin", 1, "123456789", "test@fkasdf.com", 1);

    return user;
}

static Severity GetSeverityInput(string prompt, bool defaultAllowed = false)
{
    while (true)
    {
        AnsiConsole.Write(prompt);
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

        AnsiConsole.WriteLine("Invalid input! Please enter a value between 1-4" + (defaultAllowed ? " or press Enter for default value." : "."));
    }
}

static string GetInput(string prompt, bool defaultAllowed = false)
{
    string input;
    do
    {
        AnsiConsole.Markup(prompt);
        input = Console.ReadLine() ?? "";
    } while (!defaultAllowed && string.IsNullOrEmpty(input));

    return input;
}


static IncidentStatus GetStatusInput(string prompt, bool defaultAllowed = false)
{
    string input;
    IncidentStatus result;
    do
    {
        AnsiConsole.Markup(prompt);
        input = Console.ReadLine() ?? "";

        if (defaultAllowed && string.IsNullOrEmpty(input))
            return default;

        if (Enum.TryParse<IncidentStatus>(input, true, out result))
            return result;

        AnsiConsole.WriteLine("Invalid status. Please enter again.");

    } while (true);
}
