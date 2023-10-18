namespace acdt_project.Classes;

public interface INotification
{
    //Thema Eskalation: Incidents können eskaliert werden, z.b. von helpdesk zu developer; developer sollte dann benachrichtig werden
    public int NotificationId { get; set; }
    public int Sender { get; set; }
    public int Receiver { get; set; }

    void Notify();
}