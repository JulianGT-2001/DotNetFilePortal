namespace Core.Entities;

public class File
{
    public string Id { get; set; }
    public string OriginalName { get; set; } = default!;
    public string Path { get; set; } = default!;
    public long SizeInBytes { get; set; }
    public string MimeType { get; set; } = default!;
    public string UploadedBy { get; set; } = default!;
    public DateTime UploadedAt { get; set; }
}