namespace TaskManager.Infrastructure.Identity
{
    public interface IJwtTokenService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
    }
}
