namespace Core.Entities;

public class FileEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string OriginalName { get; set; } = default!;
    public string Path { get; set; } = default!;
    public long SizeInBytes { get; set; }
    public string MimeType { get; set; } = default!;
    public DateTime UploadedAt { get; set; }

    public ICollection<FileUser> FileUsers { get; set; } = new List<FileUser>();
}