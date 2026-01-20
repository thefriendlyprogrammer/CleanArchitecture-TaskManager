using Microsoft.AspNetCore.Identity;

namespace TaskManager.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public int CreatedById { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public int? LastModifiedById { get; set; }
        public DateTimeOffset? LastModifiedDateTime { get; set; }
    }
}
