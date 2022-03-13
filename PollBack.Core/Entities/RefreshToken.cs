using PollBack.Core.UserAggregate;
using PollBack.Shared;

namespace PollBack.Core.Entities;

public class RefreshToken : BaseEntity
{
    public int UserId { get; set; }
    public User? User { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public string CreatedByIp { get; set; }
    public DateTime? Revoked { get; set; }
    public string? RevokedByIp { get; set; }
    public string? ReplacedByToken { get; set; }
    public string? ReasonRevoked { get; set; }

    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsRevoked => Revoked != null;
    public bool IsActive => !IsRevoked && !IsExpired;

    public RefreshToken(string token, string createdByIp)
    {
        Token = token;
        CreatedByIp = createdByIp;
    }

}
