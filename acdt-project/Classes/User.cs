using acdt_project.Database;

namespace acdt_project.Classes;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public int RoleId { get; set; }
    public string PhoneNumber { get; set; }
    public string EMail { get; set; }

    public User(string username, int roleId, string phoneNumber, string eMail, int userId)
    {
        Username = username;
        RoleId = roleId;
        PhoneNumber = phoneNumber;
        EMail = eMail;
        UserId = userId;
    }

    public User(string username, int roleId, string phoneNumber, string eMail)
    {
        Username = username;
        RoleId = roleId;
        PhoneNumber = phoneNumber;
        EMail = eMail;
    }

    public static void AddUser(User userObj)
    {
        using (var context = new UserContext())
        {
            context.Users.Add(userObj);
            context.SaveChanges();
        }
    }

    public void DeleteUser()
    {
        throw new NotImplementedException();
    }
}