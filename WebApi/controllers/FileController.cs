using System.Security.Claims;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.controllers;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class FileController : ControllerBase
{
    #region Attributes
    private readonly IFileService _fileService;
    private readonly IFileUserService _fileUserservice;
    #endregion
    #region Constructor
    public FileController(IFileService service, IFileUserService fileUserService)
    {
        _fileService = service;
        _fileUserservice = fileUserService;
    }
    #endregion
    #region Methods
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] IFormFileCollection files)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        var fileGuids = await _fileService.RegisterFileAsync(files, userId);
        await _fileUserservice.RegisterFileUserAsync(userId, fileGuids);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        await _fileUserservice.DeleteUserFileAsync(userId, id);
        await _fileService.DeleteFileAsync(id);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        return Ok(await _fileUserservice.GetFilesUserAsync(userId));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        return Ok(await _fileUserservice.GetFileUserAsync(userId, id));
    }

    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download(Guid id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();
        var result = await _fileUserservice.GetFileContentUserAsync(userId, id);

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