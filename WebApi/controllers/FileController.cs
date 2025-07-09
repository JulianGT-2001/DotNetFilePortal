using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.controllers;

[ApiController]
[Route("/api/[controller]")]
public class FileController : ControllerBase
{
    #region Attributes
    private readonly IFileService _service;
    #endregion
    #region Constructor
    public FileController(IFileService service)
    {
        _service = service;
    }
    #endregion
    #region Methods
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] IFormFileCollection files, [FromForm] string userId)
    {
        await _service.RegisterFileAsync(files, userId);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.GetFilesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        return Ok(await _service.GetFileAsync(id));
    }

    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download(Guid id)
    {
        var result = await _service.GetFileContentAsync(id);

        if (result == null)
            return NotFound();

        return File(
            result.ContentStream,
            result.ContentType,
            result.OriginalName
        );
    }
    #endregion
}