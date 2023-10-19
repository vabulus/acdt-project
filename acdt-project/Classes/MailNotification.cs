using acdt_project.Database;
using Org.BouncyCastle.Bcpg;

namespace acdt_project.Classes;

public class MailNotification : INotification
{
    public int NotificationId { get; set; }
    public int Sender { get; set; }
    public int Receiver { get; set; }
    
    public void Notify()
    {
       using (var dbContext = new IncidentContext())
       {
           var user = dbContext.Users.FirstOrDefault(u => u.UserId == Receiver);

           if (user != null)
           {
               Console.WriteLine($"Mail Message sent to {user.Username} via {user.EMail} ");
           }
           else
           {
               Console.WriteLine("User not found");
           }
       }
       
    }
}