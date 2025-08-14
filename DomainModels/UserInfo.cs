// UserInfo.cs
using DomainModels;

public class UserInfo
{

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? AvatarUrl { get; set; }

    public string UserId { get; set; } = string.Empty;

    public User User { get; set; } = default!;
}