namespace Core.Entities.Dto
{
    public class FileResponseDto
    {
        public Guid Id { get; set; } = default!;
        public string OriginalName { get; set; } = default!;
        public string Path { get; set; } = default!;
        public long SizeInBytes { get; set; }
        public string MimeType { get; set; } = default!;
        public DateTime UploadedAt { get; set; }
    }
}