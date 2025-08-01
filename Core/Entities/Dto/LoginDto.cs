using System.Diagnostics.CodeAnalysis;

namespace Core.Entities.Dto
{
    [ExcludeFromCodeCoverage]
    public class LoginDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}