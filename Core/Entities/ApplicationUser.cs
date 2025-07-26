using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    [ExcludeFromCodeCoverage]
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        public ICollection<FileUser> FileUsers { get; set; } = new List<FileUser>();
    }
}