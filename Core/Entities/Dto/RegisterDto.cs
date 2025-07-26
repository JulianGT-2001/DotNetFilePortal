using System.Diagnostics.CodeAnalysis;

namespace Core.Entities.Dto
{
    [ExcludeFromCodeCoverage]
    public class RegisterDto
    {
        public string Email { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}