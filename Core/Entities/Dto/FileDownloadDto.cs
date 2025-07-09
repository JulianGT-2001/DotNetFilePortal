namespace Core.Entities.Dto;

public class FileDownloadDto
{
    public Stream ContentStream { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public string OriginalName { get; set; } = default!;
}