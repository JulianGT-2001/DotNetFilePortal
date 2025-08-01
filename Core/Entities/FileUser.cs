using System.Diagnostics.CodeAnalysis;

namespace Core.Entities
{
    [ExcludeFromCodeCoverage]
    public class FileUser
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid FileId { get; set; }
        public string UserId { get; set; } = default!;

        public FileEntity File { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}