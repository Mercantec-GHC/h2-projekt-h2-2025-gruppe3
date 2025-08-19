namespace DomainModels.Mapping;

public class UserMapping
{
    public static UserGetDto ToUserGetDto(User user)
    {
        return new UserGetDto
        {
            Id = user.Id,
            Email = user.Email,
<<<<<<< HEAD
            Username = user.Username,
=======
            FirstName = user.FirstName,
            LastName = user.LastName,
>>>>>>> Dev
            Role = user.Role?.Name ?? string.Empty
        };
    }
}
