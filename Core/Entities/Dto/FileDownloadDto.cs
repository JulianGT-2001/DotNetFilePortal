using System.Diagnostics.CodeAnalysis;

namespace Core.Entities.Dto;

[ExcludeFromCodeCoverage]
public class FileDownloadDto
{
    public Stream ContentStream { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public string OriginalName { get; set; } = default!;
}