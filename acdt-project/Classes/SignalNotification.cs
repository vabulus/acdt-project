using acdt_project.Database;

namespace acdt_project.Classes;

public class SignalNotification : INotification
{
    public string phoneNumber { get; set; }
    public int Sender { get; set; }
    public int Receiver { get; set; }

    public void Notify()
    {
        using (var dbContext = new IncidentContext())
        {
            var user = dbContext.Users.FirstOrDefault(u => u.UserId == Receiver);

            if (user != null)
            {
                Console.WriteLine($"Signal Message sent to {user.Username} via {user.PhoneNumber} and Signal ");
            }
            else
            {
                Console.WriteLine("User not found");
            }
        }
    }
}