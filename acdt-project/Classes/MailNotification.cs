using acdt_project.Database;

namespace acdt_project.Classes;

public class MailNotification :INotification
{
    private readonly UserContext dbContext;
    
    public string MailAddress { get; set; }
    
    public int NotificationId { get; set; }
    public int Sender { get; set; }
    public int Receiver { get; set; }
    private string hey;

    public MailNotification(UserContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public void Notify()
    {
       // Console.WriteLine("Notification send to " + Receiver + "from " + Sender + " via Mail");
    }
}