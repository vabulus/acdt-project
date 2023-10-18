namespace acdt_project.Classes;

public class SmsNotification : INotification
{
    public string phoneNumber { get; set; }

    public int NotificationId { get; set; }
    public int Sender { get; set; }
    public int Receiver { get; set; }

    public void Notify()
    {
       // Console.WriteLine("Notification send to " + Receiver + "from " + Sender + "via SMS");
    }
}