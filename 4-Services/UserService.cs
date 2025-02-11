using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace Talent;

public enum RoleType
{
    Admin = 1,
    Cheff = 2,
    Member = 3
}

public class UserService : IDisposable
{

    private readonly RestaurantContext _db;

    public UserService(RestaurantContext db)
    {
        _db = db;
    }

    public User? GetUserInformation(int id)
    {
        User? user = _db.Users.AsNoTracking().SingleOrDefault(u => u.UserId == id);
        if (user == null) return null;
        user.Password = null!; // one method
        return user;
    }

    public string Register(User user)
    {
        user.Email = user.Email.ToLower();
        user.Password = Cyber.HashPassword(user.Password); // encrypt the password
        user.RoleId = (int)RoleType.Member; // declare user as admin regardless of what was sent
        _db.Users.Add(user);
        _db.SaveChanges();
        user.Role = _db.Roles.Single(r => r.RoleId == user.RoleId);
        return JwtHelper.GetNewToken(user);
    }

    public string? Login(Credentials credentials)
    {
        credentials.Email = credentials.Email.ToLower();
        credentials.Password = Cyber.HashPassword(credentials.Password);
        User? user = _db.Users.AsNoTracking().Include(u => u.Role).SingleOrDefault(u => u.Email == credentials.Email && u.Password == credentials.Password);
        if (user == null) return null;
        return JwtHelper.GetNewToken(user);
    }

    public bool IsEmailTaken(string email)
    {
        return _db.Users.Any(u => u.Email == email);
    }


    public void Dispose()
    {
        _db.Dispose();
    }
}
