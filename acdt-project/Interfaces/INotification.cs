namespace acdt_project.Classes;

public interface INotification
{
    public int NotificationId { get; set; }
    public int Sender { get; set; }
    public int Receiver { get; set; }

    void Notify();
}