using System.ComponentModel.DataAnnotations;
using acdt_project.Database;
using Microsoft.EntityFrameworkCore;

namespace acdt_project.Classes;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string Username { get; set; }
    public int RoleId { get; set; }
    public string PhoneNumber { get; set; }
    public string EMail { get; set; }

    public User()
    {
        
    }
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
        using (var context = new IncidentContext())
        {
            context.Users.Add(userObj);
            context.SaveChanges();
        }
    }

    public static bool DoesUserExist(string usernameToCheck)
    {
        using (var context = new IncidentContext())
        {
            User userToCheck = context.Users.FirstOrDefault(u => u.Username == usernameToCheck);
            if (userToCheck != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
    public static User GetUser(string username)
    {
        using (var context = new IncidentContext())
        { 
            return context.Users.SingleOrDefault(i => i.Username == username);
        }
    }
    
    public static List<User> FetchAllUser()
    {
        using (var context = new IncidentContext())
        {
            var user = context.Users.ToListAsync().Result;
            return user;
        }  
    }

    public static void DeleteUser(int userId)
    {
        using (var context = new IncidentContext())
        {
            var user = context.Users.Single(u => u.UserId == userId);
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}