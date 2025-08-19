namespace DomainModels.Mapping;

public class UserMapping
{
    public static UserGetDto ToUserGetDto(User user)
    {
        return new UserGetDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = user.Role?.Name ?? string.Empty
        };
    }
}
